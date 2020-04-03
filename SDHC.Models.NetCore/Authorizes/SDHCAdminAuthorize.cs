using Microsoft.AspNetCore.Authorization;
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
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SDHCAdminRequirement requirement)
    {
      context.Succeed(requirement);
      return Task.CompletedTask;
    }

  }
}

