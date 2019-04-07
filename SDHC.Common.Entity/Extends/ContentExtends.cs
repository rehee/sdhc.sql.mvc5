using SDHC.Common.Entity.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ContentExtends
  {
    public static string GetValueByKey(this object input, string key)
    {
      if (input == null)
      {
        return "";
      }
      var type = input.GetType().GetRealType();
      foreach (var p in type.GetProperties())
      {
        if(p.Name != key)
        {
          continue;
        }
        var value = p.GetValue(input);
        return value.MyTryConvert<string>();
      }
      return "";
    }

  }
}
