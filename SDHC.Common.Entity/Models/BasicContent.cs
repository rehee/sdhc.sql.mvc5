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
        return (IContentModel)this.ThisParent;
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

  public abstract class BaseViewModel : IInt64Key
  {
    [BaseProperty]
    public virtual long Id { get; set; }
    [BaseProperty]
    public virtual string FullType { get; set; }
    [BaseProperty]
    public virtual string ThisAssembly { get; set; }

    public virtual Type ModelType()
    {
      return Type.GetType($"{this.FullType},{this.ThisAssembly}");
    }

    public virtual void SetViewModel(IInt64Key model = null)
    {
      if (model != null)
      {
        var type = model.GetType().GetRealType();
        FullType = type.FullName;
        ThisAssembly = type.Assembly.FullName;
        model.SetObjectByObject(this);
      }
    }

    public virtual object ConvertToModel(IInt64Key model = null)
    {
      if (model == null)
      {
        var type = Type.GetType($"{FullType},{ThisAssembly}");
        if (type == null)
        {
          return null;
        }
        model = Activator.CreateInstance(type) as IInt64Key;
      }
      else
      {
        if (model.Id != this.Id)
        {
          return null;
        }
      }
      this.SetObjectByObject(model);
      return model;
    }


  }
}
