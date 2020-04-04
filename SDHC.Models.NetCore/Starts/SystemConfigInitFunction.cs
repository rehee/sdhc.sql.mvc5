using Microsoft.Extensions.Configuration;
using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class SystemConfigInitFunction
  {
    public static void SystemConfigInit([NotNullAttribute] this IServiceCollection serviceCollection, IConfiguration configuration,
      string systemConfigKey)
    {
      serviceCollection.Configure<SystemConfig>(configuration.GetSection(systemConfigKey));
      serviceCollection.Configure<LanguageConfig>(configuration.GetSection(nameof(LanguageConfig)));
      ConfigContainer.GetSetting = (key) => configuration.GetValue<string>(key);
      IConfigurationSection sec = configuration.GetSection(systemConfigKey);
      var type = typeof(SystemConfig);
      var obj = new SystemConfig();
      type.GetProperties().ToList().ForEach(p =>
      {
        var value = sec[p.Name];
        if (value != null)
        {
          try
          {
            var cValue = value.MyTryConvert(p.PropertyType);
            p.SetValue(obj, cValue);
          }
          catch { }
        }
      });
      ConfigContainer.Systems = obj;
    }
  }
}
