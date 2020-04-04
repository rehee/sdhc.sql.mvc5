using Microsoft.AspNetCore.Identity;
using SDHC.Models.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Services
{
  public class SDHCUserRoleStore : IUserRoleStore<SDHCUser>
  {
    public Task AddToRoleAsync(SDHCUser user, string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public Task<SDHCUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<SDHCUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IList<string>> GetRolesAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserNameAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IList<SDHCUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(SDHCUser user, string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task RemoveFromRoleAsync(SDHCUser user, string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(SDHCUser user, string normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetUserNameAsync(SDHCUser user, string userName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(SDHCUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
