using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
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
