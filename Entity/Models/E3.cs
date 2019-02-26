using Linq.PropertyTranslator.Core;
using Microsoft.Linq.Translations;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
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

    [NotMapped]
    public IEnumerable<long> ListValue => fullNameExpression.Evaluate(this);
    //{
    //  get
    //  {
    //    return fullNameExpression.Evaluate(this);
    //  }
    //  set
    //  {
    //    TList = String.Join(",", value);
    //  }
    //}
    public static IEnumerable<long> convertTo(string value)
    {
      if (String.IsNullOrEmpty(value))
      {
        return Enumerable.Empty<long>();
      }
      return value.Split(',').Select(b =>
      {
        var longValue = long.TryParse(b, out long result);
        return result;
      }).ToList();
    }


    private static readonly CompiledExpressionMap<E3, IEnumerable<long>> fullNameExpression =
        Linq.PropertyTranslator.Core.DefaultTranslationOf<E3>.Property(p => p.ListValue)
          .Is(p => Enumerable.Empty<long>());

    [Column]
    public string TList { get; set; }
  }
}
