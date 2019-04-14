using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System
{
  public static class MyConvertExtend
  {
    public static Dictionary<Type, Func<string, object>> MyStringObjectConvertDictionary { get; set; } =
      new Dictionary<Type, Func<string, object>>()
      {
        [typeof(DateTime)] = b =>
        {
          if (string.IsNullOrEmpty(b))
            return null;
          var t = DateTime.TryParse(b, out var s);
          if (!t)
            return null;
          return s.ToUniversalTime();
        },
      };
    public static Dictionary<Type, Func<object, string>> MyObjectStringConvertDictionary { get; set; } =
      new Dictionary<Type, Func<object, string>>()
      {
        [typeof(DateTime)] = b =>
        {
          if (b.GetType().GetRealType() != typeof(DateTime))
            return "";
          var date = (DateTime)b;
          if (String.IsNullOrEmpty(C.DateConvertFormats))
            return date.ToString();
          return date.ToString(C.DateConvertFormats);
        },
      };
    public static object MyTryConvert(this string value, Type type)
    {
      if (MyStringObjectConvertDictionary.ContainsKey(type))
        return MyStringObjectConvertDictionary[type](value);
      var convertMethod = type.GetMethods().Where(b => b.Name == "" && b.GetParameters().Count() == 2).FirstOrDefault();
      if (convertMethod != null)
      {
        var parames = new object[] { value, null };
        var result = convertMethod.Invoke(null, parames);
        return parames[1];
      }
      try
      {
        if (type.IsEnum)
        {
          var values = type.GetEnumValues();
          foreach (var v in values)
          {
            if (v.ToString() == value)
              return v;
          }
        }
        return value.ChangeTypeNull(type);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return null;
      }
    }
    public static object MyTryConvert(this object value, Type type)
    {
      if (value == null)
        return null;
      var valueType = value.GetType().GetRealType();
      if (valueType == typeof(string))
        return MyTryConvert((string)value, type);
      if (type.IsEnum)
        return value.MyTryConvertIEnumable(type);
      if (type == typeof(string))
      {
        if (MyObjectStringConvertDictionary.ContainsKey(valueType))
          return MyObjectStringConvertDictionary[valueType](value);
        return value.ToString();
      }
      try
      {
        return value.ChangeTypeNull(type);
      }
      catch
      {
        return null;
      }
    }
    public static T MyTryConvert<T>(this object value)
    {
      if (value == null)
        return default(T);
      var targetType = typeof(T);
      if (targetType.IsEnum)
      {
        var valueType = value.GetType().GetRealType();
        if (!valueType.IsEnum)
          return default(T);
        var targetElementType = targetType.GetElementType();
        var valueElementType = valueType.GetElementType();
        var listType = typeof(List<>);
        var constructedListType = listType.MakeGenericType(targetElementType);
        var instance = (IList)Activator.CreateInstance(constructedListType);
        foreach (var item in (IEnumerable)value)
        {
          if (targetElementType == valueElementType)
          {
            instance.Add(item);
          }
          else
          {
            var elementConvert = item.MyTryConvert(targetElementType);
            if (elementConvert != null)
              instance.Add(elementConvert);
          }
        }
        if (!targetType.IsArray)
          return (T)instance;
        var array = new object[instance.Count];
        instance.CopyTo(array, 0);
        return (T)((object)array);

      }
      var resultConvert = value.MyTryConvert(targetType);
      if (resultConvert == null)
        return default(T);
      return (T)resultConvert;
    }
    public static object MyTryConvertIEnumable(this object value, Type type)
    {
      if (value == null)
        return null;
      var valueType = value.GetType().GetRealType();
      var elementType = type.GetElementType();
      var valueElementType = valueType.GetElementType();
      if (elementType != null)
      {
        var listType = typeof(List<>);
        var constructedListType = listType.MakeGenericType(elementType);
        var instance = (IList)Activator.CreateInstance(constructedListType);
        if (valueElementType != null)
        {
          foreach (var v in ((IEnumerable)value))
          {
            var convertV = v.MyTryConvert(elementType);
            if (convertV == null)
              continue;
            instance.Add(convertV);
          }
        }
        if (type.IsArray)
        {
          if (instance.Count == 0)
            return null;
          Array array = new object[instance.Count];
          instance.CopyTo(array, 0);
          return array;
        }
        return instance;
      }
      return null;
    }
    public static object ChangeTypeNull(this object value, Type type)
    {
      try
      {
        if (type.Name.Contains("Nullable"))
        {
          return Convert.ChangeType(value, type.GenericTypeArguments.FirstOrDefault());
        }
        return Convert.ChangeType(value, type);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return null;
    }
  }
}
