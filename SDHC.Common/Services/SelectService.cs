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

    public SelectService(CrudInit init) : base(init)
    {

    }
    private List<Type> allowSelect { get; } = new List<Type>();
    public void AddSelectType<T>() where T : IBasicSelect
    {
      var type = typeof(T);
      if (!allowSelect.Any(b => b == type))
        allowSelect.Add(type);
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
      if (!allowSelect.Any())
        return Enumerable.Empty<IBasicSelect>();
      var selectType = allowSelect.FirstOrDefault(b => b.Name == type);
      if (selectType == null)
        return Enumerable.Empty<IBasicSelect>();
      return GetAllSelect(selectType);
    }
    public IEnumerable<Type> GetAllAvaliableSelect()
    {
      return allowSelect;
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
