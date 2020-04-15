using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public class InputTypeAttribute : Attribute, IInputCommon
  {
    public Type RelatedType { get; set; } = null;

    public EnumInputType EditorType { get; set; } = EnumInputType.Text;
    public bool MultiSelect { get; set; } = false;
    public int RangeMin { get; set; } = 0;
    public int RangeMax { get; set; } = 100;

    public bool RangeMaxSelf { get; set; } = false;

    public int SortOrder { get; set; } = 0;
    public bool Required { get; set; }
    public bool Readonly { get; set; }
    public bool ReadonlyEdit { get; set; }
    public int InputWidth { get; set; }
    public bool NewLine { get; set; }
    public bool NewLineAfter { get; set; }
    public bool HasRangeMin { get; set; }
    public bool HasRangeMax { get; set; }
    public InputTypeAttribute()
    {

    }
  }
  public interface IInputCommon
  {
    EnumInputType EditorType { get; set; }
    Type RelatedType { get; set; }
    bool MultiSelect { get; set; }
    bool HasRangeMin { get; set; }
    bool HasRangeMax { get; set; }
    int RangeMin { get; set; }
    int RangeMax { get; set; }
    int SortOrder { get; set; }
    bool Required { get; set; }
    bool Readonly { get; set; }
    bool ReadonlyEdit { get; set; }
    int InputWidth { get; set; }
    bool NewLine { get; set; }
    bool NewLineAfter { get; set; }
  }

}
