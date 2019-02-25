using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IPassModel
  {
    List<ContentProperty> Properties { get; set; }
  }
  public class ContentPostModel : IPassModel
  {
    public string FullType { get; set; }
    public Assembly ThisAssembly { get; set; }
    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }
}
