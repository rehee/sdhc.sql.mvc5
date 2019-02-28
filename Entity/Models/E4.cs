using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E4
  {
    [Key]
    public long Id { get; set; }

    public List<long> Lll { get; set; } = new List<long>();

  }
}
