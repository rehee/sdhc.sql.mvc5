using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSQL.Areas.Admin.Controllers
{
  public class UserController : Controller
  {
    // GET: Admin/User
    public ActionResult Index()
    {
      return RedirectToAction("Index", "DashBoard", new { @area = "Admin" });
    }
    public ActionResult Login()
    {
      return View();
    }
  }
}