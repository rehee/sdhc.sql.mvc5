using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
  public static class MyReflect
  {
    /// <summary>
    /// Get All CustomAttributeData from object as IEnumerable
    /// </summary>
    /// <param name="o">Object you want get CustomAttribute</param>
    /// <param name="getField">switch check fields attribute if it's Enum</param>
    /// <returns></returns>
    public static IEnumerable<CustomAttributeData> GetObjectCustomAttribute(this Object o, bool getField = true)
    {
      Type type = o.GetType().GetRealType();
      var customeAttribute = type.CustomAttributes;
      if (type.IsEnum && getField)
      {
        var info = o.GetType().GetRealType().GetField(o.ToString());
        customeAttribute = info.CustomAttributes;
      }
      return customeAttribute;
    }
    /// <summary>
    /// Retrive CustomAttribute from input object or projerty
    /// </summary>
    /// <typeparam name="T">CustomAttribute class try to Retrive</typeparam>
    /// <param name="o">Property or attribute try to get value</param>
    /// <param name="getField">switch check field attribute if the object is Enum. </param>
    /// <returns></returns>
    public static T GetObjectCustomAttribute<T>(this Object o, bool getField = true) where T : Attribute, new()
    {
      try
      {
        Type type = o.GetType().GetRealType();
        if (type.IsEnum && getField)
        {
          var info = o.GetType().GetRealType().GetField(o.ToString());
          return info.GetCustomAttribute<T>();
        }
        var t1 = o.GetType().GetRealType().GetCustomAttribute<T>();
        var t2 = o.GetType().GetRealType().GetCustomAttribute(typeof(T));
        return (T)t2;
      }
      catch { return default(T); }
    }
    public static T GetObjectCustomAttribute<T>(this PropertyInfo p, bool getField = true) where T : Attribute, new()
    {
      try
      {
        if (p.PropertyType.IsEnum && getField)
        {
          var info = p.PropertyType.GetType().GetRealType().GetField(p.Name);
          return info.GetCustomAttribute<T>();
        }
        var t1 = p.GetCustomAttribute<T>(false);
        return t1;
      }
      catch { return default(T); }
    }
    public static T GetObjectCustomAttribute<T>(this Type p, bool getField = true, bool inhe = false) where T : Attribute, new()
    {
      try
      {
        if (p.IsEnum && getField)
        {
          var info = p.GetType().GetRealType().GetField(p.Name);
          return info.GetCustomAttribute<T>(inhe);
        }
        //var t1 = p.CustomAttributes.Where(b => b.AttributeType == typeof(T)).FirstOrDefault();
        //if (t1 == null)
        //{
        //  return default(T);
        //}
        var t1 = p.GetCustomAttribute<T>(inhe);
        return t1;
      }
      catch { return default(T); }
    }

    /// <summary>
    /// get DisplayAttribute from input object
    /// </summary>
    /// <param name="o">object to get DisplayAttribute</param>
    /// <param name="getField"></param>
    /// <returns>switch check field attribute if the object is Enum. </returns>
    public static DisplayAttribute GetObjectDisplayAttribute(this Object o, bool getField = true)
    {
      return o.GetObjectCustomAttribute<DisplayAttribute>(getField);
    }
    /// <summary>
    /// Try Get CustomAttribute field by Attribute Name And Field Name return null if there no custom attribute or field
    /// </summary>
    /// <param name="o">Object to heck custom attribute property</param>
    /// <param name="attributeName">the attribute checked</param>
    /// <param name="name">attribute property name</param>
    /// <param name="getField">switch check field attribute if the object is Enum. </param>
    /// <returns></returns>
    public static object GetObjectAttribute(this Object o, string attributeName, string name, bool getField = true)
    {
      Type type = o.GetType().GetRealType();
      var customeAttribute = type.CustomAttributes.Where(b => b.AttributeType.Name == attributeName).FirstOrDefault();
      if (type.IsEnum && getField)
      {
        var info = o.GetType().GetRealType().GetField(o.ToString());
        customeAttribute = info.CustomAttributes.Where(b => b.AttributeType.Name == attributeName).FirstOrDefault();
      }
      if (customeAttribute == null)
        return null;
      var nameAttribute = customeAttribute.NamedArguments.Where(b => b.MemberName == name).FirstOrDefault();
      if (nameAttribute == null)
        return null;
      return nameAttribute.TypedValue.Value;
    }
    /// <summary>
    /// Try Get Generic CustomAttribute field by Attribute Name And Field Name return null if there no custom attribute or field
    /// </summary>
    /// <typeparam name="T">Generic of the result</typeparam>
    /// <param name="o">Object to heck custom attribute property</param>
    /// <param name="attributeName">the attribute checked</param>
    /// <param name="name">attribute property name</param>
    /// <param name="getField">switch check field attribute if the object is Enum. </param>
    /// <returns></returns>
    /// <returns>switch check field attribute if the object is Enum. </returns>
    public static T GetObjectAttribute<T>(this Object o, string attributeName, string name, bool getField = true)
    {
      var customeAttribute = o.GetObjectCustomAttribute(getField)
        .Where(b => b.AttributeType.Name == attributeName).FirstOrDefault();
      if (customeAttribute == null)
        return default(T);
      var names = customeAttribute.NamedArguments.Where(b => b.MemberName == name).FirstOrDefault();
      if (names == null || names.TypedValue == null || names.TypedValue.Value == null)
        return default(T);
      return (T)names.TypedValue.Value;
    }
    /// <summary>
    /// Try Get List Generic CustomAttribute field by Attribute Name And Field Name return null if there no custom attribute or field
    /// </summary>
    /// <typeparam name="T">Generic of the result</typeparam>
    /// <param name="o">Object to heck custom attribute property</param>
    /// <param name="attributeName">the attribute checked</param>
    /// <param name="name">attribute property name</param>
    /// <param name="getField">switch check field attribute if the object is Enum. </param>
    /// <returns></returns>
    public static IEnumerable<T> GetObjectAttributes<T>(this Object o, string attributeName, string name, bool getField = true)
    {
      var customeAttribute = o.GetObjectCustomAttribute(getField)
        .Where(b => b.AttributeType.Name == attributeName).FirstOrDefault();
      if (customeAttribute == null)
        return Enumerable.Empty<T>();
      var names = customeAttribute.NamedArguments.Where(b => b.MemberName == name).FirstOrDefault();
      if (names == null || names.TypedValue == null || names.TypedValue.Value == null)
        return Enumerable.Empty<T>();
      var types = ((System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)names.TypedValue.Value)
            .Select(b => (T)b.Value).ToArray();
      return types;
    }
    /// <summary>
    /// Check the type is IEnumerable
    /// </summary>
    /// <param name="type">type to check</param>
    /// <returns></returns>
    public static bool IsIEnumerable(this Type type)
    {
      return typeof(IList).IsAssignableFrom(type);
    }
    /// <summary>
    /// Get Element Type from Ienumerable Type
    /// </summary>
    /// <param name="type">IEnumerable type to check</param>
    /// <returns></returns>
    public static Type GetIElementType(this Type type)
    {
      try
      {
        if (!type.IsIEnumerable())
          return null;
        return type.GetElementType() == null ? type.GetGenericArguments()[0] : type.GetElementType();
      }
      catch
      {
        return null;
      }
    }
    public static void SetPropertyValue(this PropertyInfo property, object result, object value)
    {
      if (value == null)
        return;
      var targetType = property.PropertyType;
      var valueType = value.GetType().GetRealType();
      if (targetType == valueType)
      {
        property.SetValue(result, value);
        return;
      }
      var convertResult = MyConvertExtend.MyTryConvert(value, targetType);
      property.SetValue(result, convertResult);
    }

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

    public static string GetPropertyLabelByKey(this Type input, string key)
    {
      var p = input.GetProperties().Where(b => b.Name == key).FirstOrDefault();
      if (p == null)
        return key;
      var display = p.GetObjectCustomAttribute<DisplayAttribute>();
      if (display == null)
        return key.SpacesFromCamel();
      return display.Name.SpacesFromCamel();
    }
  }
}
