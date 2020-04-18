using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Services
{
  public interface ISelectService : IModelService
  {
    void AddSelectType<T>() where T : IBasicSelect;
    IEnumerable<IBasicSelect> GetAllSelect(Type selectType);
    IEnumerable<IBasicSelect> GetAllSelect(string type);
    IEnumerable<Type> GetAllAvaliableSelect();
    IEnumerable<DropDownSummary> GetAllAvaliableSelectList();
  }
}
