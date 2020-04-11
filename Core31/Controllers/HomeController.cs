using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.NetCore.Models.Models;

namespace Controllers
{
  public class HomeController : Controller
  {
    public HomeController()
    {
      
    }
    public IActionResult Index()
    {
      return View();
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
  }
  public class TestFile
  {
  }
}