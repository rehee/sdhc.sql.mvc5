using Microsoft.AspNet.Identity;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static partial class G
  {
    [Config]
    public static bool UseContentRouter { get; set; } = false;
    [Config]
    public static bool AdminFree { get; set; } = false;
    [Config]
    public static int SortChildLevel { get; set; } = 4;
    [Config]
    public static string FileUploadPath { get; set; } = "files";

    [Config]
    public static string AdminTitle { get; set; } = "";
    [Config]
    public static string AdminCopyright { get; set; } = "";
    [Config]
    public static string AdminPath { get; set; } = "Admin";
    [Config]
    public static string SuperUserRole { get; set; } = "Admin";
    [Config]
    public static string AdminRole { get; set; } = "Admin";
    [Config]
    public static int DeleteMinTime { get; set; } = 1000;

    public static Func<IUserStore<SDHCUser>> MongoDbIuserStore { get; set; }
  }
}
