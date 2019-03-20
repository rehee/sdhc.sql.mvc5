using SDHC.Common.Entity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SDHC.Common.Entity.Models
{
  public class ContentProperty
  {
    public string Key { get; set; } = "";
    public string Value { get; set; } = "";
    public IEnumerable<string> MultiValue { get; set; } = new List<string>();
    public string ValueType { get; set; } = "";
    public string Title { get; set; } = "";
    public EnumInputType EditorType { get; set; } = EnumInputType.Text;
    public bool MultiSelect { get; set; } = false;
    public IEnumerable<DropDownViewModel> SelectItems { get; set; } = new List<DropDownViewModel>();
    public HttpPostedFileBase File { get; set; }
    public bool RemoveFile { get; set; } = false;
    public bool BaseProperty { get; set; } = false;
    public bool IgnoreProperty { get; set; } = false;
    public bool CustomProperty { get; set; } = false;

    public int RangeMin { get; set; } = 0;
    public int RangeMax { get; set; } = 100;

  }
  public class DropDownViewModel
  {
    public string Name { get; set; } = "";
    public string Value { get; set; }
    public bool Select { get; set; } = false;
  }
}
