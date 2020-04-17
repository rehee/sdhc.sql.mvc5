using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Models;
using SDHC.Common.Configs;
using SDHC.Common.Entity.Models;
using SDHC.NetCore.Models.Models;

namespace Controllers
{
  public class HomeController : Controller
  {
    public HomeController(MyDBContext db)
    {
      db.BaseContentModels.FirstOrDefault();
    }
    public async Task<IActionResult> Index()
    {
      var model = (await ServiceContainer.ModelService.ReadAsync<About>(b => true)).ToList();
      return View(null);
    }
    public IActionResult Files()
    {
      var provider = new FileExtensionContentTypeProvider();
      string contentType;
      var fileName = ServiceContainer.SDHCFileService.BasePath + @"\FileUpload\lj2.jpg";
      var stream = new FileStream(fileName, FileMode.Open);
      if (!provider.TryGetContentType(fileName, out contentType))
      {
        contentType = "application/octet-stream";
      }
      return File(stream, contentType);
    }
    public IActionResult Edit(TestFile input)
    {
      return Content("");
    }
    [HttpPost]
    public IActionResult Test(ContentViewModelSummary model)
    {
      return Content("");
    }
  }
  public class TestFile
  {
    public IEnumerable<string> name { get; set; }
  }
}