﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSQL.Areas.Admin.Controllers
{
  [Admin(Roles = "Admin")]
  public class DashBoardController : Controller
  {
    // GET: Admin/Home
    public ActionResult Index()
    {
      return View();
    }
  }
}