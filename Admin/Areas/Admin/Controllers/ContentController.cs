﻿using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{

  public class ContentController : Controller
  {
    [Admin(adminRole: "ContentIndex")]
    public ActionResult Index(long? id)
    {
      var content = ContentManager.GetContent(id);
      AllowChildrenAttribute childrenAttribute;
      if (content == null)
      {
        childrenAttribute = ContentManager.BasicContentType.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      else
      {
        childrenAttribute = content.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      ViewBag.childrenAttribute = childrenAttribute;
      var u = HttpContext.User;
      Func<IEnumerable<string>, bool> isInRole = b =>
       {
         if (G.AdminFree)
           return true;
         if (!u.Identity.IsAuthenticated)
           return false;
         return b.Any(c => u.IsInRole(c));
       };
      ViewBag.IsInCreateRoles = isInRole(childrenAttribute.CreateRoles);
      ViewBag.IsInReadRoles = isInRole(childrenAttribute.ReadRoles);
      ViewBag.IsInEditRoles = isInRole(childrenAttribute.EditRoles);
      ViewBag.IsInDeleteRoles = isInRole(childrenAttribute.DeleteRoles);
      ViewBag.IsInSortRoles = isInRole(childrenAttribute.SortRoles);
      return View(content);
    }
    [HttpPost]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentCreate")]
    public ActionResult PreCreate(long? ContentId, string FullType)
    {
      var content = ContentManager.GetPreCreate(ContentId, FullType);
      return View("Create", content);
    }
    [HttpPost]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentCreate")]
    public ActionResult Create(ContentPostModel model)
    {
      var content = model.ConvertToBaseModel() as BaseContent;
      ContentManager.CreateContent(content, content.ParentId);
      return RedirectToAction("Index");
    }
    [HttpGet]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentEdit")]
    public ActionResult Edit(long? id)
    {
      if (!id.HasValue)
      {
        return RedirectToAction("index");
      }
      var content = ContentManager.GetContent(id);
      if (content == null)
      {
        return RedirectToAction("index");
      }
      return View(content.ConvertModelToPost());
    }
    [HttpPost]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentEdit")]
    public ActionResult Edit(ContentPostModel model)
    {
      ContentManager.UpdateContent(model);
      return RedirectToAction("Index");
    }
    [Admin(adminRole: "ContentSort")]
    public ActionResult Sort(long? id)
    {
      var model = ContentManager.GetContentListView(id);
      return View(model);
    }
    [HttpPost]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentSort")]
    public ActionResult Sort(IEnumerable<ContentSortPostModel> input)
    {
      ContentManager.UpdateContentOrder(input);
      return RedirectToAction("Sort");
    }
    [HttpPost]
    [ValidateInput(false)]
    [Admin(adminRole: "ContentDelete")]
    public ActionResult Delete(long? id)
    {
      if (!id.HasValue)
        return RedirectToAction("Index", "Content", new { @area = G.AdminPath });
      var content = ContentManager.GetContent(id);
      if (content == null)
        return RedirectToAction("Index", "Content", new { @area = G.AdminPath });
      ContentCruds.Delete(id.Value);
      return RedirectToAction("Index");
    }

  }
}