using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class ViewModelExtends
  {
    public static T ConvertModelToViewModel<T>(this IInt64Key input) where T : BaseViewModel, new()
    {
      var result = new T();
      result.SetViewModel(input);
      return result;
    }
  }
}
