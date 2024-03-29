﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{
  public class DropDownController : Controller
  {
    // GET: Admin/DropDown
    public ActionResult Index(string id)
    {
      if (String.IsNullOrEmpty(id))
      {

        var list = SelectManager.GetAllAvaliableSelectList();
        return View(list);
      }
      var list2 = SelectManager.GetAllSelect(id.Replace('_', '.'));
      return View("IndexSelect", list2);
    }


  }
}