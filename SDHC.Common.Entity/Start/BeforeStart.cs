using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
  public static class BeforeStart
  {
    public static void Init(Func<string, string> getConfig, Type E, Func<string, object> getSession, Action<string, object> setSession)
    {
      G.GetSetting = getConfig;
      var eProperty = E.GetProperties();
      foreach (var p in eProperty)
      {
        var config = p.GetObjectCustomAttribute<ConfigAttribute>();
        if (config == null)
        {
          continue;
        }
        try
        {
          var value = G.GetSetting(p.Name);
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
        setSession((string)LanguageManager.LanguageKey,lang);
      };

      
    }
  }
}
