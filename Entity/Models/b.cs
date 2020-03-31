using SDHC.Common.Entity.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
  [AllowChildren(
    ChildrenType = new Type[] { typeof(S1), typeof(S2) },
    TableList = new string[] { "Id", "Title", "E2Select" }
    //,
    //CreateRoles = new string[] { "Admin" },
    //EditRoles = new string[] { "Admin" }

    )]
  public class SCHCContent : BaseContent
  {
    [NotMapped]
    [InputType(EditorType = EnumInputType.Bool)]
    public virtual bool IsBool { get; set; }
    [InputType(EditorType = EnumInputType.FileUpload)]
    public virtual string Title_Title { get; set; }
  }
  [AllowChildren(
    Name = "ss 1"
    )]
  public class S1 : SCHCContent
  {

  }

  public class S1View : BaseViewModel
  {
    public string Title { get; set; }
  }

  [Table("S2")]
  public class S2 : SCHCContent
  {
    [InputType(EditorType = EnumInputType.FileUpload)]
    public string Title2 { get; set; }

    [InputType(EditorType = EnumInputType.DropDwon, MultiSelect = true, RelatedType = typeof(GenderSelect))]
    public string Gender { get; set; }

    [InputType(EditorType = EnumInputType.DropDwon, RelatedType = typeof(E2))]
    public long E2Select { get; set; }
  }
  [AllowChildren(ChildrenType = new Type[] { typeof(GenderSelect) })]
  public class SDHCBascSelect : BaseSelect
  {

  }

  public class GenderSelect : SDHCBascSelect
  {

  }
  public enum CC
  {
    C1 = 1,
    C2 = 2
  }
}
