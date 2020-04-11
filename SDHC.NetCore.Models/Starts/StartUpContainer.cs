using Microsoft.AspNetCore.Builder;
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
using SDHC.NetCore.Models.Models;
using SDHC.NetCore.Models.Services;
using SDHC.NetCore.Models.Services.UserAndRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class StartUpContainer
  {
    public static void StartUpFunction<TRepo, TUser, TBaseContent, TBaseSelect>(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
      where TRepo : DbContext, IContent
      where TUser : SDHCUser, new()
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
    {
      var systemConfigKey = "SystemConfig";
      services.AddSession();
      services.AddHttpContextAccessor();

      Action<DbContextOptionsBuilder> dbAction = options =>
      {
        options.UseSqlServer(
              configuration.GetConnectionString("DefaultConnection"));
      };
      services.AddDbContext<TRepo>(dbAction);

      services.InitSDHCContainer<TRepo, TBaseContent, TBaseSelect, FormFile, TUser>(configuration, dbAction,
               env.ContentRootPath, systemConfigKey);


      services.UserAndAuthInit<TRepo, TUser, TBaseContent>();


      services.AddSingleton<IEmailService, EmailService>(b => new EmailService(ConfigContainer.Systems));
      services.AddSingleton<ISmsService, SmsService>();

      services.AddControllersWithViews();
      services.AddRazorPages();
      services.ConfigureOptions(typeof(V.EditorRCLConfigureOptions));
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
        name: "Pages",
        pattern: $"Pages/{{*path}}", defaults: new { controller = "Page", action = "Index", });

        endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}", defaults: new { area = "Admin" });
      });
    }
  }
}

