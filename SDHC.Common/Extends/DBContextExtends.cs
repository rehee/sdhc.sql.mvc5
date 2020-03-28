using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
  public static class DBContextExtends
  {
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
