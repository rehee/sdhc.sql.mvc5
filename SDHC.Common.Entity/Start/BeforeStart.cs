using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
  public static class BeforeStart
  {
    public static void Init(
      Func<string, string> getConfig,
      Type E,
      Func<string, object> getSession,
      Action<string, object> setSession,
      Func<IOwinContext> getContext)
    {
      G.GetSetting = getConfig;
      var eProperty = E.GetProperties();
      foreach (var p in eProperty)
      {
        switch (p.Name)
        {
          case "UserManager":
            Func<ApplicationUserManager> UserManager = () => OwinContextExtensions.Get<ApplicationUserManager>(getContext());
            p.SetValue(null, UserManager);
            continue;
          case "SignManager":
            Func<ApplicationSignInManager> SignManager = () => OwinContextExtensions.Get<ApplicationSignInManager>(getContext());
            p.SetValue(null, SignManager);
            continue;
          case "RoleManager":
            Func<ApplicationRoleManager> RoleManager = () => OwinContextExtensions.Get<ApplicationRoleManager>(getContext());
            p.SetValue(null, RoleManager);
            continue;
        }
        var config = p.GetObjectCustomAttribute<ConfigAttribute>();
        if (config == null)
        {
          continue;
        }
        try
        {
          var value = G.GetSetting(p.Name);
          switch (p.Name)
          {
            case "ContentViewPath":
              ContentManager.ContentViewPath = value == null ? "" : value.MyTryConvert<string>();
              break;
            case "ContentPageUrl":
              ContentManager.ContentPageUrl = value == null ? "" : value.MyTryConvert<string>();
              break;

          }
          var ptype = p.PropertyType;
          if (ptype == typeof(string))
          {
            p.SetValue(null, value);
            continue;
          }
          p.SetValue(null, value.MyTryConvert(ptype));
        }
        catch { }
      }

      LanguageManager.GetLang = () =>
      {
        var obj = getSession((string)LanguageManager.LanguageKey);
        if (obj == null)
        {
          var defaultLangulage = eProperty.Where(b => b.Name == "DefaultLanguage").FirstOrDefault();
          if (defaultLangulage == null)
            return 0;
          var value = defaultLangulage.GetValue(null);
          if (value == null)
            return 0;
          return value.MyTryConvert<int>();
        }
        return obj.MyTryConvert<int>();
      };
      LanguageManager.SetLang = (lang) =>
      {
        setSession((string)LanguageManager.LanguageKey, lang);
      };


    }
  }
}
