using Microsoft.AspNetCore.Identity;
using SDHC.Models.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Services.UserAndRoles
{
  public class SDHCUserTwoFactorTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser> where TUser : SDHCUser
  {
    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
      throw new NotImplementedException();
    }

    public Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
      throw new NotImplementedException();
    }

    public Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
      throw new NotImplementedException();
    }
  }
}
