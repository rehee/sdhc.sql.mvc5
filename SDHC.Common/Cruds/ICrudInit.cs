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
  public interface ICrudSelect : ICrudInit
  {
    Type BasicSelectType { get; }
  }
  public class CrudSelectInit : CrudInit, ICrudSelect
  {
    public CrudSelectInit(Func<ISave> getRepo, Type baseIContentModelType, Type basicSelectType) : base(getRepo, baseIContentModelType)
    {
      BasicSelectType = basicSelectType;
    }
    public Type BasicSelectType { get; }
  }
}
