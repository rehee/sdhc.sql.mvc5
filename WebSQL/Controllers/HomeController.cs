using Entity.Models;
using SDHC.Common.Entity.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSQL.Models;

namespace WebSQL.Controllers
{
  public class HomeController : Controller
  {
    public ApplicationDbContext db = new ApplicationDbContext();
    public ActionResult Index()
    {
      var e1 = new E1();
      e1.Name = "1";
      var pass = e1.ConvertModelToPost();
      //db.e1s.Add(e1);
      var type = db.GetType();
      foreach(var property in type.GetProperties())
      {
        if(property.Name == "e1s")
        {
          var a = property;
          var b = property.PropertyType;
          var g = b.GenericTypeArguments.FirstOrDefault();
          var isE1 = g == typeof(E1);
          var method = b.GetMethods();
          var addMethod = b.GetMethod("Add");
          addMethod.Invoke(a.GetValue(db), new object[1]{ e1 });
          db.SaveChanges();
        }
      };
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