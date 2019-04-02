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
  public class ListItemAttribute : Attribute
  {
    public ListItemAttribute()
    {

    }
    public string[] KeyAndDisplayNames { get; set; }
  }
  public class AllowChildrenAttribute : Attribute
  {
    public string Name { get; set; }
    public Type[] ChildrenType { get; set; }
    public string[] CreateRoles { get; set; } 
    public string[] ReadRoles { get; set; } 
    public string[] EditRoles { get; set; } 
    public string[] SortRoles { get; set; } 
    public string[] DeleteRoles { get; set; }
    public string[] TableList { get; set; }
    public bool SingleRecord { get; set; } = false;
    public int DisplayOrder { get; set; } = 0;
    public bool DisableDelete { get; set; }
    public AllowChildrenAttribute()
    {

    }
  }
}
