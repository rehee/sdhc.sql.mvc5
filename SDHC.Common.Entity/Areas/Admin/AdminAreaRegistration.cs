using System;
using System.Web.Mvc;

namespace Admin.Areas.Admin
{
  public class AdminAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Admin";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.MapRoute(
          "Admin_default",
          $"{(String.IsNullOrEmpty(G.AdminPath) ? "Admin" : G.AdminPath)}/{{controller}}/{{action}}/{{id}}",
          new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}