//using SDHC.Common.Entity.Attributes;
//using SDHC.Common.Entity.Models;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Web.Routing;

//namespace System2
//{
//  public static class TypeExtends
//  {
//    public static IEnumerable<Type> GetAllowChildrens(object input)
//    {
//      Type type;
//      if (input == null)
//      {
//        type = ServiceContainer.ContentService.BaseIContentModelType;
//      }
//      else
//      {
//        type = input.GetType().GetRealType();
//      }
//      var allows = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
//      if (allows == null || allows.ChildrenType == null)
//      {
//        return Enumerable.Empty<Type>();
//      }
//      return allows.ChildrenType;
//    }
//    public static int GetTableSize(object input)
//    {
//      Type type;
//      if (input == null)
//      {
//        type = ServiceContainer.ContentService.BaseIContentModelType;
//      }
//      else
//      {
//        type = input.GetType().GetRealType();
//      }
//      return GetTableSize(type);
//    }
//    public static int GetTableSize(Type type)
//    {
//      var allows = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
//      if (allows == null || allows.TableSize == EnumTablePageSize.L0)
//      {
//        return G.DefaultTablePageSize;
//      }
//      return (int)allows.TableSize;
//    }
//    public static string GetClassDisplayName(Type input)
//    {
//      var display = input.GetObjectCustomAttribute<AllowChildrenAttribute>();
//      if (display != null)
//      {
//        if (!String.IsNullOrEmpty(display.Name))
//        {
//          return display.Name;
//        }
//      }
//      return input.Name.SpacesFromCamel();
//    }

//    public static bool GetAdminAuthorize(string userId, EnumAdminAuthorize crud, Type input)
//    {
//      if (String.IsNullOrEmpty(userId))
//        return false;
//      IEnumerable<string> basicRole = G.AdminRole.Split(',');
//      var typeRole = GetAdminAuthorizeRoles(crud, input);
//      if (typeRole.Count() > 0)
//      {
//        basicRole = typeRole;
//      }
//      return UserIsInRoles(userId, basicRole);
//    }
//    public static bool UserIsInRoles(this string id, string role)
//    {
//      if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(role))
//      {
//        return false;
//      }
//      return role.Trim().Split(',').Any(b => G.UserManager().IsInRoleAsync(id, b.Trim()).GetAsyncValue<bool>());
//    }
//    public static bool UserIsInRoles(this string id, IEnumerable<string> roles)
//    {
//      if (String.IsNullOrEmpty(id) || roles == null)
//      {
//        return false;
//      }
//      return roles.Any(b => G.UserManager().IsInRoleAsync(id, b.Trim()).GetAsyncValue<bool>());
//    }
//    public static IEnumerable<string> GetAdminAuthorizeRoles(EnumAdminAuthorize crud, Type input)
//    {
//      var adminList = G.AdminRole.Split(',').Select(b => b.Trim()).Where(b => !String.IsNullOrEmpty(b)).ToList();
//      var children = input.GetAllowChildren();
//      if (children == null)
//        return adminList;
//      switch (crud)
//      {
//        case EnumAdminAuthorize.Create:
//          if (children.CreateRoles != null)
//            return children.CreateRoles;
//          break;
//        case EnumAdminAuthorize.Read:
//          if (children.ReadRoles != null)
//            return children.ReadRoles;
//          break;
//        case EnumAdminAuthorize.Update:
//          if (children.EditRoles != null)
//            return children.EditRoles;
//          break;
//        case EnumAdminAuthorize.Delete:
//          if (children.DeleteRoles != null)
//            return children.DeleteRoles;
//          break;
//      }
//      return adminList;
//    }
    
    
//  }

  

 
//}
