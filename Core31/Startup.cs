using Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDHC.NetCore.Models.Models;
using System.Reflection;
using System;
using View.Areas.Admin.Controllers;

namespace Core31
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      WebHostEnvironment = env;
    }
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment WebHostEnvironment { get; }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      // Source: https://dotnetstories.com/blog/Dynamically-pre-load-assemblies-in-a-ASPNET-Core-or-any-C-project-en-7155735300


      services.StartUpFunction<MyDBContext, SDHCUser, BaseContentModel>(Configuration, WebHostEnvironment);
      services.AddSignalR();
      services.UseChat();
      ServiceContainer.ModelService.AddSharedContent<Home>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
      }
      //app.UseDeveloperExceptionPage();
      //app.UseStatusCodePagesWithReExecute("/error/{0}");
      //app.UseExceptionHandler("/error/500");
      app.UseSDHC(env);
      app.UseChat();

      ServiceContainer.ModelService.ModelMapper.Add("OurService", typeof(OurService));

    }

  }


}
