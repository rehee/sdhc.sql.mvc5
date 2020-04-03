﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using SDHC.Common.EntityCore.Services;
using SDHC.Common.Services;
using SDHC.Models.NetCore.Models;
using SDHC.Models.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Services
{
  public static class SUContainer
  {
    public static void SUFunction<TRepo, TUser, TBaseContent, TBaseSelect>(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
      where TRepo : DbContext, IContent
      where TUser : SDHCUser, new()
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
    {
      var systemConfigKey = "SystemConfig";
      services.Configure<SystemConfig>(configuration.GetSection(systemConfigKey));
      services.AddSession();
      services.AddHttpContextAccessor();
      Action<DbContextOptionsBuilder> dbAction = options =>
      {
        options.UseSqlServer(
              configuration.GetConnectionString("DefaultConnection"));
      };

      services.AddScoped<ISDHCLanguageServiceInit, SDHCLanguageServiceInit>();
      services.AddDbContext<TRepo>(dbAction);

      services.AddIdentity<TUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddEntityFrameworkStores<TRepo>();

      services.InitSDHC<TRepo, TBaseContent, TBaseSelect, FormFile>(configuration, dbAction,
               env.ContentRootPath, systemConfigKey);
      //services.AddIdentity<TUser, IdentityRole>()
      //  .AddEntityFrameworkStores<TRepo>();
      //services.AddScoped<RoleStore<IdentityRole>>();
      //services.AddScoped<UserManager<TUser>>();
      services.AddScoped<RoleManager<IdentityRole>>();

      services.AddScoped<ISDHCUserManager<TUser>, SDHCUserManager<TUser>>();


      services.AddSingleton<IEmailService, EmailService>(b => new EmailService(ConfigContainer.Systems));
      services.AddSingleton<ISmsService, SmsService>();

      services.AddControllersWithViews();
      services.AddRazorPages();
      services.ConfigureOptions(typeof(V.EditorRCLConfigureOptions));
      services.AddSingleton<ISecretService, SecretService>(s => new SecretService(ConfigContainer.Systems));
      ServiceContainer.SecretService = new SecretService(ConfigContainer.Systems);
    }
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseStaticFiles();
      app.UseHttpsRedirection();

      app.UseSession();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
        name: "Files",
        pattern: $"{ConfigContainer.Systems.FileUploadPath}/{{*path}}", defaults: new { controller = "Files", action = "Index", });
        endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
