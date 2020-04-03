using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Attributes
{
  public class FileReadAttribute : Attribute
  {
    public string ReadRoles { get; set; }
  }
}
