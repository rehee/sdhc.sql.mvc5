﻿using DelegateDecompiler;
using Entity.Models;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebSQL.Attribute;
using WebSQL.Models;

namespace WebSQL.Controllers
{
  public class HomeController : Controller
  {

    public ApplicationDbContext db = new ApplicationDbContext();
    [ValidateDto]
    public ActionResult Index()
    {
      
      var list = Microsoft.Linq.Translations.ExpressiveExtensions.WithTranslations(db.e3s.Where(b => b.ListValue.Contains("1"))).ToList();
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