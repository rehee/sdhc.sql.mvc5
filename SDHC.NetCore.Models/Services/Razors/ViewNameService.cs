using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SDHC.NetCore.Models.Services.Razors
{
  public class ViewNameService : IViewNameService
  {
    private readonly SystemConfig config;
    private readonly ISDHCLanguageService lang;

    public ViewNameService(IOptions<SystemConfig> config, ISDHCLanguageService lang)
    {
      this.config = config.Value;
      this.lang = lang;
    }
    private string getLangMark(int inputLang)
    {
      string langMark = null;
      if (config.ViewForEveryLang)
      {
        var langKey = lang.LangConfigs.FirstOrDefault(b => b.Key == inputLang)?.Value ?? "";
        if (!String.IsNullOrWhiteSpace(langKey))
        {
          langMark = $"_{langKey}";
        }
      }
      return langMark;
    }
    public string ViewName(ILanguage model)
    {
      var type = model?.GetType();
      if (type == null)
        return null;

      var allInterface = type.GetInterfaces();
      if (allInterface.Any(b => b == typeof(IBasicContent)))
      {
        return $"Views/{config.ContentViewPath ?? "Pages"}/{type.Name}{getLangMark(model.Lang) ?? ""}.cshtml";
      }
      if (allInterface.Any(b => b == typeof(ISharedLink)))
      {
        return $"Views/{config.ContentViewPath ?? "Pages"}/{type.Name}{getLangMark(model.Lang) ?? ""}.cshtml";
      }
      throw new NotImplementedException();
    }

    public string ViewName(ContentPostModel model)
    {
      var type = Type.GetType($"{model.FullType},{model.ThisAssembly}");
      return $"Views/{config.ContentViewPath ?? "Pages"}/{type.Name}{getLangMark(model.Lang ?? 0) ?? ""}.cshtml";
    }

    public string ViewName(ContentViewModal model)
    {
      return ViewName(model.Model);
    }
    public string ViewName(ContentPropertyIndex model)
    {
      return $"~/Views/Shared{(String.IsNullOrWhiteSpace(config.SharedLinkViewPath) ? "" : $"/{config.SharedLinkViewPath}")}/{model.Property.Key}{getLangMark(model.Lang ?? 0) ?? ""}.cshtml";
    }
  }
}
