using Entity.Models;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
      //var e2 = new E2();
      //e2.Title = "1";
      ////var pass = e1.ConvertModelToPost();
      ////db.e1s.Add(e1);
      //var e3 = new E3();
      //e3.FullType = typeof(E1_2).FullName;
      //var type = db.GetType();

      //foreach (var property in type.GetProperties())
      //{
      //  if (property.PropertyType.GenericTypeArguments.FirstOrDefault() == typeof(E2))
      //  {
      //    var addMethod = property.PropertyType.GetMethod("Add");
      //    addMethod.Invoke(property.GetValue(db), new object[1] { e2 });
      //    Expression<Func<IInt64Key, bool>> w = f => f.Id == 1;
      //    var list = Queryable.Where<IInt64Key>((IQueryable<IInt64Key>)property.GetValue(db), w).ToList();
      //    db.SaveChanges();
      //  }
      //};
      //db.e3s.Add(e3);
      var lastE2 = db.e2s.OrderByDescending(b => b.Id).FirstOrDefault();
      var e2 = new E2();
      //lastE2.CreateContentChildren(e2);
      e2.CreateContent();
      e2.Move(Guid.Parse("CE86F4C1-AFC4-44D2-B49D-2465A5D91751"));
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