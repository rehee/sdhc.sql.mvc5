using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
  public class E2 : BaseModel
  {
    [InputType(EditorType = EnumInputType.Number, InputWidth = 48, Readonly = true)]
    public int MMM { get; set; } = 10;
    [InputType(EditorType = EnumInputType.Number, HasRangeMax = true, HasRangeMin = true, InputWidth = 48)]
    public int MMM2 { get; set; }
    [InputType(EditorType = EnumInputType.Number)]
    public int MMM3 { get; set; }
    [InputType(EditorType = EnumInputType.Currency)]
    public int MMM4 { get; set; }

    [InputType(EditorType = EnumInputType.Range, RangeMin = 0, RangeMax = 100)]
    public decimal Range { get; set; }
  }
}
