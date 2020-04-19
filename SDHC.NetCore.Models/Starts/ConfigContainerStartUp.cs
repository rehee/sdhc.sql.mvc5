using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.EntityCore.Services;
using SDHC.Common.Services;
using SDHC.NetCore.Models.Services.Razors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ConfigContainerStartUp
  {
    public static void InitSDHCContainer<TRepo, TBaseContent, TFileSngle, TUser>([NotNullAttribute] this IServiceCollection serviceCollection,
      IConfiguration configuration, Action<DbContextOptionsBuilder> optionsAction, string basicRoot, string systemConfigKey)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
      where TFileSngle : IFormFile
    {
      serviceCollection.SystemConfigInit(configuration, systemConfigKey);
      serviceCollection.ContainerInit<TRepo, TBaseContent, TUser>(optionsAction, ConfigContainer.Systems);
      serviceCollection.AddScoped<ISDHCLanguageServiceInit, SDHCLanguageServiceInit>();
      serviceCollection.TryAddScoped<ISDHCLanguageService, SDHCLanguageService>();
      serviceCollection.FileServiceInit<TFileSngle>(basicRoot);
      serviceCollection.TryAddScoped<IViewNameService, ViewNameService>();

    }
  }
}
