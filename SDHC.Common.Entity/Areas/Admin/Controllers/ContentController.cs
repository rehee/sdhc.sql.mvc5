using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using System;
using System.Web.Mvc;

namespace WebSQL.Areas.Admin.Controllers
{
  public class ContentController : Controller
  {
    // GET: Admin/Content
    public ActionResult Index(long? id)
    {
      var content = ContentManager.GetContent(id);
      return View(content);
    }
    [HttpPost]
    public ActionResult PreCreate(long? ContentId,string FullType)
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
      return View();
    }

    [HttpPost]
    public ActionResult Delete(long id)
    {
      var content = ContentManager.GetContent(id);
      ContentCruds.Delete(id);
      return RedirectToAction("Index");
    }
  }
}