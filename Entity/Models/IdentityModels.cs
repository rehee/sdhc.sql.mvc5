using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SDHC.Common.Entity.Models;

namespace WebSQL.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : MyUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }

  public class MyUser: SDHCUser
  {
    public string AAA { get; set; }
  }

  public class ApplicationDbContext : IdentityDbContext<SDHCUser>, IContent
  {
    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {

    }
    public DbSet<MyUser> MyUsers { get; set; }
    public DbSet<BaseContent> Contents { get; set; }
    public DbSet<S1> S1s { get; set; }
    public DbSet<S2> S2s { get; set; }


    public DbSet<BaseSelect> Selects { get; set; }

    public DbSet<GenderSelect> GenderSelects { get; set; }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }

  }
}