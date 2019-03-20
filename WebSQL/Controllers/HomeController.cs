using Entity.Models;
using SDHC.Common.Entity.Extends;
using System;
using System.Web.Mvc;
using WebSQL.Models;

namespace WebSQL.Controllers
{
  public class HomeController : Controller
  {
    ApplicationDbContext db = new ApplicationDbContext();
    public ActionResult Index()
    {
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