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
  public class ContentPostModel : IPassModel
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
