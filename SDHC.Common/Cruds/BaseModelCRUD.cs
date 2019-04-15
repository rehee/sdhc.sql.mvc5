using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    
  }
}
