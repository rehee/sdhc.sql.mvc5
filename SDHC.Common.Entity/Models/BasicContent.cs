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

  public abstract class BaseSelect : AbstractBaseSelect
  {
    [Key]
    [BaseProperty]
    public override long Id { get; set; }
  }
  public abstract class BaseContent : AbstractBaseContent
  {
    [Key]
    [BaseProperty]
    public override long Id { get; set; }
    
    [BaseProperty]
    [ForeignKey("ThisParent")]
    public override long? ParentId { get; set; }

    [IgnoreEdit]
    public virtual BaseContent ThisParent { get; set; }

    [IgnoreEdit]
    public override IContentModel Parent
    {
      get
      {
        var p = (IContentModel)this.ThisParent;
        if (p == null && this.ParentId.HasValue && ParentId.Value > 0)
        {
          p = ServiceContainer.ContentService.Find<IContentModel>(
            CrudContainer.CrudContent.BaseIContentModelType,
            ParentId.Value);
        }
        return p;
      }
    }
  }
  public abstract class BaseModel : AbstractBaseModel
  {
    [Key]
    [BaseProperty]
    public override long Id { get; set; }
  }

}
