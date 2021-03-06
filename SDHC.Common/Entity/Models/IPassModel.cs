﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SDHC.Common.Entity.Models
{
  public interface IPassModel
  {
    List<ContentProperty> Properties { get; set; }
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

public string PostUrl { get; set; }
    public string PostReturnUrl { get; set; }
    public bool IsPostAjax { get; set; }
    public string PostBeforeMethod { get; set; }
    public string PostResponseMethod { get; set; }
    public string PostFormId { get; set; } = Guid.NewGuid().ToString().Replace('-', '_');
    public string PostFormTitle { get; set; }

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
