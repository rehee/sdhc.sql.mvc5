using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Areas.Admin.Controllers
{
  public class DropDownController : Controller
  {
    // GET: Admin/DropDown
    public ActionResult Index(string id)
    {
      if (String.IsNullOrEmpty(id))
      {

        var list = ServiceContainer.SelectService.GetAllAvaliableSelectList();
        return View(list);
      }
      var list2 = ServiceContainer.SelectService.GetAllSelect(id.Replace('_', '.'));
      return View("IndexSelect", list2);
    }


  }
}