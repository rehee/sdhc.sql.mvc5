using System;
using System.Collections.Generic;
using System.Linq;

namespace SDHC.Common.Entity.Models
{
  public interface IPassModel
  {
    List<ContentProperty> Properties { get; set; }
  }
  public interface IPostModel : IPassModel, IInt64Key
  {
    string FullType { get; set; }
    string ThisAssembly { get; set; }
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
    [BaseProperty]
    public string ViewPath { get; set; }
    public List<ContentProperty> Properties { get; set; } = PassModeConvert.NewContentPropertyList();
  }

  public abstract class IPostModeltViewModal<T> where T : IPostModel
  {
    public int Lang { get; }
    private Dictionary<string, ContentPropertyIndex> list { get; set; } = new Dictionary<string, ContentPropertyIndex>();
    public T Model { get; }
    public IPostModeltViewModal(T model, string outerKey = null, int? lang = null)
    {
      Model = model;
      var index = 0;
      OutIndex = outerKey;
      if (lang == null && model is ISharedContent)
      {
        lang = (model as ISharedContent).Lang;
      }
      Lang = lang ?? 0;
      foreach (var property in Model.Properties)
      {
        list.Add(property.Key, new ContentPropertyIndex(property, index, outerKey, lang, Model.Id, model.FullType, model.ThisAssembly));
        index++;
      }
    }
    public IEnumerable<ContentPropertyIndex> ContentPropertyIndexs => list.Values;
    public ContentPropertyIndex GetContentPropertyByName(string key)
    {
      if (list.ContainsKey(key))
        return list[key];
      return null;
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
    public string GetModelNameByName(string key)
    {
      var p = GetContentPropertyByName(key);
      return p != null ? $"modal_{p.OuterNameNoMark}" : null;
    }
    public string GetModelRefreshByName(string key)
    {
      var p = GetContentPropertyByName(key);
      return p != null ? $"modal_{p.OuterNameNoMark}_refresh" : null;
    }
    public string GetModelReviewByName(string key)
    {
      var p = GetContentPropertyByName(key);
      return p != null ? $"modal_{p.OuterNameNoMark} review" : null;
    }

    public string OutIndex { get; }
    public string OutMakr => String.IsNullOrEmpty(OutIndex) ? $"" : $"{OutIndex}.";
  }

  public class ContentViewModal : IPostModeltViewModal<ContentPostModel>
  {
    public ContentViewModal(ContentPostModel model, string outerKey = null) : base(model, outerKey, model.Lang)
    {
    }
    public string ViewPath => Model.ViewPath;
  }
  public class ModelViewModal : IPostModeltViewModal<ModelPostModel>
  {
    public ModelViewModal(ModelPostModel model, string outerKey = null) : base(model, outerKey)
    {
    }
  }

  public class ContentViewModelSummary
  {
    public ContentViewModal ContentModel { get; set; }
    public IEnumerable<ModelViewModal> Models { get; set; }
  }

  public class ContentViewModelSummaryPost
  {
    public ContentPostModel ContentModel { get; set; }
    public IEnumerable<ModelPostModel> Models { get; set; }
  }

  public class ContentPropertyIndex
  {
    public ContentProperty Property { get; set; }
    public int Index { get; set; }
    public string RandomIndex { get; }
    public string InputName { get; }
    public string FileName { get; }
    public bool IsFile => Property != null ? Property.EditorType == EnumInputType.FileUpload : false;
    public string OuterName => $"{OutMakr}{(IsFile ? FileName : InputName)}";
    public string OuterNameNoMark
    {
      get
      {
        var s = OuterName.Split('[', ']', '.');
        return String.Join("_", s);
      }
    }
    public string OutIndex { get; }
    public string OutMakr => String.IsNullOrEmpty(OutIndex) ? $"" : $"{OutIndex}.";
    public int? Lang { get; }
    public long? ModelId { get; }
    public string FullType { get; }
    public string AssemblyName { get; }
    public ContentPropertyIndex(ContentProperty property, int index, string outIndex = null, int? lang = null, long? modelId = null,
      string fullType = null, string assemblyName = null

      )
    {
      Property = property;
      Index = index;
      OutIndex = outIndex;
      RandomIndex = Guid.NewGuid().ToString().Replace('-', '_');
      var valueName = "Value";
      if (Property.MultiSelect)
      {
        valueName = "MultiValue";
      }
      InputName = "Properties[" + Index.ToString() + "]." + valueName;
      FileName = "Properties[" + Index.ToString() + "].FileCore";
      Lang = lang;
      ModelId = modelId;
      FullType = fullType;
      AssemblyName = assemblyName;
    }
  }

}
