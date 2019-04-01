using SDHC.Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class TypeExtends
  {
    public static IEnumerable<Type> GetAllowChildrens(object input)
    {
      Type type;
      if (input == null)
      {
        type = ContentManager.BasicContentType;
      }
      else
      {
        type = input.GetType();
      }
      var allows = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
      if (allows == null || allows.ChildrenType == null)
      {
        return Enumerable.Empty<Type>();
      }
      return allows.ChildrenType;
    }

    public static string GetClassDisplayName(Type input)
    {
      var display = input.GetObjectCustomAttribute<AllowChildrenAttribute>();
      if (display != null)
      {
        if (!String.IsNullOrEmpty(display.Name))
        {
          return display.Name;
        }
      }
      return input.Name.SpacesFromCamel();
    }
  }

  public static partial class G
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
