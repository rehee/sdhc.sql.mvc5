using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSQL.Controllers
{
  public class Content2Controller : Controller
  {
    public ActionResult Index(string names)
    {

      return Content(names);
    }
    public ActionResult Create()
    {
      return View();
    }
    public ActionResult Read()
    {
      return View();
    }
    public ActionResult Update()
    {
      return View();
    }
    public ActionResult Delete()
    {
      return View();
    }
  }
}