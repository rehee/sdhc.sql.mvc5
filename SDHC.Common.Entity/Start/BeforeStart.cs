using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
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
      var eProperty = E.GetProperties();
      G.UserManager = () => OwinContextExtensions.Get<ApplicationUserManager>(getContext());
      G.SignManager = () => OwinContextExtensions.Get<ApplicationSignInManager>(getContext());
      G.RoleManager = () => OwinContextExtensions.Get<ApplicationRoleManager>(getContext());
      Action<PropertyInfo> setValue = p =>
      {
        var value = G.GetSetting(p.Name);
        if (value == null)
          return;
        if (value.Text() == "")
          return;
        p.SetValue(null, value.MyTryConvert(p.PropertyType));
      };
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
