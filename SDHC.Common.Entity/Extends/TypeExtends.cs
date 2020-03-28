using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Routing;

namespace System
{
  public static class TypeExtends
  {
    public static IEnumerable<Type> GetAllowChildrens(object input)
    {
      Type type;
      if (input == null)
      {
        type = ServiceContainer.ContentService.BaseIContentModelType;
      }
      else
      {
        type = input.GetType().GetRealType();
      }
      var allows = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
      if (allows == null || allows.ChildrenType == null)
      {
        return Enumerable.Empty<Type>();
      }
      return allows.ChildrenType;
    }
    public static int GetTableSize(object input)
    {
      Type type;
      if (input == null)
      {
        type = ServiceContainer.ContentService.BaseIContentModelType;
      }
      else
      {
        type = input.GetType().GetRealType();
      }
      return GetTableSize(type);
    }
    public static int GetTableSize(Type type)
    {
      var allows = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
      if (allows == null || allows.TableSize == EnumTablePageSize.L0)
      {
        return G.DefaultTablePageSize;
      }
      return (int)allows.TableSize;
    }
    public static string GetClassDisplayName(Type input)
    {
      var display = input.GetObjectCustomAttribute<AllowChildrenAttribute>();
      if (display != null)
      {
        if (!String.IsNullOrEmpty(display.Name))
        {
          return display.Name;
        }
      }
      return input.Name.SpacesFromCamel();
    }

