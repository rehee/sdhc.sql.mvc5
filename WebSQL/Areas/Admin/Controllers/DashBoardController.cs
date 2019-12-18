using Entity.Models;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{
  [Admin("Admin")]
  public class DashBoardController : Controller
  {
    // GET: Admin/Home
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Edit(ModelPostModel model)
    {
      var e2 = model.ConvertToBaseModel() as E2;
      return Json(e2);
    }
  }
}