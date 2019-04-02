using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System
{
  public class AdminAttribute : AuthorizeAttribute
  {
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      base.HandleUnauthorizedRequest(filterContext);

      filterContext.Result = new RedirectToRouteResult(
        new RouteValueDictionary(new { controller = ControllerName, action = ActionName, area = AreaName }));
    }
    public static string ControllerName { get; set; } = "User";
    public static string ActionName { get; set; } = "Login";
    public static string AreaName { get; set; } = "Admin";
  }
  public class MemberAttribute : AuthorizeAttribute
  {
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      base.HandleUnauthorizedRequest(filterContext);

      filterContext.Result = new RedirectToRouteResult(
        new RouteValueDictionary(new { controller = ControllerName, action = ActionName, area = AreaName }));
    }
    public static string ControllerName { get; set; } = "Account";
    public static string ActionName { get; set; } = "Login";
    public static string AreaName { get; set; } = "";
  }
}
