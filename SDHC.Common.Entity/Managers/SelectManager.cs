﻿using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class SelectManager
  {
    public static Type BasicSelectType { get; set; } = typeof(BaseSelect);
    public static IEnumerable<BaseSelect> GetAllSelect(Type selectType)
    {
      var dbset = BaseCruds.GetRepo().GetDbSet(selectType);
      if(dbset==null)
        return Enumerable.Empty<BaseSelect>();
      try
      {
        return Queryable.Where<BaseSelect>(dbset as IQueryable<BaseSelect>, b => true).ToList();
      }
      catch {
        return Enumerable.Empty<BaseSelect>();
      }
    }
    public static IEnumerable<BaseSelect> GetAllSelect(string type)
    {
      var children = BasicSelectType.GetAllowChildren();
      if (children == null)
        return Enumerable.Empty<BaseSelect>();
      var selectType = children.ChildrenType.Where(b => String.Equals(b.FullName, type, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
      if (children == null)
        return Enumerable.Empty<BaseSelect>();
      return GetAllSelect(selectType);
    }
    public static IEnumerable<Type> GetAllAvaliableSelect()
    {
      var avaliable = BasicSelectType.GetObjectCustomAttribute<AllowChildrenAttribute>(true);
      if (avaliable == null || avaliable.ChildrenType == null)
      {
        return Enumerable.Empty<Type>();
      }
      return avaliable.ChildrenType;
    }
    public static IEnumerable<DropDownSummary> GetAllAvaliableSelectList()
    {
      var list = GetAllAvaliableSelect();
      if (list == null)
        return Enumerable.Empty<DropDownSummary>();
      return list.Select(b =>
      {
        var count = BaseCruds.Read<IInt64Key>(b, c => true, out ISave db).ToList().Count;
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
