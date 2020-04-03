using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SDHC.Common.Services;
using SDHC.Models.NetCore.Models;

namespace Core31.Controllers
{
  public class HomeController : Controller
  {
    public HomeController(RoleManager<IdentityRole> manager)
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