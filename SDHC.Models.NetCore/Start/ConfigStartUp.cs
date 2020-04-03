﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ConfigStartUp
  {
    public static void InitSDHC<TRepo, TBaseContent, TBaseSelect, TFileSngle>([NotNullAttribute] this IServiceCollection serviceCollection,
      IConfiguration configuration, Action<DbContextOptionsBuilder> optionsAction, string basicRoot, string systemConfigKey)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
      where TFileSngle : IFormFile
    {
      serviceCollection.SystemConfigInit(configuration, systemConfigKey);
      serviceCollection.ContainerInit<TRepo, TBaseContent, TBaseSelect>(optionsAction);
      serviceCollection.TryAddScoped<ISDHCLanguageService, SDHCLanguageService>();
      serviceCollection.FileServiceInit<TFileSngle>(basicRoot);


    }
  }
}
