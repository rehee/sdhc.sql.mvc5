using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Configs
{
  public class SystemConfig
  {
    public  bool UseContentRouter { get; set; } = false;
    public  bool AdminFree { get; set; } = false;
    public  int SortChildLevel { get; set; } = 4;
    public  string FileUploadPath { get; set; } = "files";
    public  string AdminTitle { get; set; } = "";
    public  string AdminCopyright { get; set; } = "";
    public  string AdminPath { get; set; } = "Admin";
    public  string SuperUserRole { get; set; } = "Admin";
    public  string AdminRole { get; set; } = "Admin";
    public  bool UserNameIsNotEmail { get; set; } = true;
    public  int DefaultLanguage { get; set; } = 0;
    public  string ContentViewPath { get; set; } = "";
    public  string ContentPageUrl { get; set; } = "pages";
    public  int DefaultTablePageSize { get; set; } = (int)EnumTablePageSize.L10;
  }
}