    public static bool GetAdminAuthorize(string userId, EnumAdminAuthorize crud, Type input)
    {
      if (String.IsNullOrEmpty(userId))
        return false;
      IEnumerable<string> basicRole = G.AdminRole.Split(',');
      var typeRole = GetAdminAuthorizeRoles(crud, input);
      if (typeRole.Count() > 0)
      {
        basicRole = typeRole;
      }
      return UserIsInRoles(userId, basicRole);
    }
    public static bool UserIsInRoles(this string id, string role)
    {
      if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(role))
      {
        return false;
      }
      return role.Trim().Split(',').Any(b => G.UserManager().IsInRoleAsync(id, b.Trim()).GetAsyncValue<bool>());
    }
    public static bool UserIsInRoles(this string id, IEnumerable<string> roles)
    {
      if (String.IsNullOrEmpty(id) || roles == null)
      {
        return false;
      }
      return roles.Any(b => G.UserManager().IsInRoleAsync(id, b.Trim()).GetAsyncValue<bool>());
    }
    public static IEnumerable<string> GetAdminAuthorizeRoles(EnumAdminAuthorize crud, Type input)
    {
      var adminList = G.AdminRole.Split(',').Select(b => b.Trim()).Where(b => !String.IsNullOrEmpty(b)).ToList();
      var children = input.GetAllowChildren();
      if (children == null)
        return adminList;
      switch (crud)
      {
        case EnumAdminAuthorize.Create:
          if (children.CreateRoles != null)
            return children.CreateRoles;
          break;
        case EnumAdminAuthorize.Read:
          if (children.ReadRoles != null)
            return children.ReadRoles;
          break;
        case EnumAdminAuthorize.Update:
          if (children.EditRoles != null)
            return children.EditRoles;
          break;
        case EnumAdminAuthorize.Delete:
          if (children.DeleteRoles != null)
            return children.DeleteRoles;
          break;
      }
      return adminList;
    }
    public static IEnumerable<string> GetTypeFromController(
      string controller, string action, string id, RouteValueDictionary vales, NameValueCollection form)
    {
      var adminList = G.AdminRole.Split(',').Select(b => b.Trim()).Where(b => !String.IsNullOrEmpty(b)).ToList();
      if (String.IsNullOrEmpty(controller))
        return adminList;
      Func<string, string> GetKey = key =>
      {
        if (vales.ContainsKey(key))
        {
          return vales[key].ToString();
        }
        return "";
      };
      Func<string, string> GetFormKey = key =>
      {
        if (form == null)
          return "";
        if (form.AllKeys.Contains(key))
        {
          return form[key].ToString();
        }
        return "";
      };
      controller = $"{controller}Controller";
      if (String.IsNullOrEmpty(action))
        action = "Index";
      #region ContentController
      //if (String.Equals(controller, nameof(ContentController), StringComparison.CurrentCultureIgnoreCase))
      //{
      //  if (String.Equals(action, nameof(ContentController.Index), StringComparison.CurrentCultureIgnoreCase))
      //  {
      //    if (String.IsNullOrEmpty(id))
      //    {
      //      return GetAdminAuthorizeRoles(EnumAdminAuthorize.Read, ServiceContainer.ContentService.BaseIContentModelType);
      //    }
      //    var model = ServiceContainer.ModelService.Find<BaseContent>(id.MyTryConvert<long>());
      //    if (model != null)
      //    {
      //      return GetAdminAuthorizeRoles(EnumAdminAuthorize.Read, model.GetType());
      //    }
      //    return adminList;
      //  }
      //  if (String.Equals(action, nameof(ContentController.PreCreate), StringComparison.CurrentCultureIgnoreCase))
      //  {
      //    var fullType = GetKey("FullType");
      //    var type = Type.GetType(fullType);
      //    if (type != null)
      //      return GetAdminAuthorizeRoles(EnumAdminAuthorize.Create, type);
      //    return adminList;
      //  }
      //  if (String.Equals(action, nameof(ContentController.Create), StringComparison.CurrentCultureIgnoreCase))
      //  {

      //    var fullType = $"{GetFormKey("FullType")},{GetFormKey("ThisAssembly")}";
      //    var type = Type.GetType(fullType);
      //    if (type != null)
      //      return GetAdminAuthorizeRoles(EnumAdminAuthorize.Create, type);
      //    return adminList;
      //  }
      //  if (String.Equals(action, nameof(ContentController.Edit), StringComparison.CurrentCultureIgnoreCase))
      //  {
      //    if (!String.IsNullOrEmpty(id))
      //    {
      //      var model = ServiceContainer.ModelService.Find<BaseContent>(id.MyTryConvert<long>());
      //      if (model != null)
      //      {
      //        return GetAdminAuthorizeRoles(EnumAdminAuthorize.Read, model.GetType());
      //      }
      //    }
      //    else
      //    {
      //      var fullType = $"{GetFormKey("FullType")},{GetFormKey("ThisAssembly")}";
      //      var type = Type.GetType(fullType);
      //      if (type != null)
      //        return GetAdminAuthorizeRoles(EnumAdminAuthorize.Create, type);
      //    }
      //    return adminList;
      //  }
      //  if (String.Equals(action, nameof(ContentController.Delete), StringComparison.CurrentCultureIgnoreCase))
      //  {
      //    if (!String.IsNullOrEmpty(id))
      //    {
      //      var model = ServiceContainer.ModelService.Find<BaseContent>(id.MyTryConvert<long>());
      //      if (model != null)
      //      {
      //        return GetAdminAuthorizeRoles(EnumAdminAuthorize.Delete, model.GetType());
      //      }
      //    }

      //  }


      //}

      #endregion

      return adminList;
    }


  }

  public static partial class G
  {
    public static AllowChildrenAttribute GetAllowChildren(this Type type)
    {
      return type.GetObjectCustomAttribute<AllowChildrenAttribute>();
    }

    public static AllowChildrenAttribute GetAllowChildren(this object input)
    {
      return input.GetType().GetRealType().GetObjectCustomAttribute<AllowChildrenAttribute>();
    }
    public static bool IsSingleRecordTable(this Type type)
    {
      var a = type.GetAllowChildren();
      if (a == null)
        return false;
      return a.SingleRecord;
    }
    public static bool IsSingleRecordTable(this object input)
    {
      return input.GetType().GetRealType().IsSingleRecordTable();
    }
    public static IEnumerable<string> GetTableList(AllowChildrenAttribute model, IEnumerable<string> defaultList)
    {
      if (model == null || model.TableList == null)
      {
        return defaultList;
      }
      return model.TableList;
    }
    public static IEnumerable<string> GetTableList(Type type, IEnumerable<string> defaultList)
    {
      return GetTableList(type.GetAllowChildren(), defaultList);
    }
  }

  public enum EnumAdminAuthorize
  {
    Create,
    Read,
    Update,
    Delete
  }
}
