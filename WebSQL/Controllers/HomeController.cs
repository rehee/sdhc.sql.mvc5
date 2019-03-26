using Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using Start;
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
    //public ActionResult Index()
    public async Task<ActionResult> Index()
    {
      var user = new MyUser();
      user.UserName = "tu1";
      user.Email = "tu1@1.com";
      //await _userManager.CreateAsync(user, "1");
      var s = await _userManager.FindByNameAsync(user.UserName);
      var v = await _userManager.CheckPasswordAsync(s, "1");
      var token = await _userManager.GeneratePasswordResetTokenAsync(s.Id);
      await _userManager.ResetPasswordAsync(s.Id, token, "2");
      
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