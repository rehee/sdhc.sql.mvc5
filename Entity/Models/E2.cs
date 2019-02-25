using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E2 : IBasicContent
  {
    [Key]
    public Int64 Id { get; set; }
    public string Title { get; set; }

  }
}
