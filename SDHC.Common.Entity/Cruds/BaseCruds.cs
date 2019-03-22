using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
          return p.GetValue(repo);
        }
      }
      return null;
    }
    public static DbSet<T> GetDbSet<T>(this ISave repo) where T : class
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
  }
}
