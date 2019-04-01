using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSQL.Areas.Admin.Controllers
{
  public class ModelManagementController : Controller
  {
    // GET: Admin/ModelManagement
    public ActionResult Index(string id)
    {
      var type = ModelManager.GetModelType(id);
      if (type == null)
      {
        goto GoToDashBoard;
      }
      var model = ModelManager.GetContentTableHtmlView(type);
      return View(model);
      GoToDashBoard:
      return RedirectToAction("Index", "DashBoard", new { @area = "Admin" });
    }

    public ActionResult Create(string id)
    {
      var type = ModelManager.GetModelType(id);
      if (type == null)
      {
        goto GoToDashBoard;
      }
      
      var passModel = ModelManager.GetModelPostModelByType(type);
      if (passModel == null)
      {
        goto GoToDashBoard;
      }
      return View(passModel);
      GoToDashBoard:
      return RedirectToAction("Index", "DashBoard", new { @area = "Admin" });
    }
    [HttpPost]
    public ActionResult Create(ModelPostModel model)
    {
      ModelManager.Create(model);
      var key = ModelManager.GetMapperKey(model.FullType);
      if (String.IsNullOrEmpty(key))
      {
        return RedirectToAction("Index", "DashBoard", new { @area = "Admin", @id = key });
      }
      return RedirectToAction("Index", "ModelManagement", new { @area = "Admin",@id= key });
    }
    public ActionResult Edit(string type, long id)
    {
      if (string.IsNullOrEmpty(type))
      {
        return RedirectToAction("Index", "DashBoard", new { @area = "Admin" });
      }
      var model = ModelManager.Find(type, id, out ISave repo);
      return View(model.ConvertModelToModelPostModel());
    }
    [HttpPost]
    public ActionResult Edit(ModelPostModel model)
    {
      ModelManager.Update(model);
      var key = ModelManager.GetMapperKey(model.FullType);
      return RedirectToAction("Index", "DashBoard", new { @area = "Admin", @id = key });
    }
    public ActionResult Delete()
    {
      return View();
    }
  }
}