using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SDHC.Common.EntityCore.Models
{
  public abstract class BaseSelect : AbstractBaseSelect
  {
    [Key]
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
    [NotMapped]
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
  public abstract class BaseSharedContent : BaseModel, ISharedContent
  {
    [HideEdit]
    public int Lang { get; set; }
  }
  public abstract class BaseSharedLink : BaseSharedContent, ISharedLink
  {
    [InputType(EditorType = EnumInputType.Number)]
    public int DisplayOrder { get; set; }

    [InputType(EditorType = EnumInputType.Bool)]
    public bool Displayed { get; set; }


  }
}
