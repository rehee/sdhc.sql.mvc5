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
    private readonly IViewNameService viewNameService;

    private ISDHCLanguageService lang { get; }
    private int currentLang { get; }
    public PageController(ISDHCLanguageService lang, IViewNameService viewNameService)
    {
      this.lang = lang;
      this.viewNameService = viewNameService;
      currentLang = this.lang.GetLang();

    }
    public IActionResult Index(string path)
    {
      var model = ServiceContainer.ContentService.GetContent(path, currentLang);
      var viewName = viewNameService.ViewName(model);
      return View(viewName, new ContentViewModal(model.ConvertModelToPost(), "ContentModel"));
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
        return View(viewNameService.ViewName(model), model);
      }
      catch { }
      return Content("");
    }
  }
}
