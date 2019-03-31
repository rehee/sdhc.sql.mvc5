﻿using SDHC.Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IPassModel
  {
    List<ContentProperty> Properties { get; set; }
  }

  public class ContentPostViewModel
  {
    public ContentPostViewModel(ContentPostModel model = null)
    {
      if (model != null)
        Model = model;
    }
    public ContentPostModel Model { get; set; }
    public string ViewPath
    {
      get
      {
        if (Model == null)
          return "";
        var t = Type.GetType($"{Model.FullType},{Model.ThisAssembly}");
        var path = String.IsNullOrEmpty(ContentManager.ContentViewPath) ? "" : $"/{ContentManager.ContentViewPath}";
        return $"~/Views{path}/{t.Name}.cshtml";
      }
    }
  }


  public class ContentPostModel : IPostModel
  {
    [BaseProperty]
    public long Id { get; set; }
    [BaseProperty]
    public long? ParentId { get; set; }
    [BaseProperty]
    public string FullType { get; set; }
    [BaseProperty]
    public string ThisAssembly { get; set; }
    [BaseProperty]
    public string Title { get; set; }
    [BaseProperty]
    public DateTime? CreateTime { get; set; }
    [BaseProperty]
    public long DisplayOrder { get; set; }

    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }

  public class ModelPostModel : IPostModel
  {
    [BaseProperty]
    public long Id { get; set; }
    [BaseProperty]
    public string FullType { get; set; }
    [BaseProperty]
    public string ThisAssembly { get; set; }

    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }

  public interface IPostModel : IPassModel, IInt64Key
  {
    string FullType { get; set; }
    string ThisAssembly { get; set; }
  }
  public class ContentPropertyIndex
  {
    public ContentProperty Property { get; set; }
    public int Index { get; set; }
    public ContentPropertyIndex(ContentProperty property, int index)
    {
      Property = property;
      Index = index;
    }
  }

}
