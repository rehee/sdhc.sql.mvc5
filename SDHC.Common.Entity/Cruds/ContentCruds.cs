using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
}


