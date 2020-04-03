using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SDHC.Common.Services
{
  public class SDHCFileService : ISDHCFileService
  {
    private ISDHCFileConfig config { get; }
    public SDHCFileService(ISDHCFileConfig config)
    {
      this.config = config;
      this.BasePath = config.BasePath;
    }
    public bool SaveFile(object input, out string filePath, string extraPath = "")
    {
      filePath = "";
      var type = input.GetType().GetRealType();
      if (!(config.AllowedInputTypes(type)))
      {
        return false;
      }

      if (input == null)
        return false;
      var file = config.AllowedInputTypeMap[type];
      var fileName = file.GetName(input);
      if (String.IsNullOrEmpty(fileName))
        return false;
      try
      {

        var name = fileName.Split('.').LastOrDefault();
        if (String.IsNullOrEmpty(name))
          name = "";
        else
          name = '.' + name;
        fileName = $"{Guid.NewGuid().ToString()}{name}";
        string uploadPath;
        uploadPath = Path.Combine(config.FileUploadPath, extraPath, fileName);
        var path = Path.Combine(config.BasePath, uploadPath);
        var exist = Directory.Exists(Path.Combine(config.BasePath,
                                 config.FileUploadPath, extraPath));
        if (!exist)
        {
          Directory.CreateDirectory(Path.Combine(config.BasePath,
                                 config.FileUploadPath, extraPath));
        }
        file.GetSaveAs(input, path);
        filePath = uploadPath;
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
    }
    public string BasePath { get; }
    public void DeleteFile(string filePath, out bool success)
    {
      var path = Path.Combine(config.BasePath, filePath);
      success = false;
      if (!File.Exists(path))
      {
        success = true;
        return;
      }
      try
      {
        File.Delete(path);
        success = true;
      }
      catch { }
    }

  }

  public class SDHCFileConfig : ISDHCFileConfig
  {
    public SDHCFileConfig(
      string basePatch, string fileUploadPath, IDictionary<Type, SDHCSaveAble> allowedInputTypeMap)
    {
      this.BasePath = basePatch;
      this.FileUploadPath = fileUploadPath;
      this.AllowedInputTypeMap = allowedInputTypeMap;
    }
    public string BasePath { get; }
    public string FileUploadPath { get; set; }
    public bool AllowedInputTypes(Type type)
    {
      return AllowedInputTypeMap.ContainsKey(type);
    }
    public IDictionary<Type, SDHCSaveAble> AllowedInputTypeMap { get; }
  }
}