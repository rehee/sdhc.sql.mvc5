using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public class UserPassModel : IdentityUser, IPassModel
  {
    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }
}
