using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ViewModelManager
  {
    public static void Update(this BaseViewModel input)
    {
      var model = ModelManager.Find(input.ModelType(), input.Id,out ISave repo);
      if (model == null)
        return;
      input.ConvertToModel(model as IInt64Key);
      repo.SaveChanges();
    }
  }
}
