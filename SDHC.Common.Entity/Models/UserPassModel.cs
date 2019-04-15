using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public class UserPassModel : IdentityUser, IPassModel
  {
    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }

  public class SDHCUser : IdentityUser, IDisplayName, IStringKey
  {
    public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<SDHCUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
    public virtual string DisplayName()
    {
      return String.IsNullOrEmpty(this.UserName) ? this.Id : this.UserName;
    }
    public override string ToString()
    {
      return this.DisplayName();
    }

    [IgnoreEdit]
    public virtual TUser GetCustomUser<TUser>() where TUser : SDHCUser
    {
      return this as TUser;
    }
  }


}


