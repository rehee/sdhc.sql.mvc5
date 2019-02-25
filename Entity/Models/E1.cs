using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E1
  {
    [Key]
    public Int64 E1_Id { get; set; }
    [Display( Name ="Page title here")]
    public string Title { get; set; }

    public string Name { get; set; }
  }
}
