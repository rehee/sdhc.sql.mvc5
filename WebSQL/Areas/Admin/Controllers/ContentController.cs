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
      return View();
    }
  }
}