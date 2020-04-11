using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDHC.Common.EntityCore.Models;
using SDHC.Models.NetCore.Models;
using SDHC.Models.NetCore.Services;
using SDHC.Models.NetCore.Services.UserAndRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Services
{
  public static class UserAndAuthStartUp
  {
    public static void UserAndAuthInit<TRepo, TUser, TBaseContent>(this IServiceCollection services)
      where TRepo : DbContext, IContent
      where TUser : SDHCUser, new()
      where TBaseContent : BaseContent
    {
      services.AddIdentity<TUser, IdentityRole>(
        options =>
        {
          options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<TRepo>()
        .AddDefaultTokenProviders();
      services.AddScoped<RoleManager<IdentityRole>>();
      services.AddScoped<ISDHCMemberService, SDHCMemberService<TUser>>();
      services.AddScoped<ISDHCSignInService, SDHCSignInService<TUser>>();
      services.AddScoped<ISDHCUserManager, SDHCUserManager<TUser>>();
      //services.AddScoped<IUserTwoFactorTokenProvider<TUser>, SDHCUserTwoFactorTokenProvider<TUser>>();
      services.AutorizeStartUpFunction();
    }
  }
}
