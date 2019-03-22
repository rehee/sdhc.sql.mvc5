using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ContentCruds
  {
    public static Func<IContent> GetRepo { get; set; } = () => BaseCruds.GetRepo() as IContent;
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
    public static IQueryable<IInt64Key> Read(Type type, Expression<Func<IInt64Key, bool>> where)
    {
      var dbset = GetRepo().GetDbSet(type);
      if (dbset == null)
      {
        return null;
      }
      var a = dbset as IQueryable<IInt64Key>;
      return Queryable.Where<IInt64Key>(a, where);
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

    public static T GetByPK<T>(long id, out IContent db) where T : BaseContent
    {
      db = GetRepo();
      var result = db.Contents.Where(b => b.Id == id).FirstOrDefault();
      if (result == null)
      {
        return default(T);
      }
      return result as T;
    }
  }

  public static class ContentManager
  {
    public static Type BasicContentType { get; set; } = typeof(BaseContent);
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
    public static void UpdateContent(ContentPostModel input)
    {
      var content = ContentCruds.GetByPK<BaseContent>(input.Id,out IContent db);
      if (content == null)
      {
        return;
      }
      input.ConvertToBaseModel(content);
      db.SaveChanges();
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
    public static IEnumerable<BaseContent> GetAllChildContent(long? parentId)
    {
      return ContentCruds.Read<BaseContent>(
        b => b.ParentId == parentId)
        .AsQueryable();
    }
    public static BaseContent GetContent(long? id)
    {
      if (!id.HasValue)
      {
        return null;
      }
      return ContentCruds.Read<BaseContent>(id.Value);
    }
    public static ContentPostModel GetPreCreate(long? id, string fullType)
    {
      long? parentId = null;
      if (id.HasValue)
      {
        var parent = ContentCruds.Read<BaseContent>(id.Value);
        if (parent != null)
        {
          parentId = parent.Id;
        }
      }
      var type = Type.GetType(fullType);
      if (type == null)
      {
        return null;
      }
      var model = Activator.CreateInstance(type) as BaseContent;
      model.ParentId = parentId;
      return model.ConvertModelToPost();
    }
  }

  public static class SelectManager
  {
    public static Type BasicSelectType { get; set; } = typeof(BaseSelect);
    public static IEnumerable<BaseSelect> GetAllSelect(Type selectType)
    {
      var dbset = BaseCruds.GetRepo().GetDbSet(selectType);
      return Queryable.Where<BaseSelect>(dbset as IQueryable<BaseSelect>, b => true).ToList();
    }
    public static IEnumerable<Type> GetAllAvaliableType()
    {
      var avaliable = BasicSelectType.GetObjectCustomAttribute<AllowChildrenAttribute>(true);
      if(avaliable==null || avaliable.ChildrenType == null)
      {
        return Enumerable.Empty<Type>();
      }
      return avaliable.ChildrenType;
    }

  }

}


