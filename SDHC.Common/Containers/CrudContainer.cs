using SDHC.Common.Cruds;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class CrudContainer
  {
    public static ICrud Crud { get; set; }
    public static ICrudModel CrudModel { get; set; }
    public static ICrudContent CrudContent { get; set; }
    public static Type BaseUser { get; set; }
  }
}
