using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
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
}
