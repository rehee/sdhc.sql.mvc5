using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.Common.Configs
{
  public class LanguageConfig
  {
    public IEnumerable<LanguageSetting> LanguageSettings { get; set; }
    public string LanguageKey { get; set; } = "Lang";

    public LanguageSetting DefaultLanguage
    {
      get
      {
        if (LanguageSettings == null)
          return null;
        var first = LanguageSettings.FirstOrDefault(b => b.IsDefault);
        if (first != null)
          return first;
        return LanguageSettings.FirstOrDefault();
      }
    }
    public int? GetLangKey(int? input)
    {
      if (!input.HasValue)
        goto GetDefaultKey;
      var searchValue = LanguageSettings.FirstOrDefault(b => b.Key == input.Value);
      if (searchValue == null)
        goto GetDefaultKey;
      return searchValue.Key;
      GetDefaultKey:
      var defaultLang = DefaultLanguage;
      return defaultLang != null ? (int?)defaultLang.Key : null;
    }
  }

  public class LanguageSetting
  {
    public int Key { get; set; }
    public string Value { get; set; }
    public string DisplayName { get; set; }
    public bool IsDefault { get; set; }
  }
}
