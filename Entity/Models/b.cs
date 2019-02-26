using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class B1: BaseContent
  {
    [Display(Name = "t1")]
    public override string Title { get; set; }
  }
  public class B2 : BaseContent
  {
    [Display(Name = "t2")]
    public override string Title { get; set; }
  }
  public class S1 : SCHCContent
  {
    [Display(Name = "t1")]
    public override string Title { get; set; }
  }
  public class S2 : SCHCContent
  {
    [Display(Name = "t2")]
    public override string Title { get; set; }
  }
}
