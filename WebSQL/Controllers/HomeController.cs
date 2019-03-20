using Dapper;
using DelegateDecompiler;
using Entity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SDHC.Common.Entity.Cruds;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Json;
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
    ApplicationDbContext db = new ApplicationDbContext();
    public ActionResult Index()
    {
      var s2 = new S2();
      s2.Gender = $",1, 2,";
      s2.Title = "1";
      var model = s2.ConvertModelToPost();
      var m = model.ConvertToBaseModel();
      
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