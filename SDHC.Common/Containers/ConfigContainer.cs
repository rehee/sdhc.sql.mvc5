using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class ConfigContainer
  {
    public static SystemConfig Systems { get; set; }
    public static Func<string, string> GetSetting { get; set; }
    
  }
}
