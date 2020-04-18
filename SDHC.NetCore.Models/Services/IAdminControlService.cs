using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.NetCore.Models.Services
{
  public interface IAdminControlService
  {
    void Check(Controller that);
  }

  public class AdminControlService : IAdminControlService
  {
    private readonly AdminControl setting;

    public AdminControlService(IOptions<AdminControl> setting)
    {
      this.setting = setting.Value;
    }

    public void Check(Controller that)
    {
      var actions = that?.ControllerContext?.ActionDescriptor;
      if (actions == null)
        return;
      var controllerName = actions?.ControllerName;
      var actionName = actions?.ActionName;
      if (String.IsNullOrWhiteSpace(controllerName) || String.IsNullOrEmpty(actionName))
        return;
      if (!setting.Setting.Any(b => b.Controller == controllerName && b.Actions.Any(a => a == actionName)))
        throw new Exception("500");

    }
  }
}
