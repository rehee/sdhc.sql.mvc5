using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Extends
{
  public static class ConvertExtends
  {
    public static ContentPostModel ConvertModelToPost(this object input)
    {
      var model = new ContentPostModel();
      var type = input.GetType();
      model.FullType = type.FullName;
      model.ThisAssembly = type.Assembly;
      var resultProperty = model.GetType().GetProperties().Where(b => b.BaseProperty()).ToList();
      var properties = input.GetType().GetProperties();
      foreach (var p in properties)
      {
        if (p.SkippedProperty())
          continue;
        var inputValue = p.GetValue(input);
        if (p.BaseProperty())
        {
          var baseP = resultProperty.Where(b => b.Name == p.Name).FirstOrDefault();
          if (baseP == null)
            continue;
          if (inputValue == null)
            continue;
          baseP.SetValue(model, inputValue);
          continue;
        }
        var prop = p.GetContentPropertyByPropertyInfo(inputValue);
        if (prop == null)
          continue;
        model.Properties.Add(prop);
      }
      return model;
    }

    public static object ConvertToBaseModel(this ContentPostModel input,bool deleteExistFile = true,List<string> oldFiles = null, List<string>newFiles = null)
    {
      var result = input.ConvertBaseTypeToEnity(out var typeName, out var assemblyName);
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

      var cValue = input.MyTryConvert(typeof(string));
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
        if (result.MultiSelect)
        {
          result.MultiValue = input.MyTryConvert<List<string>>();
          result.Value = "";
        }
        //p.SetDropDownSelect(
        //  (List<DropDownViewModel>)result.SelectItems, relatedType, result.Value, result.MultiValue);
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

    public static void SetPropertyValue(this PropertyInfo p, IPassModel input, object result, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var propertyPost = input.Properties.Find(b => b.Key == p.Name);
      if (propertyPost == null)
      {
        return;
      }
      dynamic value = null;
      var stringValue = !String.IsNullOrEmpty(propertyPost.Value) ? propertyPost.Value : "";
      var keyType = p.PropertyType;
      var inputAttribute = p.GetCustomAttribute<InputTypeAttribute>();
      if (inputAttribute != null)
      {
        switch (inputAttribute.EditorType)
        {
          case EnumInputType.DropDwon:
            //value = GetDropDownValue(inputAttribute, p, propertyPost);
            break;
          case EnumInputType.FileUpload:
            var files = propertyPost;
            if (files.File != null)
            {
              files.File.SaveFile(out var filePath, $"{p.DeclaringType.FullName}_{p.Name}");
              if (deleteExistFile)
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

    public static object ConvertBaseTypeToEnity(this ContentPostModel input, out string typeName, out Assembly assemblyName)
    {
      var type = Type.GetType(input.FullType);
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
  }

}
