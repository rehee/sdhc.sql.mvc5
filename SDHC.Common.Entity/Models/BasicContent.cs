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

  public abstract class BaseContent : IBasicSelect
  {
    public BaseContent()
    {
      if (GetRepo != null)
      {
        this.repo = GetRepo();
      }
    }
    [NotMapped]
    private IContent repo { get; }
    [Key]
    public long Id { get; set; }
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

    public virtual void Save()
    {
      if (Id <= 0)
      {
        repo.Contents.Add(this);
        repo.SaveChanges();
      }
    }
  }

  public class SCHCContent : BaseContent
  {
    public virtual string Title { get; set; }

    public virtual string Title_Title { get; set; }
  }
}
