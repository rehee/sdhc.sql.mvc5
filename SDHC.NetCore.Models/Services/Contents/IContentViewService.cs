﻿using Microsoft.Extensions.Options;
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
    string GetValueByName<T>(int lang, string name);
    string GetModalNameByName<T>(int lang, string name);
    string GetModalRefreshByName<T>(int lang, string name);
    string GetModalReviewByName<T>(int lang, string name);
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

    public string GetModalNameByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModalNameByName(name);
    }

    public string GetValueByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetValueByName(name);
    }

    public string GetModalRefreshByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModalRefreshByName(name);
    }

    public string GetModalReviewByName<T>(int lang, string name)
    {
      var key = typeof(T).FullName;
      return modelMapper[new Tuple<string, int>(key, lang)]?.GetModalReviewByName(name);
    }
  }
}
