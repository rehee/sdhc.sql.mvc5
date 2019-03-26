using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public class LanguageManager
  {
    public static string LanguageKey { get; set; } = "Lang";

    public static Func<int> GetLang { get; set; }

    public static Action<int> SetLang { get; set; }
  }
}
