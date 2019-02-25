using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Extends
{
  public static class BaseModelCRUD
  {
    public static void Create(this IInt64Key model, ISave repo)
    {
      var type = model.GetType();
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
    

    public static MethodInfo GetMethod(this ISave repo, Type property, string methodName, out object propertyObj)
    {
      var repoType = repo.GetType();
      propertyObj = null;
      foreach (var p in repoType.GetProperties())
      {
        if (p.PropertyType.GenericTypeArguments.FirstOrDefault() == property)
        {
          var addMethod = p.PropertyType.GetMethod("Add");
          if (addMethod != null)
          {
            propertyObj = p.GetValue(repo);
          }
          return addMethod;
        }
      }
      return null;
    }
  }
}
