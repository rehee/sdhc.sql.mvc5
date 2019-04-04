using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models.ViewModels
{
  public class ContentListView
  {
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public long? ParentId { get; set; }
    public string Icon { get; set; } = "";
    public long DisplayOrder { get; set; }
    public List<ContentListView> Children { get; set; } = new List<ContentListView>();
  }
}
