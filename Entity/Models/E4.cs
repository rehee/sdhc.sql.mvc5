using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E4
  {
    [Key]
    public long Id { get; set; }

    public string Lll { get; set; }

    [NotMapped]
    public List<long> GList
    {
      get
      {
        if (string.IsNullOrEmpty(this.Lll))
        {
          return new List<long>();
        }
        return JsonConvert.DeserializeObject<List<long>>(this.Lll);
      }
      set
      {
        this.Lll = JsonConvert.SerializeObject(value);
      }
    }
  }
}
