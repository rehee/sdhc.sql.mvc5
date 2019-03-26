using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static partial class G
  {
    public static string ImagePath(this string path)
    {
      if (string.IsNullOrEmpty(path))
      {
        return path;
      }
      return path.Replace('\\','/');
    }
  }
}
