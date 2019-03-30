using Microsoft.AspNet.Identity.EntityFramework;
using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SDHC.Common.Entity.Extends
{
}

namespace System
{
  public static class ConvertExtends
  {
    public static ContentPostModel ConvertModelToPost(this object input)
    {
      var model = new ContentPostModel();
      ConvertToIPost(input, model);
      return model;
    }
    public static ModelPostModel ConvertModelToModelPostModel(this object input)
    {
      var model = new ModelPostModel();
      ConvertToIPost(input, model);
      return model;
    }

    public static void ConvertToIPost(object input, IPostModel model)
    {
      var type = input.GetType();
      model.FullType = type.FullName;
      model.ThisAssembly = type.Assembly.FullName;
      var resultProperty = model.GetType().GetProperties().Where(b => b.BaseProperty()).ToList();
      var properties = input.GetType().GetProperties();
      foreach (var p in properties)
      {
        if (p.SkippedProperty())
          continue;
        if (p.BaseProperty())
        {
          var baseP = resultProperty.Where(b => b.Name == p.Name).FirstOrDefault();
          if (baseP == null)
            continue;
          var inputValue = p.GetValue(input);
          if (inputValue == null)
            continue;
          baseP.SetValue(model, inputValue);
          continue;
        }
        var prop = p.GetContentPropertyByPropertyInfo(input);
        if (prop == null)
          continue;
        model.Properties.Add(prop);
      }
      model.Properties = model.Properties.OrderBy(b => b.SortOrder).ToList();
    }

    public static object ConvertToBaseModel(this IPostModel input, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var result = input.ConvertBaseTypeToEnity(out var typeName, out var assemblyName);
      return input.ConvertToBaseModel(result, deleteExistFile, oldFiles, newFiles);
    }
    public static object ConvertToBaseModel(this IPostModel input, object result, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var type = input.FullType;
      var asm = input.ThisAssembly;

      var properties = result.GetType().GetProperties();
      var baseProperty = input.GetType().GetProperties().Where(b => b.BaseProperty()).ToList();
      foreach (var p in properties)
      {
        try
        {
          if (p.BaseProperty())
          {
            var inputBaseProperty = baseProperty.Where(b => b.Name == p.Name).FirstOrDefault();
            if (inputBaseProperty == null)
              continue;
            p.SetValue(result, inputBaseProperty.GetValue(input));
            continue;
          }
          if (p.SkippedProperty())
            continue;

          p.SetPropertyValue(input, result, deleteExistFile, oldFiles, newFiles);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }

      }
      return result;
    }

    public static UserPassModel ConvertUserToPost(this IdentityUser input)
    {
      var model = new UserPassModel();

      var inputType = input.GetType();
      var inputProperty = inputType.GetProperties();
      var userType = typeof(IdentityUser);
      var userProperty = userType.GetProperties();
      foreach (var p in inputProperty)
      {
        if (userProperty.Where(b => b.Name == p.Name).FirstOrDefault() != null)
        {
          try
          {
            p.SetValue(model, p.GetValue(input));
          }
          catch { }
          continue;
        }
        var prop = p.GetContentPropertyByPropertyInfo(input);
        if (prop == null)
          continue;
        model.Properties.Add(prop);
      }
      model.Properties = model.Properties.OrderBy(b => b.SortOrder).ToList();
      return model;
    }
    public static IdentityUser ConvertPostToUser(this UserPassModel input, IdentityUser user, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var inputType = user.GetType();
      var inputProperty = inputType.GetProperties();

      var userType = typeof(IdentityUser);
      var userProperty = userType.GetProperties();

      foreach (var p in inputProperty)
      {
        try
        {
          if (userProperty.Where(b => b.Name == p.Name).FirstOrDefault() != null)
          {
            continue;
          }
          p.SetPropertyValue(input, user, deleteExistFile, oldFiles, newFiles);
        }
        catch { }
      }

      return user;
    }


