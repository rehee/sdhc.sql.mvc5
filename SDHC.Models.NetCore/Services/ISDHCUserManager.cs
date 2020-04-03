using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDHC.Models.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Services
{
  public interface ISDHCUserManager<TUser> where TUser : SDHCUser, new()
  {
    Type BaseUser { get; }
    UserManager<TUser> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<TUser> SignInManager { get; }
    ContentTableHtmlView GetContentTableHtmlView(IEnumerable<TUser> users);
    TUser CreateUser(string email, string userName);
    Task<TUser> GetUserAsync(ClaimsPrincipal principal);
  }

  public class SDHCUserManager<TUser> : ISDHCUserManager<TUser> where TUser : SDHCUser, new()
  {
    public SDHCUserManager(UserManager<TUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<TUser> signInManager)
    {
      UserManager = userManager;
      RoleManager = roleManager;
      SignInManager = signInManager;
      BaseUser = typeof(TUser);
    }
    public Type BaseUser { get; }
    public UserManager<TUser> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }
    public SignInManager<TUser> SignInManager { get; }

    public TUser CreateUser(string email, string userName)
    {
      return new TUser()
      {
        Email = email,
        UserName = userName,
        EmailConfirmed = true
      };
    }

    public ContentTableHtmlView GetContentTableHtmlView(IEnumerable<TUser> users)
    {
      var children = users == null ? Enumerable.Empty<TUser>() : users;
      var allowChild = BaseUser.GetObjectCustomAttribute<AllowChildrenAttribute>();
      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { };
      long id = 0;
      var rowItems = children.Select(b =>
      {
        id++;
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(id, values, b.GetType().GetRealType(), 0, b.Id);
      }).ToList();
      var result = new ContentTableHtmlView();
      if (allowChild != null && allowChild.DisableDelete)
      {
        result.DisableDelete = true;
      }
      result.TableHeaders = additionalList.Select(b => BaseUser.GetPropertyLabelByKey(b)).ToList();
      result.Rows = rowItems;
      return result;
    }

    public async Task<TUser> GetUserAsync(ClaimsPrincipal principal)
    {
      var user = await UserManager.GetUserAsync(principal);
      if (user == null)
        return default(TUser);
      return user as TUser;
    }
  }
}
