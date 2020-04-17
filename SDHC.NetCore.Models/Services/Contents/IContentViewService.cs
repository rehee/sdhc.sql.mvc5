using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.NetCore.Models.Services.Contents
{
  public interface IContentViewService
  {
    IEnumerable<ModelViewModal> Models { get; }
    string GetModelValueByName<T>(int lang, string name);
    string GetModelNameByName<T>(int lang, string name);
    string GetModelRefreshByName<T>(int lang, string name);
    string GetModelReviewByName<T>(int lang, string name);
  }

  public class ContentViewService : IContentViewService
  {
    private readonly ISDHCLanguageService lang;
    private readonly IEnumerable<LanguageSetting> langSetting;
    private IDictionary<Tuple<string, int>, ModelViewModal> modelMapper { get; }
    public IEnumerable<ModelViewModal> Models
    {
      get => modelMapper.Values;
    }

    public ContentViewService(ISDHCLanguageService lang, IOptions<LanguageConfig> langSetting)
    {
      this.lang = lang;
      this.langSetting = langSetting.Value.LanguageSettings;
      var mapper = ServiceContainer.ModelService.getAllSharedContentByLangs(this.langSetting.Select(b => b.Key));
      modelMapper = new Dictionary<Tuple<string, int>, ModelViewModal>();
      var index = 0;
      foreach (var m in mapper)
      {
        modelMapper.Add(m.Key, new ModelViewModal(m.Value.ConvertModelToModelPostModel(), $"Models[{index}]"));
        index++;
      }
    }

    public string GetModelNameByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModelNameByName(name);
    }

    public string GetModelValueByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModelValueByName(name);
    }

    public string GetModelRefreshByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModelRefreshByName(name);
    }

    public string GetModelReviewByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModelReviewByName(name);
    }
  }
}