    public static ContentProperty GetContentPropertyByPropertyInfo(this PropertyInfo p, object input)
    {
      var result = new ContentProperty();
      if (p.GetCustomAttribute<BasePropertyAttribute>() != null)
      {
        result.BaseProperty = true;
      }
      if (p.GetCustomAttribute<IgnoreEditAttribute>() != null)
      {
        result.IgnoreProperty = true;
      }
      if (p.GetCustomAttribute<CustomPropertyAttribute>() != null)
      {
        result.CustomProperty = true;
      }
      result.Key = p.Name;
      var inputType = p.GetObjectCustomAttribute<InputTypeAttribute>();
      if (inputType != null)
      {
        result.SortOrder = inputType.SortOrder;
        result.Required = inputType.Required;
        result.Readonly = inputType.Readonly;
        result.ReadonlyEdit = inputType.ReadonlyEdit;
      }
      var cValue = p.GetValue(input).MyTryConvert(typeof(string));
      if (cValue != null)
        result.Value = (string)cValue;

      Type relatedType = null;
      var inputAttribute = p.GetCustomAttribute<InputTypeAttribute>();
      if (inputAttribute != null)
      {
        result.EditorType = inputAttribute.EditorType;
        result.MultiSelect = inputAttribute.MultiSelect;
        relatedType = inputAttribute.RelatedType;
        result.RangeMax = inputAttribute.RangeMax;
        result.RangeMin = inputAttribute.RangeMin;
        if (inputAttribute.RangeMaxSelf)
        {
          var max = input.MyTryConvert<int>();
          result.RangeMax = max;
        }
      }
      var propertyType = p.GetType();
      var displayAttribute = p.GetCustomAttributes().Where(b => b.GetType().Name == "DisplayAttribute").FirstOrDefault();
      var property = displayAttribute != null ? displayAttribute.GetType().GetProperties().Where(b => b.Name == "Name").FirstOrDefault() : null;
      if (displayAttribute != null && property != null && !String.IsNullOrEmpty((string)property.GetValue(displayAttribute)))
      {
        result.Title = (string)property.GetValue(displayAttribute);
      }
      else
      {
        result.Title = p.Name.SpacesFromCamel();
      }

      var type = p.PropertyType;
      var datetimeType = typeof(DateTime);
      if (result.EditorType == EnumInputType.DropDwon)
      {
        var stringValue = input != null ? p.GetValue(input).MyTryConvert<string>() : "";
        var stringValueList = stringValue.StringValueToList();
        if (result.MultiSelect || stringValueList.Count() > 1)
        {
          result.MultiValue = stringValueList;
          result.Value = "";
        }
        p.SetDropDownSelect(
          (List<DropDownViewModel>)result.SelectItems, relatedType, result.Value, result.MultiValue);
      }
      return result;
      //return new ContentProperty()
      //{
      //  Key = p.Name,
      //  Value = postValue,
      //  EditorType = editorType,
      //  ValueType = propertyType.FullName,
      //  Title = displayTitle,
      //  MultiSelect = multiSelect,
      //  SelectItems = editorType == EnumInputType.DropDwon ? selector : Enumerable.Empty<DropDownViewModel>(),
      //  MultiValue = multiSelect ? postMultiValue : Enumerable.Empty<string>(),
      //};
    }
    public static void SetDropDownSelect(
      this PropertyInfo p, List<DropDownViewModel> selector, Type relatedType, string postValue, IEnumerable<string> postValues = null)
    {
      List<String> values;
      if (postValues != null && postValues.Count() > 0)
      {
        values = postValues.ToList();
      }
      else
      {
        values = !String.IsNullOrEmpty(postValue) ? new List<string>() { postValue } : new List<string>();
      }

      if (relatedType.IsEnum)
      {
        Array enumValues = relatedType.GetEnumValues();
        foreach (var item in enumValues)
        {
          var select = new DropDownViewModel();
          select.Name = item.ToString();
          select.Value = item.ToString();
          select.Select = values.Contains(select.Value);
          selector.Add(select);
        }
      }
      else
      {
        //dropdown Classes
        var isContent = relatedType.GetInterfaces().Any(b => b == typeof(IInt64Key));
        if (isContent)
        {
          var allSelect = ModelManager.Read<IBasicContent>(relatedType, b => true).ToList();

          var selects = new List<DropDownViewModel>();
          foreach (var item in allSelect)
          {
            var select = new DropDownViewModel();
            select.Name = item.DisplayName();
            select.Value = item.Id.ToString();
            select.Select = values.Contains(select.Value);
            selector.Add(select);
          }
        }
        else
        {
          var allSelect = ModelManager.Read<SDHCUser>(relatedType, b => true).ToList();

          var selects = new List<DropDownViewModel>();
          foreach (var item in allSelect)
          {
            var select = new DropDownViewModel();
            select.Name = item.DisplayName();
            select.Value = item.Id;
            select.Select = values.Contains(select.Value);
            selector.Add(select);
          }
        }

      }

    }

