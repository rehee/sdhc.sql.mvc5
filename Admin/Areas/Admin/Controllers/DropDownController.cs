using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{
  public class DropDownController : Controller
  {
    // GET: Admin/DropDown
    public ActionResult Index()
    {
      var list = SelectManager.GetAllAvaliableSelectList();
      return View(list);
    }
  }
}