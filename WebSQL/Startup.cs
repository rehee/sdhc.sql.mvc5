using Entity.Models;
using Microsoft.Owin;
using Owin;
using SDHC.Common.Entity.Models;
using Start;
using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using WebSQL.Models;

[assembly: OwinStartupAttribute(typeof(WebSQL.Startup))]
namespace WebSQL
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      SDHCStartup.Init<ApplicationDbContext, SCHCContent, SDHCBascSelect, MyUser>(
        app, () => ApplicationDbContext.Create(), HostingEnvironment.MapPath("/"));
      ModelManager.ModelMapper = new Dictionary<string, Type>()
      {
        ["S1"] = typeof(S1),
        ["S2"] = typeof(S2),
        ["Gender"] = typeof(GenderSelect),
      };
      ModelManager.ModelManagerMapper = new List<string>()
      {
        "S1","S2","Gender"
      };

      
    }
  }
  
}
