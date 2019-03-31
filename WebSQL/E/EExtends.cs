using SDHC.Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
  public static partial class E
  {
    public static AllowChildrenAttribute GetAllowChildren(this Type type)
    {
      return type.GetObjectCustomAttribute<AllowChildrenAttribute>();
    }

    public static AllowChildrenAttribute GetAllowChildren(this object input)
    {
      return input.GetType().GetObjectCustomAttribute<AllowChildrenAttribute>();
    }
    public static bool IsSingleRecordTable(this Type type)
    {
      var a = type.GetAllowChildren();
      if (a == null)
        return false;
      return a.SingleRecord;
    }
    public static bool IsSingleRecordTable(this object input)
    {
      return input.GetType().IsSingleRecordTable();
    }
    public static IEnumerable<string> GetTableList(AllowChildrenAttribute model, IEnumerable<string> defaultList)
    {
      if (model == null || model.TableList == null)
      {
        return defaultList;
      }
      return model.TableList;
    }
    public static IEnumerable<string> GetTableList(Type type, IEnumerable<string> defaultList)
    {
      return GetTableList(type.GetAllowChildren(), defaultList);
    }
  }
}