using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Cruds
{
  public interface ICrudInit
  {
    Func<ISave> GetRepo { get; }
    Type BaseIContentModelType { get; }
  }

  public class CrudInit : ICrudInit
  {
    public Func<ISave> GetRepo { get; }
    public Type BaseIContentModelType { get; }
    public CrudInit(Func<ISave> getRepo, Type baseIContentModelType)
    {
      GetRepo = getRepo;
      BaseIContentModelType = baseIContentModelType;
    }
  }
}
