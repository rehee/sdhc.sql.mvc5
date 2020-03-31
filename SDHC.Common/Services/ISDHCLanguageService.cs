using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Services
{
  public interface ISDHCLanguageService
  {
    string LanguageKey { get; }
    Func<int> GetLang { get; }
    Action<int> SetLang { get; }
  }
  public interface ISDHCLanguageServiceInit 
  {
    string LanguageKey { get; }
    Func<string, object> getSession { get; }
    Action<string, object> setSession { get; }
  }
}
