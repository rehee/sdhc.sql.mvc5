using Microsoft.AspNetCore.Builder;
using SDHC.NetCore.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class Set_Up_Function
  {
    public static void UseChat(this IServiceCollection services)
    {
      services.ConfigureOptions(typeof(EditorRCLConfigureOptions));

    }
    public static void UseChat(this IApplicationBuilder app)
    {
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<ChatHub>("/chatHub");
      });

    }
  }
}
