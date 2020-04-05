using System;
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
    [BaseProperty]
    public int? Lang { get; set; }
    public List<ContentProperty> Properties { get; set; } = PassModeConvert.NewContentPropertyList();
  }
  public class ContentViewModal
  {
    public ContentPostModel Model { get; }
    public ContentViewModal(ContentPostModel model)
    {
      Model = model;
    }
    private List<ContentPropertyIndex> list { get; set; }
    public IEnumerable<ContentPropertyIndex> ContentPropertyIndexs
    {
      get
      {
        if (list != null)
          return list;
        var index = 0;
        list = new List<ContentPropertyIndex>();
        foreach (var property in Model.Properties)
        {
          list.Add(new ContentPropertyIndex(property, index));
          index++;
        }
        return list;

      }
    }

    public ContentPropertyIndex GetContentPropertyByName(string key)
    {
      return ContentPropertyIndexs.FirstOrDefault(b => b.Property.Key == key);
    }
    public string GetModelValueByName(string key)
    {
      var p = GetContentPropertyByName(key);
      if (p == null)
        return null;
      switch (p.Property.EditorType)
      {
        case EnumInputType.DropDwon:
          return String.Join(",", p.Property.SelectItems.Where(b => b.Select).Select(b => b.Value));
        case EnumInputType.FileUpload:
          var path = p.Property.Value.ImagePath().GetUrlPath();
          return path;
      }
      return p.Property.Value;
    }
    public string GetModalNameByName(string key)
    {
      var p = GetContentPropertyByName(key);
      return p != null ? $"modal_{p.OuterNameNoMark}" : null;
    }
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
    public string RandomIndex { get; }
    public string InputName { get; }
    public string FileName { get; }
    public bool IsFile => Property != null ? Property.EditorType == EnumInputType.FileUpload : false;
    public string OuterName => IsFile ? FileName : InputName;
    public string OuterNameNoMark => String.Join("", OuterName.Split('[', ']', '.'));
    public ContentPropertyIndex(ContentProperty property, int index)
    {
      Property = property;
      Index = index;
      RandomIndex = Guid.NewGuid().ToString().Replace('-', '_');
      var valueName = "Value";
      if (Property.MultiSelect)
      {
        valueName = "MultiValue";
      }
      InputName = "Properties[" + Index.ToString() + "]." + valueName;
      FileName = "Properties[" + Index.ToString() + "].FileCore";
    }
  }

}
