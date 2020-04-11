using Microsoft.AspNetCore.Identity;
using SDHC.NetCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.NetCore.Models.Models
{
  public static class ConvertExtends
  {
    public static UserPassModel ConvertUserToPost(this IdentityUser input)
    {
      var model = new UserPassModel();

      var inputType = input.GetType().GetRealType();
      var inputProperty = inputType.GetProperties();
      var userType = typeof(IdentityUser);
      var userProperty = userType.GetProperties();
      foreach (var p in inputProperty)
      {
        if (userProperty.Where(b => b.Name == p.Name).FirstOrDefault() != null)
        {
          try
          {
            p.SetValue(model, p.GetValue(input));
          }
          catch { }
          continue;
        }
        var prop = p.GetContentPropertyByPropertyInfo(input);
        if (prop == null)
          continue;
        model.Properties.Add(prop);
      }
      model.Properties = model.Properties.OrderBy(b => b.SortOrder).ToList();
      return model;
    }
    public static IdentityUser ConvertPostToUser(this UserPassModel input, IdentityUser user, bool deleteExistFile = true, List<string> oldFiles = null, List<string> newFiles = null)
    {
      var inputType = user.GetType().GetRealType();
      var inputProperty = inputType.GetProperties();

      var userType = typeof(IdentityUser);
      var userProperty = userType.GetProperties();

      foreach (var p in inputProperty)
      {
        try
        {
          if (userProperty.Where(b => b.Name == p.Name).FirstOrDefault() != null)
          {
            continue;
          }
          p.SetPropertyValue(input, user, deleteExistFile, oldFiles, newFiles);
        }
        catch { }
      }

      return user;
    }
  }
}
