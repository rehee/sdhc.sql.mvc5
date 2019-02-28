using Dapper;
using DelegateDecompiler;
using Linq.PropertyTranslator.Core;
using Microsoft.Linq.Translations;
using Newtonsoft.Json;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.SqlServer;
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
    [Computed]
    public IEnumerable<string> ListValue
    {
      get
      {
        //if (String.IsNullOrEmpty(this.TList))
        //{
        //  return Enumerable.Empty<long>();
        //}
        //return this.TList.Split(',').Select(b => long.Parse(b)).ToList();
        //return new List<long>();
        return fullNameExpression.Evaluate(this);
      }
      set
      {
        TList = String.Join(",", value);
      }
    }
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


    private static readonly Microsoft.Linq.Translations.CompiledExpression<E3, IEnumerable<string>> fullNameExpression =
        Microsoft.Linq.Translations.DefaultTranslationOf<E3>.Property(p => p.ListValue)
          .Is(p => p.TList.Split(','));
    //.Is(p => p.TList != null ? new long[1] { 1 } : new long[1] { 1 });
    //.Is(p => String.IsNullOrEmpty(p.TList) ? new long[1] { 1 } : new long[1] { 1 } );
    //p.TList.Split(',').Select(b => long.Parse(b)).ToArray()

    public static Expression<Func<E3, bool>> TT()
    {
      return p => true;
    }
    [Column]
    public string TList { get; set; } = "";
  }

  public class EHandler : SqlMapper.ITypeHandler
  {
    public object Parse(Type destinationType, object value)
    {
      return JsonConvert.DeserializeObject(value.ToString(), destinationType);
    }

    public void SetValue(IDbDataParameter parameter, object value)
    {
      parameter.Value = (value == null)
      ? (object)DBNull.Value
        : JsonConvert.SerializeObject(value);
        parameter.DbType = DbType.String;
    }
  }
}
