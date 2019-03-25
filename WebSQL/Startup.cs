using Entity.Models;
using Microsoft.Owin;
using Owin;
using SDHC.Common.Entity.Models;
using Start;
using System;
using WebSQL.Models;

[assembly: OwinStartupAttribute(typeof(WebSQL.Startup))]
namespace WebSQL
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      SDHCStartup.ConfigureAuth<ApplicationDbContext>(app, () => ApplicationDbContext.Create());
      BaseCruds.GetRepo = () => new ApplicationDbContext();
      ContentManager.BasicContentType = typeof(SCHCContent);
      SelectManager.BasicSelectType = typeof(SDHCBascSelect);
    }
  }
}
