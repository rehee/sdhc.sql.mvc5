using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public interface IContent : ISave
  {
    DbSet<BaseContent> Contents { get; set; }
  }
  public interface ISave
  {
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync();
  }
}
