using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDHC.Common.Entity.Models;
using SDHC.Models.NetCore.Attributes;
using SDHC.Models.NetCore.Models;
using SDHC.Models.NetCore.Models.ViewModels;
using SDHC.Models.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class RoleAndUserController : Controller
  {
    // GET: Admin/Roles
    private ISDHCUserManager<SDHCUser> userManager { get; }
    public RoleAndUserController(ISDHCUserManager<SDHCUser> userManager)
    {
      this.userManager = userManager;
    }
    [Admin(adminRole: "RolesIndex")]
    public ActionResult Index(string id = "")
    {
      var allRoles = userManager.RoleManager.Roles
        .Where(b => true)
        .Select(b => new RoleNameAndUser { Id = b.Id, RoleName = b.Name, RoleDisplayName = b.Name, Users = b.Users.Count })
        .ToList();
      var role = allRoles
        .Where(b => String.Equals(b.RoleName, id, StringComparison.CurrentCultureIgnoreCase))
        .FirstOrDefault();

      var allUserType = new RoleNameAndUser()
      {
        Id = "",
        RoleDisplayName = "All",
        RoleName = "All",
        Users = userManager.UserManager.Users.Count(),
      };
      var otherUserType = new RoleNameAndUser()
      {
        Id = "__",
        RoleDisplayName = "Register",
        RoleName = "Register",
        Users = userManager.UserManager.Users.Where(b => b.Roles.Count <= 0).Count(),
      };
      allRoles.Insert(0, allUserType);
      allRoles.Add(otherUserType);
      IEnumerable<SDHCUser> users = Enumerable.Empty<SDHCUser>();
      switch (id)
      {
        case "__":
          users = userManager.UserManager.Users.Where(b => b.Roles.Count <= 0).ToList();
          break;
        case null:
        case "":
          users = userManager.UserManager.Users.ToList();
          break;
        default:
          if (role == null)
            break;
          var roleSelect = userManager.RoleManager.Roles.Where(b => b.Id == role.Id).FirstOrDefault();
          if (roleSelect == null)
            break;
          var userIds = roleSelect.Users.ToList().Select(b => b.UserId).ToList();
          users = userManager.UserManager.Users.Where(b => userIds.Contains(b.Id)).ToList();
          break;
      }
      users = userManager.UserManager.Users.ToList();
      return View(new RolesIndexView()
      {
        RoleAndUsers = allRoles,
        Users = users,
      });
    }
    [Admin(adminRole: "RolesRoleList")]
    public ActionResult RoleList()
    {
      return View(userManager.getRoleAndUsers());
    }
    [HttpPost]
    [Admin(adminRole: "RolesCreateRole")]
    public async Task<ActionResult> CreateRole(string name)
    {
      if (String.IsNullOrEmpty(name))
      {
        goto returns;
      }
      var role = await userManager.RoleManager.FindByNameAsync(name);
      if (role != null)
      {
        goto returns;
      }
      var r = new IdentityRoleUser();
      r.Name = name;
      await userManager.RoleManager.CreateAsync(r);
      returns:
      return RedirectToAction("RoleList", "Roles", new { @area = "Admin" });
    }
    [HttpPost]
    [Admin(adminRole: "RolesDeleteRole")]
    public async Task<ActionResult> DeleteRole(string id)
    {
      var role = await userManager.RoleManager.FindByIdAsync(id);
      if (role == null || role.Users.Count > 0)
      {
        goto returns;
      }
      await userManager.RoleManager.DeleteAsync(role);
      returns:
      return RedirectToAction("RoleList", "Roles", new { @area = "Admin" });
    }

    [Admin(adminRole: "RolesCreateUser")]
    public ActionResult CreateUser()
    {
      return View(userManager.GetUserView());
    }
    [HttpPost]
    [Admin(adminRole: "RolesCreateUser")]
    public async Task<ActionResult> CreateUser(UserCreateView model)
    {
      if (model.ConfirmPassword != model.Password)
      {
        return View(userManager.GetUserView(model));
      }
      SDHCUser user = Activator.CreateInstance(userManager.BaseUser) as SDHCUser;
      var m = user.ConvertUserToPost();
      m.Properties = model.Properties;
      var mu = m.ConvertPostToUser(user);
      if (ConfigContainer.Systems.UserNameIsNotEmail)
      {
        user.UserName = model.UserName;
      }
      else
      {
        user.UserName = model.Email;
      }
      user.Email = model.Email;
      var result = await userManager.UserManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
      {
        return View(userManager.GetUserView(model));
      }
      await setRolesForUser(user, model.SelectedRoles);
      return RedirectToAction("Index", "Roles", new { @area = "Admin" });
    }
    [Admin(adminRole: "RolesEditUser")]
    public async Task<ActionResult> EditUser(string id)
    {
      var user = await userManager.UserManager.FindByIdAsync(id);
      if (user == null)
      {
        return RedirectToAction("Index");
      }
      var userPass = user.ConvertUserToPost();
      var model = new UserCreateView();
      model.Properties = userPass.Properties;
      model.Id = user.Id;
      model.UserName = user.UserName;
      model.Email = user.Email;
      model.Roles = userManager.getRoleAndUsers();
      model.SelectedRoles = user.Roles.Select(b => b.RoleId).ToList();
      return View(model);
    }
    [HttpPost]
    [Admin(adminRole: "RolesEditUser")]
    public async Task<ActionResult> EditUser(UserCreateView model)
    {
      var user = await userManager.UserManager.FindByIdAsync(model.Id);
      if (user == null)
      {
        return RedirectToAction("Index");
      }
      try
      {
        var passModel = user.ConvertUserToPost();
        passModel.Properties = model.Properties;

        passModel.ConvertPostToUser(user);
        await userManager.UserManager.UpdateAsync(user);
        await setRolesForUser(user, model.SelectedRoles);
      }
      catch { }

      return RedirectToAction("Index");
    }

    private async Task setRolesForUser(SDHCUser user, IEnumerable<string> selectedRoles)
    {
      try
      {
        var userRoles = user.Roles.Select(b => b.RoleId).ToList();
        var userRemoveRoles = userRoles.Where(b => !selectedRoles.Contains(b)).ToList();
        var userAddRoles = selectedRoles.Where(b => !userRoles.Contains(b)).ToList();
        if (userRemoveRoles.Count > 0 || userAddRoles.Count > 0)
        {
          var allRoles = userManager.RoleManager.Roles.ToList();
          var removeRoleName = allRoles.Where(b => userRemoveRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
          var addRoleName = allRoles.Where(b => userAddRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
          await userManager.UserManager.RemoveFromRolesAsync(user, removeRoleName);
          await userManager.UserManager.AddToRolesAsync(user, addRoleName);
        }

      }
      catch { }
    }
  }
}