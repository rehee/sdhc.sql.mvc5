using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Responses;
using SDHC.NetCore.Models.Models;
using SDHC.NetCore.Models.Models.ViewModels;
using SDHC.NetCore.Models.Services.UserAndRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.NetCore.Models.Services
{
  public interface ISDHCUserManager
  {
    ISDHCMemberService UserManager { get; }

    Type BaseUser { get; }
    ISDHCSignInService SignInManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    ContentTableHtmlView GetContentTableHtmlView(IEnumerable<SDHCUser> users);
    SDHCUser CreateUser(string email, string userName);
    Task<SDHCUser> GetUserAsync(ClaimsPrincipal principal);
    IEnumerable<RoleNameAndUser> GetRoleAndUsers();
    UserCreateView GetUserView(UserCreateView input = null);
    Task SetRolesForUser(SDHCUser user, IEnumerable<string> selectedRoles);
    Task<RolesIndexView> GetRolesIndexViewByRoleName(string roleName);
    Task CreateRole(string roleName, MethodResponse response = null);
    Task DeleteRole(string roleName, MethodResponse response = null);
    Task CreateUser(UserCreateView model, MethodResponse response = null);
    Task<UserCreateView> GetUserCreateViewById(string id);
    Task UpdateUserCreateView(UserCreateView model);
  }
}
