using SDHC.Common.Entity.Models;
using System.Linq;
using System.Linq.Expressions;

namespace System
{
  public static class ContentCruds
  {
    public static Func<ISave> GetRepo { get; set; } = () => BaseCruds.GetRepo();
    public static Type BaseIContentModelType { get; set; }
    public static void Create(IContentModel input)
    {
      var db = GetRepo();
      var IQueable = BaseCruds.GetDbSet(db, BaseIContentModelType);
      var method = BaseCruds.GetMethod(db, BaseIContentModelType, "Add", out var IContent);
      method.Invoke(IContent, new object[1] { input });
      db.SaveChanges();
    }
    public static T Read<T>(long id) where T : class, IContentModel
    {
      return GetByPK<T>(id, out ISave db);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : IContentModel
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
    public static void Update<T>(T input) where T : class, IContentModel
    {
      var model = GetByPK<T>(input.Id, out ISave db);
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
    public static void Delete(long id, ISave db = null)
    {
      if (db == null)
      {
        db = GetRepo();
      }
      var model = BaseCruds.Read<IContentModel>(BaseIContentModelType, b => b.Id == id, db).FirstOrDefault();
      if (model == null)
      {
        return;
      }
      var childrens = BaseCruds.Read<IContentModel>(BaseIContentModelType, b => b.ParentId == id, db).ToList();
      if (childrens.Count > 0)
      {
        childrens.ForEach(b => Delete(b.Id, db));
      }
      BaseCruds.Delete(model, db);
    }
    public static T GetByPK<T>(long id, out ISave db) where T : class, IContentModel
    {
      db = GetRepo();
      return BaseCruds.Read<T>(BaseIContentModelType, b => b.Id == id, db).FirstOrDefault();

    }
  }
}


