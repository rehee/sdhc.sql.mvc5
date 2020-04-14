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
  }

  public class HomePage : BaseContentModel
  {

  }
  public class BaseSelectModel : BaseSelect
  {
  }

  public class OurService : BaseModel
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; }
  }

  public class Home : AbstractBaseModel, ISharedContent
  {
    public int Lang { get; set; }

    [InputType(EditorType = EnumInputType.FileUpload)]
    public string HomeLogo { get; set; }
  }
}
