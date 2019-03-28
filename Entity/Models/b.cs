using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  [AllowChildren(
    ChildrenType = new Type[] { typeof(S1), typeof(S2) }
    //,
    //CreateRoles = new string[] { "Admin" },
    //EditRoles = new string[] { "Admin" }
    )]
  public class SCHCContent : BaseContent
  {
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
    [InputType(EditorType = EnumInputType.Password)]
    public string Title2 { get; set; }

    [InputType(EditorType = EnumInputType.DropDwon, MultiSelect = false, RelatedType = typeof(GenderSelect))]
    public string Gender { get; set; }
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
