﻿using Microsoft.EntityFrameworkCore;
using SDHC.Common.Cruds;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ContainerInitFunction
  {
    public static void ContainerInit<TRepo, TBaseContent, TBaseSelect>([NotNullAttribute] this IServiceCollection serviceCollection,
      Action<DbContextOptionsBuilder> optionsAction)
      where TRepo : DbContext, IContent
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
    {
      var builder = new DbContextOptionsBuilder<TRepo>();
      optionsAction(builder);
      var options = builder.Options;
      var crudInit = new CrudSelectInit(
        () => Activator.CreateInstance(typeof(TRepo), options) as TRepo,
        typeof(TBaseContent), typeof(TBaseSelect)
      );
      CrudContainer.Crud = new BaseCruds(crudInit);
      CrudContainer.CrudModel = new CrudModel(crudInit);
      CrudContainer.CrudContent = new CrudContent(crudInit);
      ServiceContainer.ModelService = new ModelService(crudInit);
      ServiceContainer.ContentService = new ContentService(crudInit);
      ServiceContainer.SelectService = new SelectService(crudInit);
    }
  }
}
