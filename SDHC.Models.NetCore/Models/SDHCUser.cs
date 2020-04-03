using Microsoft.AspNetCore.Identity;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Models
{
  public class UserPassModel : IdentityUser, IPassModel
  {
    public string FullType { get; set; }
    public string ThisAssembly { get; set; }
    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
  }

  public class SDHCUser : IdentityUser, IDisplayName, IStringKey
  {
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

    public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
  }
  public class IdentityRoleUser : IdentityRole
  {
    public virtual ICollection<IdentityUserRole<string>> Users { get; set; }
  }
}
