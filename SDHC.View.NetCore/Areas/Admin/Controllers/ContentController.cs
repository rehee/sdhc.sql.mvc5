using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using SDHC.Common.EntityCore.Models;
using SDHC.Models.NetCore.Attributes;

namespace View.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ContentController : Controller
  {
    LanguageConfig langConfig { get; }
    public ContentController(IOptions<LanguageConfig> lang)
    {
      this.langConfig = lang.Value;
    }
    [Admin(adminRole: "ContentIndex")]
    public IActionResult Index(long? id, int? lang)
    {
      var inputLang = langConfig.GetLangKey(lang);
      return View(ServiceContainer.ContentService.GetContentIndexViewModelByIdOrLang<BaseContent>(id, inputLang, HttpContext.User.IsInRole));
    }
    [HttpPost]
    [Admin(adminRole: "ContentCreate")]
    public ActionResult PreCreate(long? ContentId, string FullTypeAndAssembly, int? LangKey)
    {
      var content = ServiceContainer.ContentService.GetPreCreate(ContentId, FullTypeAndAssembly, langConfig.GetLangKey(LangKey));
      return View("Create", content);
    }
    [HttpPost]
    [Admin(adminRole: "ContentCreate")]
    public ActionResult Create(ContentPostModel model)
    {
      var content = model.ConvertToBaseModel() as BaseContent;
      ServiceContainer.ContentService.CreateContent(content, content.ParentId);
      return RedirectToAction("Index", "Content", new { @id = content.Id, @lang = content.Lang, @area = "Admin" });
    }
    [HttpGet]
    [Admin(adminRole: "ContentEdit")]
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
    [Admin(adminRole: "ContentEdit")]
    public ActionResult Edit(ContentPostModel model)
    {
      ServiceContainer.ContentService.UpdateContent(model);
      return RedirectToAction("Index");
    }
    [Admin(adminRole: "ContentSort")]
    public ActionResult Sort(long? id, int? lang)
    {
      var model = ServiceContainer.ContentService.GetContentListView(id, 0, langConfig.GetLangKey(lang));
      return View(model);
    }
    [HttpPost]
    [Admin(adminRole: "ContentSort")]
    public ActionResult Sort(IEnumerable<ContentSortPostModel> input)
    {
      ServiceContainer.ContentService.UpdateContentOrder(input);
      return RedirectToAction("Sort");
    }
    [HttpPost]
    [Admin(adminRole: "ContentDelete")]
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
