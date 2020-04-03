using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

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
      G.GetContext = getContext;
      var eProperty = E.GetProperties();
      //G.UserManager = () => OwinContextExtensions.Get<ApplicationUserManager>(getContext());
      //G.SignManager = () => OwinContextExtensions.Get<ApplicationSignInManager>(getContext());
      G.RoleManager = () => OwinContextExtensions.Get<ApplicationRoleManager>(getContext());
      ConfigContainer.Systems = new SDHC.Common.Configs.SystemConfig();
      Action<PropertyInfo> setValue = p =>
      {
        var value = G.GetSetting(p.Name);
        if (value == null)
          return;
        if (value.Text() == "")
          return;
        p.SetValue(null, value.MyTryConvert(p.PropertyType));
      };
      typeof(SystemConfig).GetProperties().Where(b => true)
        .ToList()
        .ForEach(b =>
        {
          try
          {
            setValue(b);
          }
          catch { }
        });
      typeof(G).GetProperties().Where(b => b.GetObjectCustomAttribute<ConfigAttribute>() != null)
        .ToList()
        .ForEach(b =>
        {
          try
          {
            setValue(b);
          }
          catch { }
        });
      E.GetProperties().Where(b => b.GetObjectCustomAttribute<ConfigAttribute>() != null)
        .ToList()
        .ForEach(b =>
        {
          try
          {
            setValue(b);
          }
          catch { }
        });

      ServiceContainer.SecretService = new SecretService(ConfigContainer.Systems);

      LanguageManager.GetLang = () =>
      {
        var obj = getSession((string)LanguageManager.LanguageKey);
        if (obj == null)
        {
          return G.DefaultLanguage;

        }
        return obj.MyTryConvert<int>();
      };
      LanguageManager.SetLang = (lang) =>
      {
        setSession((string)LanguageManager.LanguageKey, lang);
      };

      if (G.UseContentRouter)
      {
        RouteTable.Routes.MapRoute("content", $"{(String.IsNullOrEmpty(G.ContentPageUrl) ? "" : G.ContentPageUrl + "/")}{{*names}}",
              defaults: new { controller = "SDHCPage", action = "Index" });
      }
    }
  }
}
