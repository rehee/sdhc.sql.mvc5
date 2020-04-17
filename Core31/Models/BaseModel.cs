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
    [NotMapped]
    [InputType(EditorType = EnumInputType.SharedLink, RelatedType = typeof(Banner))]
    public string Banners { get; set; }
    [NotMapped]
    [InputType(EditorType = EnumInputType.SharedLink, RelatedType = typeof(About))]
    public string HomeAbout { get; set; }
    [NotMapped]
    [InputType(EditorType = EnumInputType.SharedLink, RelatedType = typeof(CaseStudy))]
    public string CaseStudies { get; set; }
    [NotMapped]
    [InputType(EditorType = EnumInputType.SharedLink, RelatedType = typeof(Partner))]
    public string Partners { get; set; }
  }
  public class Partner : BaseSharedLink
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; set; }
  }
  public class CaseStudy : BaseSharedLink
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; set; }
    [InputType(EditorType = EnumInputType.DateTime)]
    public DateTime EditDate { get; set; }
    public string AuthName { get; set; }
    [InputType(EditorType = EnumInputType.Number)]
    public int Comment { get; set; }
  }
  public class About : BaseSharedLink
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; set; }
    [InputType(EditorType = EnumInputType.Html)]
    public string Content { get; set; }

  }
  public class Banner : BaseSharedLink
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Image { get; set; }
    public string SubTitle { get; set; }
    public string Content { get; set; }

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
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string FootLogo { get; set; }
    public string FootAbout { get; set; }
    [InputType(EditorType = EnumInputType.TextArea)]
    public string SocialMedia { get; set; }
    [InputType(EditorType = EnumInputType.Html)]
    public string FootContactInfo { get; set; }
    [InputType(EditorType = EnumInputType.TextArea)]
    public string FootLinks { get; set; }
    [InputType(EditorType = EnumInputType.Html)]
    public string FootWechat { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }
    public string WorkingHours { get; set; }
  }
}
