using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDHC.View.NetCore.Controllers
{
  public class FilesController : Controller
  {
    // GET: /<controller>/
    public IActionResult Index(string path)
    {
      var file = ServiceContainer.SDHCFileService.BasePath + "\\" + ConfigContainer.Systems.FileUploadPath + "\\" + path;
      if (!System.IO.File.Exists(file))
        goto returnNull;
      Func<bool> needReturnNull = () =>
      {
        var paths = path.Split('/').ToList();
        if (paths.Count < 2)
        {
          return false;
        }
        var folder = paths[paths.Count - 2];
        try
        {
          var folderInfo = ServiceContainer.SecretService.Decrypt(folder);
          if (folderInfo == null)
            return false;
          var propertys = folderInfo.Split("_");
          if (propertys.Length != 2)
            return false;
          var type = Type.GetType(propertys[0]);
          if (type == null)
            return false;
          var inputProperty = type.GetProperties().FirstOrDefault(b => b.Name == propertys[1]);
          if (inputProperty == null)
            return false;
          var attribute = inputProperty.GetCustomAttribute<InputTypeAttribute>();
          if (attribute == null)
            return false;
          return false;
        }
        catch
        {
          return false;
        }
      };

      if (needReturnNull())
        goto returnNull;

      return PhysicalFile(file, "application/octet-stream");

      returnNull:
      Response.StatusCode = 404;
      return Content(path);
    }
  }
}
