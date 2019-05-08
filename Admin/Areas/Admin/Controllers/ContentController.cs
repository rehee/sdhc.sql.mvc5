using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{

  public class ContentController : Controller
  {
    [Admin(adminRole: "ContentIndex")]
    public ActionResult Index(long? id)
    {
      var content = ContentManager.GetContent(id);
      string CreateRole = "";
      string ReadRole = "";
      string UpdateRole = "";
      string DeleteRole = "";
      string SortRole = "";
      AllowChildrenAttribute childrenAttribute;
      if (content == null)
      {
        childrenAttribute = ContentManager.BasicContentType.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      else
      {
        childrenAttribute = content.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      if (childrenAttribute != null)
      {
        CreateRole = String.Join(",", childrenAttribute.CreateRoles);
        ReadRole = String.Join(",", childrenAttribute.ReadRoles);
        UpdateRole = String.Join(",", childrenAttribute.EditRoles);
        DeleteRole = String.Join(",", childrenAttribute.DeleteRoles);
        SortRole = String.Join(",", childrenAttribute.SortRoles);
      }
      ViewBag.CreateRole = CreateRole;
      ViewBag.ReadRole = ReadRole;
      ViewBag.UpdateRole = UpdateRole;
      ViewBag.DeleteRole = DeleteRole;
      ViewBag.SortRole = SortRole;
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