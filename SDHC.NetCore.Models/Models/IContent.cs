using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Models
{
  public interface IContent : ISave
  {
    DbSet<BaseContent> Contents { get; set; }
    DbSet<BaseSelect> Selects { get; set; }
  }
}
