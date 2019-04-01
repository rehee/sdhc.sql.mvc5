using Admin.Areas.Admin.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System
{
  public class AdminAttribute : AuthorizeAttribute
  {
    public AdminAttribute()
    {

    }
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      if (!G.AdminRequestLogin)
        return true;
      var request = httpContext.Request;
      var currentUserId = httpContext.User.Identity.GetUserId();
      if (String.IsNullOrEmpty(currentUserId))
        return false;
      //if (currentUserId.UserIsInRoles(G.SuperUserRole))
      //  return true;

      Func<string, string> GetKey = key =>
       {
         if (request.RequestContext.RouteData.Values.ContainsKey(key))
         {
           return request.RequestContext.RouteData.Values[key].ToString();
         }
         return "";
       };

      string controller = GetKey("controller");
      string action = GetKey("action");
      string id = GetKey("id");

      var roles = TypeExtends.GetTypeFromController(
        controller, action, id, request.RequestContext.RouteData.Values, request.Form);


      return true;
    }
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
