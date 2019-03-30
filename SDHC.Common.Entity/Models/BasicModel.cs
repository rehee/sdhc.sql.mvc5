using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IInt64Key
  {
    Int64 Id { get; set; }
  }

  public interface IDisplayName
  {
    string DisplayName();
  }
}
