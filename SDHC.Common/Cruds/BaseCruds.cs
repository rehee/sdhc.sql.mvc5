using SDHC.Common.Entity.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System
{
  public static class BaseCruds
  {
    public static Func<ISave> GetRepo { get; set; }
    public static object GetDbSet(this ISave repo, Type type)
    {
      var repoType = repo.GetType().GetRealType();
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
          var tt = p.GetValue(repo);
          return tt;
        }
      }
      return null;
    }
    public static IQueryable<T> GetDbSet<T>(this ISave repo) where T : class
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
        if (t == typeof(T))
        {
          return (IQueryable<T>)p.GetValue(repo);
        }
      }
      return null;
    }
    public static IQueryable<T> GetDbSet<T>(out ISave repo) where T : class
    {
      repo = GetRepo();
      return GetDbSet<T>(repo);
    }
    public static IQueryable<object> GetDbSet(Type type, out ISave repo)
    {
      repo = GetRepo();
      var set = repo.GetDbSet(type);
      return (IQueryable<object>)set;
    }

    public static void Create(object input, out ISave repo)
    {
      repo = GetRepo();
      Create(input, repo);
    }
    public static void Create(object input)
    {
      Create(input, out var repo);
    }
    public static void Create(object input, ISave repo)
    {

      var type = input.GetType().GetRealType();
      var repoType = repo.GetType().GetRealType();
      var addMethod = repo.GetMethod(type, "Add", out object p);
      if (addMethod != null)
      {
        try
        {
          addMethod.Invoke(p, new object[1] { input });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }

    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave db) where T : class
    {
      db = GetRepo();
      return Read<T>(where, db);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class
    {
      return Read<T>(where, out var repo);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave db) where T : class
    {
      var dbset = db.GetDbSet<T>();
      if (dbset == null)
        return null;
      return Queryable.Where<T>(dbset, where);
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave db)
    {
      db = GetRepo();
      return Read<T>(type, where, db);
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where)
    {
      return Read<T>(type, where, out var repo);
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where,ISave db)
    {
      var dbset = db.GetDbSet(type) as IQueryable<T>;
      if (dbset == null)
        return null;
      return Queryable.Where<T>(dbset, where);
    }

    public static T Find<T>(long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public static T Find<T>(long id) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out ISave repo).FirstOrDefault();
      return result;
    }
    public static object Find(Type type, long id, out ISave repo)
    {
      var result = Read<IInt64Key>(type, b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public static object Find(Type type, long id)
    {
      var result = Find(type, id, out ISave repo);
      return result;
    }

    public static void Delete<T>(ISave repo, T model) where T : class, IInt64Key
    {
      var tType = typeof(T);
      var modelType = model.GetType().GetRealType();
      if (tType != modelType)
      {
        Delete(model as object, repo);
        return;
      }
      if (repo == null)
      {
        var id = model.Id;
        var deleteModel = Find<T>(id, out repo);
        if (deleteModel == null)
          return;
        Delete(deleteModel as object, repo);
      }
    }
    public static void Delete<T>(T model, ISave repo = null) where T : class, IInt64Key
    {
      Delete<T>(repo, model);
    }
    public static void Delete(object model, ISave repo = null)
    {
      var type = model.GetType().GetRealType();
      if (!type.GetInterfaces().Any(b => b == typeof(IInt64Key)))
        return;
      if (repo == null)
      {
        var id = ((IInt64Key)model).Id;
        model = Find(type, id, out repo);
      }
      var method = repo.GetMethod(type, "Remove", out object p);
      if (method != null)
      {
        try
        {
          method.Invoke(p, new object[1] { model });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }

    public static MethodInfo GetMethod(this ISave repo, Type property, string methodName, out object propertyObj)
    {
      var repoType = repo.GetType();
      propertyObj = null;
      foreach (var p in repoType.GetProperties())
      {
        if (p.PropertyType.GenericTypeArguments.FirstOrDefault() == property)
        {
          var addMethod = p.PropertyType.GetMethod(methodName);
          if (addMethod != null)
          {
            propertyObj = p.GetValue(repo);
          }
          return addMethod;
        }
      }
      return null;
    }

    public static MethodInfo GetMethod(this object input, string methodName)
    {
      var type = input.GetType();
      var addMethod = type.GetMethod(methodName);
      return addMethod;
    }
  }
}
