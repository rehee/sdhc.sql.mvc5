using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models.ViewModels
{
  public class ContentSortPostModel
  {
    public long? id { get; set; }
    public long? parentId { get; set; }
    public long? displayOrder { get; set; }
  }
}
