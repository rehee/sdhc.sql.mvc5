using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SDHC.Common.EntityCore.Models;
using SDHC.NetCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
  public class MyDBContext : IdentityDbContext, IContent
  {
    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public DbSet<BaseContent> Contents { get; set; }
    public DbSet<BaseSelect> Selects { get; set; }
    public DbSet<BaseContentModel> BaseContentModels { get; set; }
    public DbSet<BaseSelectModel> BaseSelectModel { get; set; }

    public DbSet<OurService> OurServices { get; set; }

    public DbSet<Home> Homes { get; set; }

    public DbSet<SDHCUser> SDHCUsers { get; set; }
    //public DbSet<IdentityRole> IdentityRoles { get; set; }
    public Task<int> SaveChangesAsync()
    {
      return this.SaveChangesAsync(default(System.Threading.CancellationToken));
    }
  }
}
