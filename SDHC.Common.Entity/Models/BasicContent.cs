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
    public string Title
    {
      get; set;
    }
  }

  public abstract class BaseContent : IInt64Key
  {
    [Key]
    [BaseProperty]
    public long Id { get; set; }
    [BaseProperty]
    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        var t = String.IsNullOrEmpty(value)|| String.IsNullOrWhiteSpace(value) ? Guid.NewGuid().ToString() : value.Trim();
        this.Url = t.Replace('/', '_').Replace(" ", "_");
        this._title = t;
      }
    }

    private string _title { get; set; }
    [IgnoreEdit]
    public virtual string Url
    {
      get; set;
    }
    [BaseProperty]
    public long DisplayOrder { get; set; }

    private DateTime? _createTime { get; set; }

    [BaseProperty]
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

    [BaseProperty]
    public long? ParentId { get; set; }
    [IgnoreEdit]
    public BaseContent Parent { get; set; }

    [NotMapped]
    [IgnoreEdit]
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
  }
}
