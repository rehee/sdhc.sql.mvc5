using SDHC.Common.Cruds;
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
  public class CrudModel : BaseCruds, ICrudModel
  {
    public CrudModel(ICrudInit container) : base(container)
    {
    }

    public void Create(ModelPostModel model)
    {
      var obj = model.ConvertToBaseModel();
      Create(obj);
    }
    public void Update(ModelPostModel model)
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
    public void Update(BaseViewModel model)
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
