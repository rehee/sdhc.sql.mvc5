using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebSQL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
      E.Init();
      ViewEngines.Engines.Add(new CustomViewEngine());
      AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

  public class CustomViewEngine : RazorViewEngine
  {
    public CustomViewEngine()
    {
      MasterLocationFormats = new string[]
      {
            "~/bin/Views/{1}/{0}.cshtml",
            "~/bin/Views/{1}/{0}.vbhtml",
            "~/bin/Views/Shared/{0}.cshtml",
            "~/bin/Views/Shared/{0}.vbhtml"

      };
      ViewLocationFormats = new string[]
      {
             "~/bin/Areas/{2}/Views/{1}/{0}.cshtml",
             "~/bin/Areas/{2}/Views/{1}/{0}.vbhtml",
             "~/bin/Areas/{2}/Views/Shared/{0}.cshtml",
             "~/bin/Areas/{2}/Views/Shared/{0}.vbhtml"
      };
    }
  }
}
