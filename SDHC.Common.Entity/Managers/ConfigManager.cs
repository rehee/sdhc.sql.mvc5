using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SDHC.Common.Entity.Models;
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
    public static Func<string, string> GetSetting { get; set; }
    public static Func<IOwinContext> GetContext { get; set; }
    //public static Func<ApplicationUserManager> UserManager { get; set; }
    //public static Func<ApplicationSignInManager> SignManager { get; set; }
    public static object UserManagerContext { get; set; } 
    public static ApplicationUserManager UserManager()
    {
      return OwinContextExtensions.Get<ApplicationUserManager>(GetContext());
    }
    public static ApplicationSignInManager SignManager()
    {
      return OwinContextExtensions.Get<ApplicationSignInManager>(GetContext());
    }
    public static Func<ApplicationRoleManager> RoleManager { get; set; }
  }

}
