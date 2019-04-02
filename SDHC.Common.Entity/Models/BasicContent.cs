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
  public interface IBasicContent : IInt64Key, IDisplayName
  {

  }
  public interface IBasicSelect : IBasicContent
  {

  }

  public abstract class BaseSelect : IBasicSelect
  {
    [Key]
    public long Id { get; set; }
    [InputType(EditorType = Types.EnumInputType.Text, SortOrder = 9999)]
    public virtual string Title
    {
      get; set;
    }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
    }
  }

  public abstract class BaseContent : IBasicContent
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
        var t = String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) ? Guid.NewGuid().ToString() : value.Trim();
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
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
    }
  }

  public abstract class BaseModel : IBasicContent
  {
    [Key]
    [BaseProperty]
    public long Id { get; set; }
    [InputType(EditorType = Types.EnumInputType.Text, SortOrder = 9999)]
    public virtual string Title { get; set; }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.Title) ? this.Id.ToString() : this.Title;
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
        var type = model.GetType();
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
