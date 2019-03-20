using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Cruds
{
  public static class ContentCruds
  {
    public static Func<IContent> GetRepo { get; set; }
    public static void Create(BaseContent input)
    {
      var db = GetRepo();
      db.Contents.Add(input);
      db.SaveChanges();
    }
    public static T Read<T>(long id) where T : BaseContent
    {
      return GetByPK<T>(id, out IContent db);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : BaseContent
    {
      
      var dbset = GetRepo().GetDbSet(typeof(T));
      if (dbset == null)
      {
        return null;
      }
      var a = dbset as IQueryable<T>;
      return Queryable.Where<T>(a, where);
    }
    public static void Update<T>(T input) where T : BaseContent
    {
      var model = GetByPK<T>(input.Id, out IContent db);
      if (model == null)
      {
        return;
      }
      var type = typeof(T);
      foreach (var p in type.GetProperties())
      {
        try
        {
          p.SetValue(model, p.GetValue(input));
        }
        catch { }
      }
      db.SaveChanges();
    }
    public static void Delete(long id, IContent db = null)
    {
      if (db == null)
      {
        db = GetRepo();
      }
      var model = db.Contents.Find(id);
      if (model == null)
      {
        return;
      }
      var childrens = db.Contents.Where(b => b.ParentId == id).ToList();
      if (childrens.Count > 0)
      {
        childrens.ForEach(b => Delete(b.Id, db));
      }
      db.Contents.Remove(model);
      db.SaveChanges();
    }

    private static T GetByPK<T>(long id, out IContent db) where T : BaseContent
    {
      db = GetRepo();
      var result = db.Contents.Where(b => b.Id == id).FirstOrDefault();
      if (result == null)
      {
        return default(T);
      }
      return result as T;
    }

    public static object GetDbSet(this IContent repo, Type type)
    {
      var repoType = repo.GetType();
      foreach (var p in repoType.GetProperties())
      {
        if (p.PropertyType.GenericTypeArguments == null)
        {
          continue;
        }
        var t = p.PropertyType.GenericTypeArguments.FirstOrDefault();
        if (t == null)
        {
          continue;
        }
        if (t == type)
        {
          return p.GetValue(repo);
        }
      }
      return null;
    }
  }

  public static class ContentManager
  {
    public static void CreateContent(BaseContent input, long? parentId = null)
    {
      if (parentId != null)
      {
        var parent = ContentCruds.Read<BaseContent>(parentId.Value);
        if (parent != null)
        {
          input.ParentId = parent.Id;
        }
      }

      ContentCruds.Create(input);
    }
    public static void UpdateContent(BaseContent input)
    {
      ContentCruds.Update<BaseContent>(input);
    }
    public static void MoveContent(long contentId, long? parentId)
    {
      var content = ContentCruds.Read<BaseContent>(contentId);
      var parent = parentId.HasValue ? ContentCruds.Read<BaseContent>(parentId.Value) : null;
      if (content == null)
      {
        return;
      }
      content.ParentId = parent != null ? (long?)parent.Id : null;
      ContentCruds.Update<BaseContent>(content);
    }
  }
  
  public static class SelectManager
  {
    public static IEnumerable<BaseSelect> GetAllSelect(Type selectType)
    {
      var dbset = ContentCruds.GetRepo().GetDbSet(selectType);
      return Queryable.Where<BaseSelect>(dbset as IQueryable<BaseSelect>, b => true).ToList();
    }
  }

}


