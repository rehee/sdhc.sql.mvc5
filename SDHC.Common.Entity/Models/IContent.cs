using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
  public interface IContent : ISave
  {
    DbSet<BaseContent> Contents { get; set; }
    DbSet<BaseSelect> Selects { get; set; }
  }
  public interface ISave
  {
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync();
  }
}
