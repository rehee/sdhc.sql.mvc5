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
    public string FullType { get; set; }
    public string ThisAssembly { get; set; }
    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }

  public class SDHCUser : IdentityUser, IDisplayName, IStringKey
  {
    public SDHCUser()
    {
    }
    public SDHCUser(IStringKey input) : this()
    {

    }
    public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync<TUser>(UserManager<TUser> manager) where TUser : SDHCUser
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this as TUser, DefaultAuthenticationTypes.ApplicationCookie);
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

    private DateTime? _createDate { get; set; }

    [InputType(EditorType = EnumInputType.DateTime)]
    [HideEdit]
    public virtual DateTime? CreateDate
    {
      get
      {
        if (_createDate == null)
        {
          _createDate = DateTime.UtcNow;
        }
        return _createDate;
      }
      set
      {
        if (value.HasValue)
          _createDate = value;
      }
    }

    [IgnoreEdit]
    public string WeChatOpenId { get; set; }
  }


}


