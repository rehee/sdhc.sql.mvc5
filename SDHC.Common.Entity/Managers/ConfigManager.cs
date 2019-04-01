using Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static partial class G
  {
    public static Func<string,string> GetSetting { get; set; }
    public static Func<ApplicationUserManager> UserManager { get; set; }
    public static Func<ApplicationSignInManager> SignManager { get; set; }
    public static Func<ApplicationRoleManager> RoleManager { get; set; }

    [Config]
    public static bool UserNameIsNotEmail { get; set; } = true;
    [Config]
    public static int DefaultLanguage { get; set; } = 0;
    [Config]
    public static string ContentViewPath { get; set; } = "";
    [Config]
    public static string ContentPageUrl { get; set; } = "";
  }
  
}
