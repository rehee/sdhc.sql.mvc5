using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System
{
  public class AdminAttribute : AuthorizeAttribute
  {
    public AdminAttribute(string adminRole = "")
    {
      var adminRoleForSetting = G.GetSetting(adminRole);
      var DefaultadminRole = G.AdminRole;
      var supperUser = G.SuperUserRole;
      var roleLists = new List<string>();
      if (!String.IsNullOrEmpty(adminRoleForSetting))
      {
        roleLists.AddRange(adminRoleForSetting.Split(',')
          .Select(b => b.Trim())
          .Where(b => !String.IsNullOrEmpty(b)));
      }
      if (!String.IsNullOrEmpty(DefaultadminRole))
      {
        roleLists.AddRange(DefaultadminRole.Split(',')
          .Select(b => b.Trim())
          .Where(b => !String.IsNullOrEmpty(b)));
      }
      if (!String.IsNullOrEmpty(supperUser))
      {
        roleLists.AddRange(supperUser.Split(',')
          .Select(b => b.Trim())
          .Where(b => !String.IsNullOrEmpty(b)));
      }
      var uniqRoles = roleLists.GroupBy(b => b).Select(b => b.Key);
      this.Roles = String.Join(",", uniqRoles);
      Console.WriteLine("");
    }
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      if (G.AdminFree)
      {
        return true;
      }
      return base.AuthorizeCore(httpContext);
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
