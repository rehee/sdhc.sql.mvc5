using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDHC.Controllers
{
  public class SDHCPageController : Controller
  {
    public ActionResult Index(string names)
    {
      var m = ContentManager.GetContentPostViewModel(names);
      if(string.IsNullOrEmpty(m.ViewPath))
        return Content("NoContent");
      return View(m.ViewPath, m.Model);
    }
  }
}