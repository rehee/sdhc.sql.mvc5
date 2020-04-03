using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public delegate bool SaveFile(object input, out string filePath, string extraPath = "");
  public delegate void DeleteFile(string filePath, out bool success);
}

namespace SDHC.Common.Services
{
  public interface ISDHCFileService
  {
    bool SaveFile(object input, out string filePath, string extraPath = "");
    void DeleteFile(string filePath, out bool success);
    string BasePath { get; }
  }

  public interface ISDHCFileConfig
  {
    string BasePath { get; }
    string FileUploadPath { get; set; }
    bool AllowedInputTypes(Type type);
    IDictionary<Type, SDHCSaveAble> AllowedInputTypeMap { get; }
  }

  public class SDHCSaveAble
  {
    public SDHCSaveAble(Func<object, string> getName, Action<object, string> getSaveAs)
    {
      this.GetName = getName;
      this.GetSaveAs = getSaveAs;
    }
    public Func<object, string> GetName { get; }
    public Action<object, string> GetSaveAs { get; }
  }
}
