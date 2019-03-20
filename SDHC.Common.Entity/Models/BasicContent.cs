using SDHC.Common.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IBasicContent : IInt64Key
  {

  }
  public interface IBasicSelect : IInt64Key
  {

  }

  public abstract class BaseSelect : IBasicSelect
  {
    [Key]
    public long Id { get; set; }
    public string Title { get; set; }
  }

  public abstract class BaseContent : IInt64Key
  {
    [Key]
    public long Id { get; set; }
    public virtual string Title { get; set; }
    public virtual string Url { get; set; }
    public long DisplayOrder { get; set; }

    private DateTime? _createTime { get; set; }
    public DateTime? CreateTime
    {
      get
      {
        if (this.Id <= 0)
        {
          return DateTime.UtcNow;
        }
        return _createTime;
      }
      set
      {
        _createTime = value;
      }
    }

    public long? ParentId { get; set; }
    public BaseContent Parent { get; set; }

    [NotMapped]
    public IEnumerable<BaseContent> Parents
    {
      get
      {
        var list = new List<BaseContent>();
        var p = this.Parent;
        do
        {
          if (p == null)
          {
            break;
          }
          list.Add(p);
        } while (p != null);
        return list;
      }
    }

    [NotMapped]
    public static Func<IContent> GetRepo { get; set; } = null;
  }
}
