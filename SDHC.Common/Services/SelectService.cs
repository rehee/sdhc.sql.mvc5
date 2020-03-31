using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.Common.Services
{
  public class SelectService : ModelService, ISelectService
  {
    public Type BasicSelectType { get; }
    public SelectService(ICrudSelect init) : base(init)
    {
      this.BasicSelectType = init.BasicSelectType;
    }
    public IEnumerable<IBasicSelect> GetAllSelect(Type selectType)
    {
      var dbset = GetRepo().GetDbSet(selectType);
      if (dbset == null)
        return Enumerable.Empty<IBasicSelect>();
      try
      {
        return Queryable.Where<IBasicSelect>(dbset as IQueryable<IBasicSelect>, b => true).ToList();
      }
      catch
      {
        return Enumerable.Empty<IBasicSelect>();
      }
    }
    public IEnumerable<IBasicSelect> GetAllSelect(string type)
    {
      var children = BasicSelectType.GetAllowChildren();
      if (children == null)
        return Enumerable.Empty<IBasicSelect>();
      var selectType = children.ChildrenType.Where(b => String.Equals(b.FullName, type, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
      if (children == null)
        return Enumerable.Empty<IBasicSelect>();
      return GetAllSelect(selectType);
    }
    public IEnumerable<Type> GetAllAvaliableSelect()
    {
      var avaliable = BasicSelectType.GetObjectCustomAttribute<AllowChildrenAttribute>(true);
      if (avaliable == null || avaliable.ChildrenType == null)
      {
        return Enumerable.Empty<Type>();
      }
      return avaliable.ChildrenType;
    }
    public IEnumerable<DropDownSummary> GetAllAvaliableSelectList()
    {
      var list = GetAllAvaliableSelect();
      if (list == null)
        return Enumerable.Empty<DropDownSummary>();
      return list.Select(b =>
      {
        var count = Read<IInt64Key>(b, c => true, out ISave db).ToList().Count;
        var allowChild = b.GetObjectCustomAttribute<AllowChildrenAttribute>();
        var dropDownName = allowChild != null && String.IsNullOrEmpty(allowChild.Name) ? allowChild.Name : b.Name;
        return new DropDownSummary()
        {
          Count = count,
          DropDownName = dropDownName,
          TypeName = b.FullName
        };
      });
    }
  }
}
