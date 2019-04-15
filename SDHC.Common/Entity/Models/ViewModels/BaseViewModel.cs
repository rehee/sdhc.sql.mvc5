using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Entity.Models
{
  public abstract class BaseViewModel : IInt64Key
  {
    [BaseProperty]
    public virtual long Id { get; set; }
    [BaseProperty]
    public virtual string FullType { get; set; }
    [BaseProperty]
    public virtual string ThisAssembly { get; set; }

    public virtual Type ModelType()
    {
      return Type.GetType($"{this.FullType},{this.ThisAssembly}");
    }

    public virtual void SetViewModel(IInt64Key model = null)
    {
      if (model != null)
      {
        var type = model.GetType().GetRealType();
        FullType = type.FullName;
        ThisAssembly = type.Assembly.FullName;
        model.SetObjectByObject(this);
      }
    }

    public virtual object ConvertToModel(IInt64Key model = null)
    {
      if (model == null)
      {
        var type = Type.GetType($"{FullType},{ThisAssembly}");
        if (type == null)
        {
          return null;
        }
        model = Activator.CreateInstance(type) as IInt64Key;
      }
      else
      {
        if (model.Id != this.Id)
        {
          return null;
        }
      }
      this.SetObjectByObject(model);
      return model;
    }
  }
}
