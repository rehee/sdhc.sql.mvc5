using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SDHC.Common.Configs;
using SDHC.Common.EntityCore.Services;
using SDHC.Common.Services;
using SDHC.Models.NetCore.Models;
using Core31.Models;
using Core31.Hubs;

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
      services.StartUpFunction<MyDBContext, SDHCUser, BaseContentModel, BaseSelectModel>(Configuration, WebHostEnvironment);
      services.AddSignalR();
      
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
      }
      app.UseDeveloperExceptionPage();
      StartUpContainer.Configure(app, env);
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<ChatHub>("/chatHub");
      });

    }
  }

  
}
