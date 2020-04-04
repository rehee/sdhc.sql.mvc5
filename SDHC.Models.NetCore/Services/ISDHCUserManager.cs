using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Responses;
using SDHC.Models.NetCore.Models;
using SDHC.Models.NetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Services
{
  public interface ISDHCUserManager<TUser> where TUser : IdentityUser, new()
  {
    Type BaseUser { get; }
    UserManager<TUser> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<TUser> SignInManager { get; }
    ContentTableHtmlView GetContentTableHtmlView(IEnumerable<TUser> users);
    TUser CreateUser(string email, string userName);
    Task<TUser> GetUserAsync(ClaimsPrincipal principal);
    IEnumerable<RoleNameAndUser> GetRoleAndUsers();
    UserCreateView GetUserView(UserCreateView input = null);
    Task SetRolesForUser(TUser user, IEnumerable<string> selectedRoles);
    Task<RolesIndexView> GetRolesIndexViewByRoleName(string roleName);
    Task CreateRole(string roleName, MethodResponse response = null);
    Task DeleteRole(string roleName, MethodResponse response = null);
    Task CreateUser(UserCreateView model, MethodResponse response = null);
    Task<UserCreateView> GetUserCreateViewById(string id);
    Task UpdateUserCreateView(UserCreateView model);
  }

  public class SDHCUserManager<TUser> : ISDHCUserManager<TUser> where TUser : SDHCUser, new()
  {
    private SystemConfig config;
    public SDHCUserManager(UserManager<TUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<TUser> signInManager,
      IOptions<SystemConfig> config)
    {

      UserManager = userManager;
      RoleManager = roleManager;
      SignInManager = signInManager;
      UserRoleStore = UserRoleStore;
      BaseUser = typeof(TUser);
      this.config = config.Value;
    }
    public Type BaseUser { get; }
    public UserManager<TUser> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }
    public SignInManager<TUser> SignInManager { get; }
    public IUserRoleStore<TUser> UserRoleStore { get; }

    public TUser CreateUser(string email, string userName)
    {
      return new TUser()
      {
        Email = email,
        UserName = userName,
        EmailConfirmed = config.AutoConfirmEmail
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
    public IEnumerable<RoleNameAndUser> GetRoleAndUsers()
    {
      var allRoles = RoleManager.Roles.ToList();
      return allRoles.Select(b => new RoleNameAndUser()
      {
        RoleName = b.Name,
        RoleDisplayName = b.Name,
        Users = UserManager.GetUsersInRoleAsync(b.Name).GetAsyncValue().Count,
        Id = b.Id
      }).ToList();
    }
    public UserCreateView GetUserView(UserCreateView input = null)
    {
      var user = Activator.CreateInstance(BaseUser) as SDHCUser;
      var model = new UserCreateView();

      model.Properties = user.ConvertUserToPost().Properties;
      model.Roles = GetRoleAndUsers();
      if (input != null)
      {
        model.Properties = input.Properties;
      }
      return model;
    }
    public async Task<TUser> GetUserAsync(ClaimsPrincipal principal)
    {
      var user = await UserManager.GetUserAsync(principal);
      if (user == null)
        return default(TUser);
      return user as TUser;
    }
    public async Task SetRolesForUser(TUser user, IEnumerable<string> selectedRoles)
    {
      try
      {
        var allRoles = RoleManager.Roles.ToList();
        var userRemoveRoles = allRoles.Where(b => !selectedRoles.Contains(b.Id)).ToList();
        var userAddRoles = allRoles.Where(b => selectedRoles.Contains(b.Id)).ToList();
        if (userRemoveRoles.Count > 0 || userAddRoles.Count > 0)
        {
          await UserManager.RemoveFromRolesAsync(user, userRemoveRoles.Select(b => b.Name));
          await UserManager.AddToRolesAsync(user, userAddRoles.Select(b => b.Name));
        }
      }
      catch { }
    }

    public async Task<RolesIndexView> GetRolesIndexViewByRoleName(string roleName)
    {
      var rolles = RoleManager.Roles.ToList();
      var allRoles = new List<RoleNameAndUser>();
      foreach (var b in rolles)
      {
        allRoles.Add(new RoleNameAndUser
        {
          Id = b.Id,
          RoleName = b.Name,
          RoleDisplayName = b.Name,
          Users = (await UserManager.GetUsersInRoleAsync(b.Name)).Count
        });
      }
      var role = allRoles.FirstOrDefault(b => String.Equals(b.RoleName, roleName, StringComparison.CurrentCultureIgnoreCase));
      var allUserType = new RoleNameAndUser()
      {
        Id = "",
        RoleDisplayName = "All",
        RoleName = "All",
        Users = UserManager.Users.Count(),
      };
      var otherUserType = new RoleNameAndUser()
      {
        Id = "__",
        RoleDisplayName = "Register",
        RoleName = "Register",
        Users = UserManager.Users.Where(b => b.EmailConfirmed).Count(),
      };
      allRoles.Insert(0, allUserType);
      allRoles.Add(otherUserType);
      IEnumerable<SDHCUser> users = Enumerable.Empty<SDHCUser>();
      switch (roleName)
      {
        case "__":
        case "":
        case "All":
          users = UserManager.Users.ToList();
          break;
        case null:
        case "Register":
          users = UserManager.Users.Where(b => b.EmailConfirmed).ToList();
          break;
        default:
          if (role == null)
            break;
          var roleSelect = RoleManager.Roles.Where(b => b.Id == role.Id).FirstOrDefault();
          if (roleSelect == null)
            break;
          users = await UserManager.GetUsersInRoleAsync(roleName);
          break;
      }
      return new RolesIndexView()
      {
        RoleAndUsers = allRoles,
        Users = users
      };
    }

    public async Task CreateRole(string roleName, MethodResponse response = null)
    {
      if (String.IsNullOrEmpty(roleName))
        return;
      var role = await RoleManager.FindByNameAsync(roleName);
      if (role != null)
        return;
      var r = new IdentityRole();
      r.Name = roleName;
      await RoleManager.CreateAsync(r);
    }
    public async Task DeleteRole(string roleName, MethodResponse response = null)
    {
      var role = await RoleManager.FindByIdAsync(roleName);
      if (role == null)
        return;
      var userInRole = (await UserManager.GetUsersInRoleAsync(role.Name)).Count;
      if (role == null || userInRole > 0)
        return;
      await RoleManager.DeleteAsync(role);
      return;
    }
    public async Task CreateUser(UserCreateView model, MethodResponse response = null)
    {
      if (model.ConfirmPassword != model.Password)
      {
        return;
      }
      TUser user = new TUser();
      var m = user.ConvertUserToPost();
      m.Properties = model.Properties;
      var mu = m.ConvertPostToUser(user);
      if (ConfigContainer.Systems.UserNameIsNotEmail)
      {
        user.UserName = model.UserName;
      }
      else
      {
        user.UserName = model.Email;
      }
      user.Email = model.Email;
      var result = await UserManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
      {
        return;
      }
      await SetRolesForUser(user, model.SelectedRoles);
      MethodResponse.SetIsSuccess(true, response);
    }

    public async Task<UserCreateView> GetUserCreateViewById(string id)
    {
      var user = await UserManager.FindByIdAsync(id);
      if (user == null)
      {
        return null;
      }
      var userPass = user.ConvertUserToPost();
      var model = new UserCreateView();
      var userRoleName = await UserManager.GetRolesAsync(user);
      model.Properties = userPass.Properties;
      model.Id = user.Id;
      model.UserName = user.UserName;
      model.Email = user.Email;
      model.Roles = GetRoleAndUsers();
      model.SelectedRoles = model.Roles != null ? model.Roles.Where(b => userRoleName.Contains(b.RoleName)).Select(b => b.Id) : new List<string>();
      return model;
    }

    public async Task UpdateUserCreateView(UserCreateView model)
    {
      var user = await UserManager.FindByIdAsync(model.Id);
      if (user == null)
      {
        return;
      }
      try
      {
        var passModel = user.ConvertUserToPost();
        passModel.Properties = model.Properties;
        passModel.ConvertPostToUser(user);
        await UserManager.UpdateAsync(user);
        await SetRolesForUser(user, model.SelectedRoles);
      }
      catch { }
    }
  }
}
