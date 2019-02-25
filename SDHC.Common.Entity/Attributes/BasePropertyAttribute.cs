using SDHC.Common.Entity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Attributes
{
  public class InputTypeAttribute : Attribute
  {
    public EnumInputType EditorType { get; set; } = EnumInputType.Text;
    public bool MultiSelect { get; set; } = false;
    public Type RelatedType { get; set; } = null;
    public int RangeMin { get; set; } = 0;
    public int RangeMax { get; set; } = 100;
    public bool RangeMaxSelf { get; set; } = false;

    public InputTypeAttribute()
    {

    }
  }
  public class BasePropertyAttribute : Attribute
  {
  }
  public class IgnoreEditAttribute : Attribute
  {

  }
  public class HideEditAttribute : Attribute
  {

  }
  public class CustomPropertyAttribute : Attribute
  {
  }

}
