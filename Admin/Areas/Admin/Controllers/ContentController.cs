using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{
  [Admin]
  public class ContentController : Controller
  {
    // GET: Admin/Content
    public ActionResult Index(long? id)
    {
      var content = ContentManager.GetContent(id);
      return View(content);
    }
    [HttpPost]
    public ActionResult PreCreate(long? ContentId, string FullType)
    {
      var content = ContentManager.GetPreCreate(ContentId, FullType);
      return View("Create", content);
    }
    [HttpPost]
    public ActionResult Create(ContentPostModel model)
    {
      var content = model.ConvertToBaseModel() as BaseContent;
      ContentManager.CreateContent(content, content.ParentId);
      return RedirectToAction("Index");
    }
    [HttpGet]
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
    public ActionResult Edit(ContentPostModel model)
    {
      ContentManager.UpdateContent(model);
      return RedirectToAction("Index");
    }

    public ActionResult Sort(long? id)
    {
      var model = ContentManager.GetContentListView(id);
      return View(model);
    }
    [HttpPost]
    public ActionResult Sort(IEnumerable<ContentSortPostModel> input)
    {
      ContentManager.UpdateContentOrder(input);
      return RedirectToAction("Sort");
    }
    [HttpPost]
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