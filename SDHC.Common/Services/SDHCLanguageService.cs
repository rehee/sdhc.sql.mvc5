using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Services
{
  public class SDHCLanguageService : ISDHCLanguageService
  {
    private Func<string, object> getSession { get; }
    private Action<string, object> setSession { get; }
    public SDHCLanguageService(ISDHCLanguageServiceInit init)
    {
      this.LanguageKey = init.LanguageKey;
      this.getSession = init.getSession;
      this.setSession = init.setSession;
    }
    public string LanguageKey { get; }
    public Func<int> GetLang => () =>
    {
      var obj = getSession(LanguageKey);
      if (obj == null)
      {
        return ConfigContainer.Systems.DefaultLanguage;
      }
      return obj.MyTryConvert<int>();
    };
    public Action<int> SetLang => (lang) =>
    {
      setSession(LanguageKey, lang);
    };
  }
}
