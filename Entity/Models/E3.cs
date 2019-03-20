using Newtonsoft.Json;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Entity.Models
{
  public class E3 : IInt64Key
  {
    [Key]
    public long Id { get; set; }
    public long ParentId { get; set; }
    public string PageUrl { get; set; }
    public string FullType { get; set; }

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


   

    public static Expression<Func<E3, bool>> TT()
    {
      return p => true;
    }
    [Column]
    public string TList { get; set; } = "";
  }
}
