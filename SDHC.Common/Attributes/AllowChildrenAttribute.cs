using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
  public class AllowChildrenAttribute : Attribute
  {
    public string Name { get; set; }
    public Type[] ChildrenType { get; set; } = new Type[0];
    public string[] CreateRoles { get; set; } = new String[0];
    public string[] ReadRoles { get; set; } = new String[0];
    public string[] EditRoles { get; set; } = new String[0];
    public string[] SortRoles { get; set; } = new String[0];
    public string[] DeleteRoles { get; set; } = new String[0];
    public string[] TableList { get; set; } = new String[0];
    public EnumTablePageSize TableSize { get; set; } = EnumTablePageSize.L0;
    public bool SingleRecord { get; set; } = false;
    public int DisplayOrder { get; set; } = 0;
    public bool DisableDelete { get; set; }
    public AllowChildrenAttribute()
    {

    }
  }

  public enum EnumTablePageSize
  {
    L0 = 0,
    L10 = 10,
    L25 = 25,
    L50 = 50,
    L100 = 100,
  }

}
