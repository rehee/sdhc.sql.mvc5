using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using SDHC.NetCore.Models.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDHC.NetCore.View.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Admin("ContentEdit")]
  public class SharedLinksController : Controller
  {
    private readonly IViewRenderService viewRender;

    public SharedLinksController(IViewRenderService viewRender)
    {
      this.viewRender = viewRender;
    }
    public async Task<IActionResult> GetSharedLinks(long id, string key)
    {
      var m = await ServiceContainer.ContentService.GetContentViewModel(id, "ContentModel");
      return Json(new { value = m?.GetSharedLinkPost(key)?.View?.Value });
    }

    public async Task<IActionResult> GetSharedView(long id, string key)
    {
      var m = await ServiceContainer.ContentService.GetContentViewModel(id, "ContentModel");
      var content = await viewRender.RenderToStringAsync(key, m);
      return Content(content);
    }
    [HttpPost]
    public async Task<IActionResult> Toggle(long id, string key, string fullType, string asm, long sharedLinksId)
    {
      try
      {
        var type = Type.GetType($"{fullType},{asm}");
        var inputType = type.GetProperties().FirstOrDefault(b => b.Name == key).GetCustomAttribute<InputTypeAttribute>();
        var toggle = ServiceContainer.ModelService.Find<ISharedLink>(inputType.RelatedType, sharedLinksId, out var db);
        toggle.Displayed = !toggle.Displayed;
        db.SaveChanges();
      }
      catch { }
      
      return await GetSharedLinks(id, key);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long id, string key, string fullType, string asm, long sharedLinksId)
    {
      try
      {
        var type = Type.GetType($"{fullType},{asm}");
        var inputType = type.GetProperties().FirstOrDefault(b => b.Name == key).GetCustomAttribute<InputTypeAttribute>();
        var toggle = ServiceContainer.ModelService.Find<ISharedLink>(inputType.RelatedType, sharedLinksId, out var db);
        ServiceContainer.ModelService.Delete<ISharedLink>(db, toggle);
      }
      catch { }
      return await GetSharedLinks(id, key);
    }
  }
}
