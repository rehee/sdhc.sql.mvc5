using Dapper;
using Entity.Models;
using Microsoft.Owin;
using Owin;
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
      SqlMapper.AddTypeHandler(typeof(List<long>), new EHandler());
      ConfigureAuth(app);
      var a = (IObjectContextAdapter)new ApplicationDbContext();
      var o = a.ObjectContext;
      ContentIndex.repo = () => new ApplicationDbContext();
      ContentCRUD.repo = () => new ApplicationDbContext();
    }
  }
}
