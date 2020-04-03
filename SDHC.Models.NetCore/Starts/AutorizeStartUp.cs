using Microsoft.AspNetCore.Authorization;
using SDHC.Models.NetCore.Authorizes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AutorizeStartUp
  {
    public static void AutorizeStartUpFunction(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddAuthorization(o =>
      {
        o.AddPolicy("AdminPolicy", policy =>
           policy.Requirements.Add(new SDHCAdminRequirement()));
      });
      serviceCollection.AddSingleton<IAuthorizationHandler, SDHCAdminHandler>();
    }
  }
}
