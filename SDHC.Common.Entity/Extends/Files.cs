using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SDHC.Common.Entity.Extends
{
  public static class Files
  {
    public static void SaveFile(this HttpPostedFileBase file, out string filePath, string extraPath = "")
    {
      filePath = "";
      if (file == null)
        return;
      try
      {
        var name = file.FileName.Split('.').LastOrDefault();
        if (String.IsNullOrEmpty(name))
          name = "";
        else
          name = '.' + name;
        var fileName = $"{Guid.NewGuid().ToString()}{name}";
        string uploadPath;
        uploadPath = Path.Combine(G.FileUploadPath, extraPath, fileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath);
        var exist = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),
                                 G.FileUploadPath, extraPath));
        if (!exist)
        {
          Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(),
                                 G.FileUploadPath, extraPath));
        }
        file.SaveAs(path);
        filePath = uploadPath;
      }
      catch { }
    }

    public static void DeleteFile(this string filePath, out bool success)
    {
      success = false;
      if (!File.Exists(filePath))
      {
        success = true;
        return;
      }
      try
      {
        var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        File.Delete(path);
        success = true;
      }
      catch { }
    }
  }
}
