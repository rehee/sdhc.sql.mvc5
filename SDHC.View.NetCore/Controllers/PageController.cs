using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDHC.View.NetCore.Controllers
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
      return Content(path);
    }
    public IActionResult Detail(long? id)
    {
      var model = ServiceContainer.ContentService.GetContent(id);
      return View($"Views/Pages/{model.GetType().Name}.cshtml", new ContentViewModal(model.ConvertModelToPost()));
    }
  }
}
