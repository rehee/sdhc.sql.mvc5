using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDHC.Common.EntityCore.Models;
using SDHC.NetCore.Models.Models;
using SDHC.NetCore.Models.Services;
using SDHC.NetCore.Models.Services.UserAndRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
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
      services.AutorizeStartUpFunction();
    }
  }
}
