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
      return View();
    }
    [HttpPut]
    public ActionResult Edit()
    {
      return View();
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