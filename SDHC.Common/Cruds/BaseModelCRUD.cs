using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class BaseModelCRUD
  {
    public static void Create(this IInt64Key model, ISave repo)
    {
      var type = model.GetType().GetRealType();
      var repoType = repo.GetType();
      var addMethod = repo.GetMethod(type, "Add", out object p);
      if (addMethod != null)
      {
        try
        {
          addMethod.Invoke(p, new object[1] { model });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }
    public static void Create<T>(T input) where T : class
    {
      var type = typeof(T);
      var set = BaseCruds.GetDbSet<T>(out ISave repo);
      var addFunc = set.GetMethod("Add");
      addFunc.Invoke(set, new object[1] { input });
      repo.SaveChanges();
    }
    public static void Create(object input)
    {
      var type = input.GetType().GetRealType();
      var repo = BaseCruds.GetRepo();
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
    public static void Create(ModelPostModel model)
    {
      var obj = model.ConvertToBaseModel();
      Create(obj);
    }

    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave repo) where T : class
    {
      repo = BaseCruds.GetRepo();
      return Read<T>(where, repo);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class
    {
      var result = Read<T>(where, out ISave repo);
      return result;
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave repo) where T : class
    {
      return BaseCruds.Read<T>(where, repo);
    }

    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave repo)
    {
      var tType = typeof(T);

      var set = BaseCruds.GetDbSet(type, out repo);
      var q = Queryable.Where<T>((IQueryable<T>)set, where);
      return q;
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where)
    {
      var result = Read<T>(type, where, out ISave repo);
      return result;
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, ISave repo)
    {
      var tType = typeof(T);
      var set = BaseCruds.GetDbSet(repo, type);
      var q = Queryable.Where<T>((IQueryable<T>)set, where);
      return q;
    }

    public static void Update(ModelPostModel model)
    {
      var type = Type.GetType($"{model.FullType},{model.ThisAssembly}");
      if (type == null)
        return;
      var target = Read<IInt64Key>(type, b => b.Id == model.Id, out ISave repo).FirstOrDefault();
      if (target == null)
        return;
      model.ConvertToBaseModel(target);
      try
      {
        repo.SaveChanges();
      }
      catch { }
    }
    public static void Update(this BaseViewModel model)
    {
      var type = model.ModelType();
      if (type == null)
        return;
      var baseModel = Read<IInt64Key>(type, b => b.Id == model.Id, out var repo).FirstOrDefault();
      if (baseModel == null)
        return;
      model.ConvertToModel(baseModel);
      try
      {
        repo.SaveChanges();
      }
      catch { }
    }
  }
}
