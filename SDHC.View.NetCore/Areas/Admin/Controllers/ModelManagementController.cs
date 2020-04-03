﻿using Microsoft.AspNetCore.Mvc;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Areas.Admin.Controllers
{
  public class ModelManagementController : Controller
  {
    //[Admin(adminRole: "ModelManagementIndex")]
    public ActionResult Index(string id)
    {
      var type = ServiceContainer.ModelService.GetModelType(id);
      if (type == null)
      {
        goto GoToDashBoard;
      }
      if (type.IsSingleRecordTable())
      {
        var singleRecord = ServiceContainer.ModelService.Read<IInt64Key>(type, b => true).FirstOrDefault();
        if (singleRecord == null)
        {
          return RedirectToAction("Create", "ModelManagement", new { @id = id, @area = ConfigContainer.Systems.AdminPath });
        }
        return RedirectToAction("Edit", "ModelManagement", new { @type = id, @id = singleRecord.Id, @area = ConfigContainer.Systems.AdminPath });
      }
      ViewBag.id = id;
      var model = ServiceContainer.ModelService.GetContentTableHtmlView(type);
      return View(model);
      GoToDashBoard:
      return RedirectToAction("Index", "DashBoard", new { @area = ConfigContainer.Systems.AdminPath });
    }

    //[Admin(adminRole: "ModelManagementCreate")]
    public ActionResult Create(string id)
    {
      var type = ServiceContainer.ModelService.GetModelType(id);
      if (type == null)
      {
        goto GoToDashBoard;
      }

      var passModel = ServiceContainer.ModelService.GetModelPostModelByType(type);
      if (passModel == null)
      {
        goto GoToDashBoard;
      }
      return View(passModel);
      GoToDashBoard:
      return RedirectToAction("Index", "DashBoard", new { @area = ConfigContainer.Systems.AdminPath });
    }
    [HttpPost]
    //[Admin(adminRole: "ModelManagementCreate")]
    public ActionResult Create(ModelPostModel model)
    {
      ServiceContainer.ModelService.Create(model);
      var key = ServiceContainer.ModelService.GetMapperKey(model.FullType);
      if (!String.IsNullOrEmpty(model.PostReturnUrl))
        return Redirect(model.PostReturnUrl);
      if (String.IsNullOrEmpty(key))
      {
        return RedirectToAction("Index", "DashBoard", new { @area = ConfigContainer.Systems.AdminPath, @id = key });
      }
      return RedirectToAction("Index", "ModelManagement", new { @area = ConfigContainer.Systems.AdminPath, @id = key });
    }

    //[Admin(adminRole: "ModelManagementEdit")]
    public ActionResult Edit(string type, long id)
    {
      if (string.IsNullOrEmpty(type))
      {
        return RedirectToAction("Index", "DashBoard", new { @area = ConfigContainer.Systems.AdminPath });
      }
      var model = ServiceContainer.ModelService.Find(type, id, out ISave repo);
      return View(model.ConvertModelToModelPostModel());
    }
    [HttpPost]
    //[Admin(adminRole: "ModelManagementEdit")]
    public ActionResult Edit(ModelPostModel model)
    {
      var key = ServiceContainer.ModelService.GetMapperKey(model.FullType);
      try
      {
        ServiceContainer.ModelService.Update(model);
      }
      catch { }
      if (!String.IsNullOrEmpty(model.PostReturnUrl))
        return Redirect(model.PostReturnUrl);
      return RedirectToAction("Index", "ModelManagement", new { @area = ConfigContainer.Systems.AdminPath, @id = key });
    }

    [HttpPost]
    //[Admin(adminRole: "ModelManagementDelete")]
    public ActionResult Delete(string deleteId, string type)
    {
      var start = DateTime.Now;
      ServiceContainer.ModelService.Delete(type, deleteId.MyTryConvert<long>());
      var end = DateTime.Now;
      var ms = (end - start).Milliseconds;
      if (ms < 1000)
      {
        System.Threading.Thread.Sleep(1000 - ms);
      }
      return RedirectToAction("Index", "ModelManagement", new { @area = ConfigContainer.Systems.AdminPath, @id = type });
    }
  }
}