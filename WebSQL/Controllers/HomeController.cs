using Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using Start;
using System;
using System.Collections.Generic;
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
      var g = G.UserManager();
    }
    //public ActionResult Index()
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Page(string name)
    {
      return Content("");
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