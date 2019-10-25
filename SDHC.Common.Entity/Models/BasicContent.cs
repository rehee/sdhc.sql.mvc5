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

  public abstract class BaseSelect : IBasicSelect
  {
    [Key]
    public long Id { get; set; }
    [InputType(EditorType = EnumInputType.Text, SortOrder = 9999)]
    public virtual string Title { get; set; }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
    }
    public override string ToString()
    {
      return this.DisplayName();
    }
  }

  public abstract class BaseContent : IContentModel
  {
    public BaseContent()
    {
      if (Id <= 0)
      {
        this.CreateTime = DateTime.UtcNow;
        this.DisplayOrder = ModelManager.Read<BaseContent>(b => true).Count() + 1;
      }
    }
    [Key]
    [BaseProperty]
    public long Id { get; set; }
    [BaseProperty]
    public virtual string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        var t = String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) ? Guid.NewGuid().ToString() : value.Trim();
        t = t.Replace('/', '_').Replace(" ", "-");
        this.Url = System.Uri.EscapeUriString(t);
        this._title = t;
      }
    }

    private string _title { get; set; }
    [IgnoreEdit]
    public virtual string Url
    {
      get; set;
    }

    private long _displayOrder { get; set; }

    [BaseProperty]
    public long DisplayOrder
    {
      get
      {
        if (this.Id == 0)
          return ModelManager.Read<BaseContent>(b => true).Count() + 1;
        return this._displayOrder;
      }
      set
      {
        _displayOrder = value;
      }
    }

    private DateTime? _createTime { get; set; }

    [BaseProperty]
    public virtual DateTime? CreateTime
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
    [ForeignKey("ThisParent")]
    public long? ParentId { get; set; }

    [IgnoreEdit]
    public virtual BaseContent ThisParent { get; set; }

    [NotMapped]
    [IgnoreEdit]
    public IContentModel Parent
    {
      get
      {
        var p = (IContentModel)this.ThisParent;
        if (p == null && this.ParentId.HasValue && ParentId.Value >0)
        {
          p = ContentCruds.Read<BaseContent>(ParentId.Value);
        }
        return p;
      }
    }

    [NotMapped]
    [IgnoreEdit]
    private IEnumerable<IContentModel> _parents { get; set; }

    [NotMapped]
    [IgnoreEdit]
    public IEnumerable<IContentModel> Parents
    {
      get
      {
        if (_parents != null)
          return _parents;
        var list = new List<IContentModel>();
        var p = this.Parent;
        do
        {
          if (p == null)
          {
            break;
          }
          list.Add(p);
          p = p.Parent;
        } while (p != null);
        _parents = list;
        return list;
      }
    }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
    }
    public override string ToString()
    {
      return this.DisplayName();
    }
    [NotMapped]
    [IgnoreEdit]
    public IEnumerable<IContentModel> Children
    {
      get
      {
        return ModelManager.Read<BaseContent>(b => b.ParentId == this.Id)
          .OrderBy(b => b.DisplayOrder).ToList();
      }
    }
  }

  public abstract class BaseModel : IBasicModel
  {
    [Key]
    [BaseProperty]
    public long Id { get; set; }
    [InputType(EditorType = EnumInputType.Text, SortOrder = 9999)]
    public virtual string Title { get; set; }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
    }
    public override string ToString()
    {
      return this.DisplayName();
    }
  }

}
