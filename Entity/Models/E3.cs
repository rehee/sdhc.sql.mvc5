using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E3 : IInt64Key
  {
    [Key]
    public long Id { get; set; }
    public long ParentId { get; set; }
    public string PageUrl { get; set; }
    public string FullType { get; set; }
  }
}
