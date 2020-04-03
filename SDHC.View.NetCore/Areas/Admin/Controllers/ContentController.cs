using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using SDHC.Common.EntityCore.Models;
using SDHC.Models.NetCore.Attributes;

namespace View.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ContentController : Controller
  {
    //[Admin(adminRole: "ContentIndex")]
    [Authorize]
    public IActionResult Index(long? id)
    {
      var content = ServiceContainer.ContentService.GetContent(id.HasValue && id.Value > 0 ? id : null);
      AllowChildrenAttribute childrenAttribute;
      if (content == null)
      {
        childrenAttribute = ServiceContainer.ContentService.BaseIContentModelType.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      else
      {
        childrenAttribute = content.GetObjectCustomAttribute<AllowChildrenAttribute>();
      }
      ViewBag.childrenAttribute = childrenAttribute;
      var u = HttpContext.User;
      Func<IEnumerable<string>, bool> isInRole = b =>
      {
        if (b == null || !b.Any())
          return true;
        if (ConfigContainer.Systems.AdminFree)
          return true;
        if (!u.Identity.IsAuthenticated)
          return false;
        return b.Any(c => u.IsInRole(c));
      };
      ViewBag.IsInCreateRoles = childrenAttribute != null ? isInRole(childrenAttribute.CreateRoles) : true;
      ViewBag.IsInReadRoles = childrenAttribute != null ? isInRole(childrenAttribute.ReadRoles) : true;
      ViewBag.IsInEditRoles = childrenAttribute != null ? isInRole(childrenAttribute.EditRoles) : true;
      ViewBag.IsInDeleteRoles = childrenAttribute != null ? isInRole(childrenAttribute.DeleteRoles) : true;
      ViewBag.IsInSortRoles = childrenAttribute != null ? isInRole(childrenAttribute.SortRoles) : true;
      return View(content);
    }
    [HttpPost]
    //[Admin(adminRole: "ContentCreate")]
    public ActionResult PreCreate(long? ContentId, string FullType)
    {
      var content = ServiceContainer.ContentService.GetPreCreate(ContentId, FullType);
      return View("Create", content);
    }
    [HttpPost]
    [Authorize(Roles = "1")]
    //[Admin(adminRole: "ContentCreate")]
    public ActionResult Create(ContentPostModel model)
    {
      var content = model.ConvertToBaseModel() as BaseContent;
      ServiceContainer.ContentService.CreateContent(content, content.ParentId);
      return RedirectToAction("Index");
    }
    [HttpGet]
    //[Admin(adminRole: "ContentEdit")]
    public ActionResult Edit(long? id)
    {
      if (!id.HasValue)
      {
        return RedirectToAction("index");
      }
      var content = ServiceContainer.ContentService.GetContent(id);
      if (content == null)
      {
        return RedirectToAction("index");
      }
      return View(content.ConvertModelToPost());
    }
    [HttpPost]
    //[Admin(adminRole: "ContentEdit")]
    public ActionResult Edit(ContentPostModel model)
    {
      ServiceContainer.ContentService.UpdateContent(model);
      return RedirectToAction("Index");
    }
    //[Admin(adminRole: "ContentSort")]
    public ActionResult Sort(long? id)
    {
      var model = ServiceContainer.ContentService.GetContentListView(id);
      return View(model);
    }
    [HttpPost]
    //[Admin(adminRole: "ContentSort")]
    public ActionResult Sort(IEnumerable<ContentSortPostModel> input)
    {
      ServiceContainer.ContentService.UpdateContentOrder(input);
      return RedirectToAction("Sort");
    }
    [HttpPost]
    //[Admin(adminRole: "ContentDelete")]
    public ActionResult Delete(long? id)
    {
      if (!id.HasValue)
        return RedirectToAction("Index", "Content", new { @area = ConfigContainer.Systems.AdminPath });
      var content = ServiceContainer.ContentService.GetContent(id);
      if (content == null)
        return RedirectToAction("Index", "Content", new { @area = ConfigContainer.Systems.AdminPath });
      CrudContainer.CrudContent.Delete(id.Value);
      return RedirectToAction("Index");
    }

  }
}
