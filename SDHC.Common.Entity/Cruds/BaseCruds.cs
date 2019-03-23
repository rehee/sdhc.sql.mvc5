using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class BaseCruds
  {
    public static Func<ISave> GetRepo { get; set; }
    public static object GetDbSet(this ISave repo, Type type)
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
          return p.GetValue(repo) as DbSet<T>;
        }
      }
      return null;
    }

    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave db) where T : class
    {
      db = GetRepo();
      var dbset = db.GetDbSet<T>();
      if (dbset == null)
        return null;
      return Queryable.Where<T>(dbset, where);
    }
    public static IQueryable<object> Read(Type type, Expression<Func<object, bool>> where, out ISave db)
    {
      db = GetRepo();
      var dbset = db.GetDbSet(type) as IQueryable<object>;
      if (dbset == null)
        return null;
      return Queryable.Where<object>(dbset, where);
    }
  }
}
