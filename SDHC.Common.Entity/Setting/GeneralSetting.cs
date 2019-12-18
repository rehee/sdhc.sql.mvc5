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
    public static bool UserNameIsNotEmail { get; set; } = true;
    [Config]
    public static int DefaultLanguage { get; set; } = 0;
    [Config]
    public static string ContentViewPath { get; set; } = "";
    [Config]
    public static string ContentPageUrl { get; set; } = "pages";

    [Config]
    public static int DefaultTablePageSize { get; set; } = (int)EnumTablePageSize.L10;

    public static string GeneralModal { get; set; } = "GeneralModelModal";
    public static string ModalButton(string id) => $"data-toggle=modal data-target=#{id}";
  }
}
