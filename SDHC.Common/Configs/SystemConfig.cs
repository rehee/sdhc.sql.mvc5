using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Configs
{
  public class SystemConfig
  {
    public bool UseContentRouter { get; set; } = false;
    public bool AdminFree { get; set; } = false;
    public int SortChildLevel { get; set; } = 4;
    public string FileUploadPath { get; set; } = "files";
    public string AdminTitle { get; set; } = "";
    public string AdminCopyright { get; set; } = "";
    public string AdminPath { get; set; } = "Admin";
    public string SuperUserRole { get; set; } = "Admin";
    public string AdminRole { get; set; } = "Admin";
    public string AdminPolicy { get; set; } = "AdminPolicy";
    public bool UserNameIsNotEmail { get; set; } = true;
    public int DefaultLanguage { get; set; } = 0;
    public string ContentViewPath { get; set; } = "Pages";
    public string SharedLinkViewPath { get; set; } = "SharedLinks";
    public string ContentPageUrl { get; set; } = "pages";
    public int DefaultTablePageSize { get; set; } = (int)EnumTablePageSize.L10;
    public int DeleteMinTime { get; set; } = 1000;

    public string EmailHost { get; set; }
    public int EmailPort { get; set; }
    public string EmailUser { get; set; }
    public string EmailPassword { get; set; }
    public bool EmailSSL { get; set; }

    public string SecretdeKey { get; set; } = "zV7JkQs7";
    public string SecretdeIV { get; set; } = "ovIeh78A";

    public bool AutoConfirmEmail { get; set; } = true;

    public bool ViewForEveryLang { get; set; } = false;
  }
}
