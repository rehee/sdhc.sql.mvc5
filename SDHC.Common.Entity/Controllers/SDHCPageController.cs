using SDHC.Common.Entity.Models;
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
      var m = ServiceContainer.ContentService.GetContentPostViewModel(names);
      if(string.IsNullOrEmpty(m.ViewPath))
        return Content("NoContent");
      return View(m.ViewPath, m);
    }
    [Admin]
    public ActionResult Preview(long? id)
    {
      var model = ServiceContainer.ContentService.GetContent(id);
      if (model == null)
        return RedirectToAction("Index", "Content", new { @id = "", @area = G.AdminPath });
      var m = new ContentPostViewModel(model);
      return View(m.ViewPath, m);
    }
  }
}