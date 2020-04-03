using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.Models.NetCore.Attributes
{
  
  public class AdminAttribute : AuthorizeAttribute
  {
    public AdminAttribute(string adminRole)
    {
      var adminRoleForSetting = ConfigContainer.GetSetting(adminRole);
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
      this.Policy = base.Policy;
    }
  }
}
