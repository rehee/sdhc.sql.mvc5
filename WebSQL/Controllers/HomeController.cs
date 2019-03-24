using Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSQL.Models;

namespace WebSQL.Controllers
{
  public class HomeController : Controller
  {
    ApplicationDbContext db = new ApplicationDbContext();
    private ApplicationUserManager _userManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
    private ApplicationSignInManager _signInManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>(); } }
    private ApplicationRoleManager _roleManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); } }

    public HomeController()
    {

    }
    public ActionResult Index()
    {
      //var user = new ApplicationUser();
      //user.UserName = "u1";
      //user.Email = "u1@u1.com";
      //user.AAA = "AAA";
      //this._userManager.CreateAsync(user, "Spdevmap1!");

      //var user = this._userManager.Users.FirstOrDefault();
      //var pass = user.ConvertUserToPost();
      //var ub = pass.ConvertPostToUser(user);
      //var m = new ApplicationUser();
      //m.AAA = "1233";
      //m.UserName = "1";
      //await _userManager.CreateAsync(m, "123");
      //var role = new IdentityRole();
      //role.Name = "Admin";
      //db.Roles.Add(role);
      //db.SaveChanges();
      var r = _roleManager.Roles.ToList();
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";
      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      return View();
    }
  }
}