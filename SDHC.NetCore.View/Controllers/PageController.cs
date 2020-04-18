using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;
using SDHC.NetCore.Models.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDHC.NetCore.View.Controllers
{
  public class PageController : Controller
  {
    private ISDHCLanguageService lang { get; }
    private int currentLang { get; }
    public PageController(ISDHCLanguageService lang)
    {
      this.lang = lang;
      currentLang = this.lang.GetLang();

    }
    public IActionResult Index(string path)
    {
      var model = ServiceContainer.ContentService.GetContent(path, currentLang);
      return View($"Views/Pages/{model.GetType().Name}.cshtml", new ContentViewModal(model.ConvertModelToPost(), "ContentModel"));
    }
    [Admin("ContentEdit")]
    public async Task<IActionResult> Detail(long? id)
    {
      try
      {
        if (!id.HasValue)
          return Content("");
        var model = await ServiceContainer.ContentService.GetContentViewModel(id.Value, "ContentModel");
        if (model == null)
          return Content("");
        ViewBag.IsReview = true;
        return View($"Views/Pages/{model.ViewPath}.cshtml", model);
      }
      catch { }
      return Content("");
    }
  }
}
