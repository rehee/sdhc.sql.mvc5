using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SDHC.Common.Configs;
using SDHC.Common.Cruds;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.Services;
using SDHC.NetCore.Models.Authorizes;
using SDHC.NetCore.Models.Services.Contents;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ContainerInitFunction
  {
    public static void ContainerInit<TRepo, TBaseContent, TUser>([NotNullAttribute] this IServiceCollection serviceCollection,
      Action<DbContextOptionsBuilder> optionsAction, SystemConfig config)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
    {
      var builder = new DbContextOptionsBuilder<TRepo>();
      optionsAction(builder);
      var options = builder.Options;
      var crudInit = new CrudInit(
        () => Activator.CreateInstance(typeof(TRepo), options) as TRepo,
        typeof(TBaseContent));
      CrudContainer.Crud = new BaseCruds(crudInit);
      CrudContainer.CrudModel = new CrudModel(crudInit);
      CrudContainer.CrudContent = new CrudContent(crudInit);
      CrudContainer.BaseUser = typeof(TUser);
      ServiceContainer.ModelService = new ModelService(crudInit);
      ServiceContainer.ContentService = new ContentService(crudInit);
      ServiceContainer.SelectService = new SelectService(crudInit);

      serviceCollection.AddSingleton<ISecretService, SecretService>(s => new SecretService(config));
      ServiceContainer.SecretService = new SecretService(config);

      serviceCollection.AddScoped<IContentViewService, ContentViewService>();
    }
  }
}
