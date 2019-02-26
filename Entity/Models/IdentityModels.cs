using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SDHC.Common.Entity.Models;
using QueryInterceptor;
using PropertyTranslator;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace WebSQL.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IContentIndex
  {
    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
      context = ((IObjectContextAdapter)this).ObjectContext;
    }
    ObjectContext context;
    public DbSet<ContentIndex> contentIndexs { get; set; }
    public DbSet<E1_2> e1s { get; set; }
    public DbSet<E2> e2s { get; set; }
    public DbSet<E3> e3s { get; set; }

    public DbSet<B1> b1 { get; set; }
    public DbSet<B2> b2 { get; set; }
    public DbSet<S1> s1 { get; set; }
    public DbSet<S2> s2 { get; set; }
    public DbSet<SCHCContent> sdc { get; set; }
    public IQueryable<E3> E3Table
    {
      get
      {
        var objectSet = context.CreateObjectSet<E3>("e3s");
        return objectSet.InterceptWith(new PropertyVisitor());
      }
    }
    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
  public class MyDataContext
  {
    ObjectContext context = ((IObjectContextAdapter)new ApplicationDbContext()).ObjectContext;

    public IQueryable<E3> E3Table
    {
      get
      {
        var objectSet = context.CreateObjectSet<E3>("e3s");
        return objectSet.InterceptWith(new PropertyVisitor());
      }
    }
  }
}