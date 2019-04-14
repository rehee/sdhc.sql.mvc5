using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class MyReflectExtends
  {
    public static string GetPropertyByKey(this object input, string key)
    {
      return String.Join(",", GetPropertyEnumerableByKey<string>(input, key));
    }
    public static T GetPropertyByKey<T>(this object input, string key)
    {
      return GetPropertyEnumerableByKey<T>(input, key).FirstOrDefault();
    }
    public static IEnumerable<T> GetPropertyEnumerableByKey<T>(this object input, string key)
    {
      var listResult = new List<T>();
      if (input == null)
      {
        return listResult;
      }
      var type = input.GetType().GetRealType();
      var p = type.GetProperties().Where(b => string.Equals(b.Name, key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
      if (p == null)
      {
        return listResult;
      }
      var inputType = p.GetObjectCustomAttribute<InputTypeAttribute>();
      var value = p.GetValue(input);
      if (inputType != null && inputType.RelatedType != null && !inputType.RelatedType.IsEnum)
      {
        var longKey = inputType.RelatedType.GetInterfaces().Any(b => b == typeof(IInt64Key));
        var values = value.Text().StringValueToList();
        if (inputType.MultiSelect)
        {
          var types = inputType.RelatedType.GetInterfaces().ToList();
          types.Add(inputType.RelatedType);
          IEnumerable<object> objList;
          if (longKey)
          {
            var longValues = values.Select(b => b.MyTryConvert<long>()).ToList();
            objList = ModelManager.Read<IBasicContent>(inputType.RelatedType, b => longValues.Contains(b.Id)).ToList();
          }
          else
          {
            if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
              objList = ModelManager.Read<SDHCUser>(inputType.RelatedType, b => values.Contains(b.Id)).ToList().Select(b => (b as IDisplayName).DisplayName()).ToList();
            }
            else
            {
              objList = ModelManager.Read<SDHCUser>(inputType.RelatedType, b => values.Contains(b.Id)).ToList();
            }
          }
          if (types.Contains(typeof(T)))
          {
            return objList.Select(b => b.MyTryConvert<T>()).ToList();
          }
          var isDisplayName = inputType.RelatedType.GetInterfaces().Any(b => b.Name == typeof(IDisplayName).Name);
          if (isDisplayName)
          {
            value = String.Join(",", objList.Select(b => (b as IDisplayName).DisplayName()));
          }
          else
          {
            value = String.Join(",", objList.ToString());
          }
        }
        else
        {
          var firstValue = values.FirstOrDefault();
          if (longKey)
          {
            var longValue = firstValue.MyTryConvert<long>();
            value = ModelManager.Read<IBasicContent>(inputType.RelatedType, b => b.Id == longValue).ToList().Select(b => b.MyTryConvert<T>()).FirstOrDefault();
          }
          else
          {
            if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
              value = ModelManager.Read<SDHCUser>(inputType.RelatedType, b => values.Contains(b.Id)).ToList().Select(b => (b as IDisplayName).DisplayName()).ToList().FirstOrDefault();
            }
            else
            {
              value = ModelManager.Read<SDHCUser>(inputType.RelatedType, b => values.Contains(b.Id)).ToList().Select(b => b.MyTryConvert<T>()).ToList().FirstOrDefault();
            }
          }
        }
      }
      if (value == null)
      {
        return listResult;
      }
      listResult.Add(value.MyTryConvert<T>());
      return listResult;
    }

  }
}
