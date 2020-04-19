using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SDHC.Common.Entity.Models
{
  public class ContentProperty : IInputCommon
  {
    public string Key { get; set; } = "";
    public string Value { get; set; } = "";
    public IEnumerable<string> MultiValue { get; set; } = new List<string>();
    public string ValueType { get; set; } = "";
    public string Title { get; set; } = "";

    public EnumInputType EditorType { get; set; } = EnumInputType.Text;
    public bool MultiSelect { get; set; } = false;
    public IEnumerable<DropDownViewModel> SelectItems { get; set; } = new List<DropDownViewModel>();
    public virtual object File
    {
      get
      {
        return FileCore != null ? FileCore : FileMvc;
      }
      set
      {

      }
    }
    public object FileMvc { get; set; }
    public IFormFile FileCore { get; set; }
    public bool RemoveFile { get; set; } = false;
    public bool BaseProperty { get; set; } = false;
    public bool IgnoreProperty { get; set; } = false;
    public bool CustomProperty { get; set; } = false;
    public int SortOrder { get; set; } = 0;
    public int RangeMin { get; set; } = 0;
    public int RangeMax { get; set; } = 100;
    public bool Required { get; set; }
    public bool Readonly { get; set; }
    public bool ReadonlyEdit { get; set; }
    public int InputWidth { get; set; }
    public bool NewLine { get; set; }
    public bool NewLineAfter { get; set; }
    public bool HasRangeMin { get; set; }
    public bool HasRangeMax { get; set; }
    public Type RelatedType { get; set; }
    public IEnumerable<ISharedLink> SharedLinks { get; set; }
    public bool IsSingleRecord { get; set; } = false;
  }
  public class DropDownViewModel
  {
    public string Name { get; set; } = "";
    public string Value { get; set; }
    public bool Select { get; set; } = false;
  }
}
