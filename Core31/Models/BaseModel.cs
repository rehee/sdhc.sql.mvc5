using SDHC.Common.Entity.Models;
using SDHC.Common.EntityCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
  [AllowChildren(ChildrenType = new Type[] { typeof(BaseContentModel) })]
  public class BaseContentModel : BaseContent
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string FF { get; set; }
    [InputType(EditorType = EnumInputType.Text)]
    public string FF2 { get; set; }

    [NotMapped]
    [InputType(EditorType = EnumInputType.SharedLink, RelatedType = typeof(OurService))]
    public string OurServices { get; set; }
  }

  public class HomePage : BaseContentModel
  {
    public string Home2 { get; set; }
  }
  public class BaseSelectModel : BaseSelect
  {
  }
  [AllowChildren(TableList = new string[] { "Image" }, TableImageList = new string[] { "Image" })]
  public class OurService : BaseSharedLink
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; set; }
  }

  public class Home : BaseSharedContent
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string HomeLogo { get; set; }
  }
}
