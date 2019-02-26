using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IBasicContent: IInt64Key
  {

  }
  public interface IBasicSelect: IInt64Key
  {

  }

  public abstract class BaseContent : IBasicSelect
  {
    [Key]
    public long Id { get; set; }

    public virtual string Title { get; set; }
  }
  public class SCHCContent : BaseContent
  {

  }
}
