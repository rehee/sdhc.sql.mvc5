using Entity.Models;
using Microsoft.Owin;
using Owin;
using SDHC.Common.Entity.Models;
using System;
using WebSQL.Models;

[assembly: OwinStartupAttribute(typeof(WebSQL.Startup))]
namespace WebSQL
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      BaseContent.GetRepo = () => new ApplicationDbContext();
      ContentCruds.GetRepo = () => new ApplicationDbContext();
      ContentManager.BasicContentType = typeof(SCHCContent);
    }
  }
}
