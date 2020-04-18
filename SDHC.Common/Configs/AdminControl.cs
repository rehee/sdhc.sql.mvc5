using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Configs
{
  public class AdminControl
  {
    public IEnumerable<AdminControlSetting> Setting { get; set; }
  }

  public class AdminControlSetting
  {
    public string Controller { get; set; }
    public IEnumerable<string> Actions { get; set; }
  }
}
