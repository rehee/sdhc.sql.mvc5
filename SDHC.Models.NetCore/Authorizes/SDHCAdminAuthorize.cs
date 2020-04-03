using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Authorizes
{
  public class SDHCAdminRequirement : IAuthorizationRequirement
  {
  }

  public class SDHCAdminHandler : AuthorizationHandler<SDHCAdminRequirement>
  {
    private SystemConfig config { get; set; }
    public SDHCAdminHandler(IOptions<SystemConfig> config)
    {
      this.config = config.Value;
    }
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context, SDHCAdminRequirement requirement)
    {
      if (this.config.AdminFree)
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }

  }
}

