using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public class InputTypeAttribute : Attribute
  {
    public EnumInputType EditorType { get; set; } = EnumInputType.Text;
    public bool MultiSelect { get; set; } = false;
    public Type RelatedType { get; set; } = null;
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

    public InputTypeAttribute()
    {

    }
  }

}
