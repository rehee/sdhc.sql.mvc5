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
  public class S1 : SCHCContent
  {
    [Display(Name = "t1")]
    public override string Title { get; set; }
  }

  [Table("S2")]
  public class S2 : SCHCContent
  {
    [Display(Name = "t2")]
    public override string Title { get; set; }

    public string Title2 { get; set; }

    [InputType(EditorType = EnumInputType.DropDwon, MultiSelect = true, RelatedType = typeof(GenderSelect))]
    public string Gender { get; set; }
  }

  public class GenderSelect : BaseSelect
  {
    
  }
  public enum CC
  {
    C1 = 1,
    C2 = 2
  }
}
