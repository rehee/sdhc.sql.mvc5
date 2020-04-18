using Microsoft.AspNetCore.Authorization;
using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.NetCore.Models.Attributes
{

  public class AdminAttribute : AuthorizeAttribute
  {
    public AdminAttribute(string adminRole)
    {
      if (!ConfigContainer.Systems.AdminFree)
      {
        var adminRoleForSetting = ConfigContainer.GetSetting($"Roles:{adminRole}");
        var DefaultadminRole = ConfigContainer.Systems.AdminRole;
        var supperUser = ConfigContainer.Systems.SuperUserRole;
        var roleLists = new List<string>();
        if (!String.IsNullOrEmpty(adminRoleForSetting))
        {
          roleLists.AddRange(adminRoleForSetting.Split(',')
            .Select(b => b.Trim())
            .Where(b => !String.IsNullOrEmpty(b)));
        }
        if (!String.IsNullOrEmpty(DefaultadminRole))
        {
          roleLists.AddRange(DefaultadminRole.Split(',')
            .Select(b => b.Trim())
            .Where(b => !String.IsNullOrEmpty(b)));
        }
        if (!String.IsNullOrEmpty(supperUser))
        {
          roleLists.AddRange(supperUser.Split(',')
            .Select(b => b.Trim())
            .Where(b => !String.IsNullOrEmpty(b)));
        }
        var uniqRoles = roleLists.GroupBy(b => b).Select(b => b.Key);
        this.Roles = String.Join(",", uniqRoles);
      }
      if (!String.IsNullOrEmpty(ConfigContainer.Systems.AdminPolicy))
      {
        this.Policy = ConfigContainer.Systems.AdminPolicy;
      }
    }
  }
}