    public static void SetPropertyValue(this PropertyInfo p, IPassModel input, object result, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var propertyPost = input.Properties.Find(b => b.Key == p.Name);
      if (propertyPost == null)
      {
        return;
      }
      dynamic value = null;
      var stringValue = !String.IsNullOrEmpty(propertyPost.Value) ? propertyPost.Value : "";
      propertyPost.MultiValue = propertyPost.MultiValue.Where(b => !string.IsNullOrEmpty(b));

      var keyType = p.PropertyType;
      var inputAttribute = p.GetCustomAttribute<InputTypeAttribute>();
      if (inputAttribute != null)
      {
        switch (inputAttribute.EditorType)
        {
          case EnumInputType.DropDwon:
            if (String.IsNullOrEmpty(stringValue) && propertyPost.MultiSelect == false && propertyPost.MultiValue.Count() > 0)
            {
              propertyPost.Value = propertyPost.MultiValue.FirstOrDefault();
            }
            value = GetDropDownValue(inputAttribute, p, propertyPost);
            break;
          case EnumInputType.FileUpload:
            var files = propertyPost;
            if (files.File != null)
            {
              files.File.SaveFile(out var filePath, $"{p.DeclaringType.FullName}_{p.Name}");
              if (deleteExistFile && !string.IsNullOrEmpty(files.Value))
              {
                files.Value.DeleteFile(out var deleted);
              }
              value = filePath;
            }
            else
            {
              value = files.Value;
            }
            break;
          default:
            value = stringValue.MyTryConvert(keyType);
            break;
        }
      }
      else
      {
        //normal type switch and set
        value = stringValue.MyTryConvert(keyType);

      }
      if (value != null)
        p.SetValue(result, value);
    }

    public static object ConvertBaseTypeToEnity(this IPostModel input, out string typeName, out string assemblyName)
    {
      var type = Type.GetType($"{input.FullType},{input.ThisAssembly}");
      typeName = input.FullType;
      assemblyName = input.ThisAssembly;
      if (type == null)
        return null;
      var result = Activator.CreateInstance(type);
      var properties = result.GetType().GetProperties();
      return result;
    }
    public static bool BaseProperty(this PropertyInfo property)
    {
      return property.GetCustomAttribute<BasePropertyAttribute>() != null;
    }
    public static bool SkippedProperty(this PropertyInfo property)
    {
      return property.GetCustomAttribute<IgnoreEditAttribute>() != null;
    }
    public static object GetDropDownValue(InputTypeAttribute inputAttribute, PropertyInfo p, ContentProperty propertyPost)
    {
      if (inputAttribute == null || propertyPost == null)
        return null;
      if (!inputAttribute.MultiSelect)
      {
        return propertyPost.Value.MyTryConvert(p.PropertyType);
      }
      else
      {
        var v = $",{String.Join(",", propertyPost.MultiValue)},";
        return v;
      }

    }


    public static void SetObjectByObject(this object thisObject, object targetObject)
    {
      if (thisObject == null || targetObject == null)
        return;
      var thisType = thisObject.GetType();
      var thisProperty = thisType.GetProperties();
      var targetType = targetObject.GetType();
      var targetProperty = targetType.GetProperties();
      foreach (var p in targetProperty)
      {
        var thisP = thisProperty.Where(b => b.Name == p.Name).FirstOrDefault();
        if (thisP == null)
          continue;
        try
        {
          p.SetValue(targetObject, thisP.GetValue(thisObject));
        }
        catch { }
      }
    }
    public static T ConvertModelToViewModel<T>(this IInt64Key input) where T : BaseViewModel, new()
    {
      var result = new T();
      result.SetViewModel(input);
      return result;
    }
  }

}
