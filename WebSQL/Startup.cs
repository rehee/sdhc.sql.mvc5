using Dapper;
using Entity.Models;
using Microsoft.Owin;
using Owin;
using SDHC.Common.Entity.Cruds;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using WebSQL.Models;

[assembly: OwinStartupAttribute(typeof(WebSQL.Startup))]
namespace WebSQL
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      SCHCContent.GetRepo = () => new ApplicationDbContext();
      ContentCruds.GetRepo = () => new ApplicationDbContext();
    }
  }
}
