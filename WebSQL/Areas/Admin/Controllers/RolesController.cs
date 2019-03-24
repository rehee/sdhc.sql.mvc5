using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSQL.Models;

namespace WebSQL.Areas.Admin.Controllers
{
  public class RolesController : Controller
  {
    // GET: Admin/Roles
    private ApplicationRoleManager roleManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); } }
    private ApplicationUserManager userManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
    private IEnumerable<RoleNameAndUser> getRoleAndUsers()
    {
      return roleManager.Roles.Select(b => new RoleNameAndUser() { RoleName = b.Name, RoleDisplayName = b.Name, Users = b.Users.Count, Id = b.Id }).ToList(); ;
    }
    public ActionResult Index(string id = "")
    {
      //var role = roleManager.Roles
      //  .Where(b => String.Equals(b.Name, id, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
      var tabNameAndUser = new List<RoleNameAndUser>();
      var allUsers = userManager.Users.ToList();
      var otherUsers = userManager.Users.Where(b => b.Roles.Count <= 0).ToList();
      tabNameAndUser.Add(new RoleNameAndUser() { RoleName = "", RoleDisplayName = "All", Users = allUsers.Count });

      var roles = getRoleAndUsers();
      tabNameAndUser.AddRange(roles);
      tabNameAndUser.Add(new RoleNameAndUser() { RoleName = "_other", RoleDisplayName = "Registered", Users = otherUsers.Count });
      IEnumerable<ApplicationUser> users = Enumerable.Empty<ApplicationUser>();

      if (string.IsNullOrEmpty(id))
      {
        users = allUsers;
      }
      if (string.Equals("_other", id, StringComparison.CurrentCultureIgnoreCase))
      {
        users = otherUsers;
      }
      var role = roleManager.Roles.Where(b => b.Name == id).FirstOrDefault();
      if (role != null)
      {
        users = userManager.Users.Where(b => b.Roles.Where(c => c.RoleId == role.Id).FirstOrDefault() != null).ToList();
      }
      return View(new RolesIndexView()
      {
        RoleAndUsers = tabNameAndUser,
        Users = users
      });
    }
    public ActionResult RoleList()
    {
      return View(getRoleAndUsers());
    }
    [HttpPost]
    public async Task<ActionResult> CreateRole(string name)
    {
      if (String.IsNullOrEmpty(name))
      {
        goto returns;
      }
      var role = await roleManager.FindByNameAsync(name);
      if (role != null)
      {
        goto returns;
      }
      var r = new IdentityRole();
      r.Name = name;
      await roleManager.CreateAsync(r);
      returns:
      return RedirectToAction("RoleList", "Roles", new { @area = "Admin" });
    }
    [HttpPost]
    public async Task<ActionResult> DeleteRole(string id)
    {
      var role = await roleManager.FindByIdAsync(id);
      if (role == null || role.Users.Count > 0)
      {
        goto returns;
      }
      await roleManager.DeleteAsync(role);
      returns:
      return RedirectToAction("RoleList", "Roles", new { @area = "Admin" });
    }

    public ActionResult CreateUser()
    {
      var user = new ApplicationUser();
      var model = new UserCreateView();

      model.Properties = user.ConvertUserToPost().Properties;
      model.Roles = getRoleAndUsers();
      return View(model);
    }
    [HttpPost]
    public async Task<ActionResult> CreateUser(UserCreateView model)
    {
      if (model.ConfirmPassword != model.Password)
      {
        return View(model);
      }
      var user = new ApplicationUser();
      var m = user.ConvertUserToPost();
      m.Properties = model.Properties;
      var mu = m.ConvertPostToUser(user);
      user.UserName = model.UserName;
      user.Email = model.Email;
      await userManager.CreateAsync(user, model.Password);
      foreach (var item in model.SelectedRoles)
      {
        var role = await roleManager.FindByIdAsync(item);
        await userManager.AddToRoleAsync(user.Id, role.Name);
      }
      return RedirectToAction("CreateUser");
    }

    public async Task<ActionResult> EditUser(string id)
    {
      var user = await userManager.FindByIdAsync(id);
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
      model.Roles = getRoleAndUsers();
      model.SelectedRoles = user.Roles.Select(b => b.RoleId).ToList();
      return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> EditUser(UserCreateView model)
    {
      var user = await userManager.FindByIdAsync(model.Id);
      if (user == null)
      {
        return RedirectToAction("Index");
      }
      var passModel = user.ConvertUserToPost();
      passModel.Properties = model.Properties;

      passModel.ConvertPostToUser(user);
      await userManager.UpdateAsync(user);

      var userRoles = user.Roles.Select(b => b.RoleId).ToList();
      var userRemoveRoles = userRoles.Where(b => !model.SelectedRoles.Contains(b)).ToList();
      var userAddRoles = model.SelectedRoles.Where(b => !userRoles.Contains(b)).ToList();
      if (userRemoveRoles.Count > 0 || userAddRoles.Count > 0)
      {
        var allRoles = roleManager.Roles.ToList();
        var removeRoleName = allRoles.Where(b => userRemoveRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
        var addRoleName = allRoles.Where(b => userAddRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
        await userManager.RemoveFromRolesAsync(user.Id, removeRoleName);
        await userManager.AddToRolesAsync(user.Id, addRoleName);
      }
      return RedirectToAction("Index");
    }
  }

  public class UserCreateView : IPassModel
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public List<ContentProperty> Properties { get; set; } = new List<ContentProperty>();
    public IEnumerable<string> SelectedRoles { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<RoleNameAndUser> Roles { get; set; }
  }
  public class RolesIndexView
  {
    public IEnumerable<RoleNameAndUser> RoleAndUsers { get; set; }
    public IEnumerable<ApplicationUser> Users { get; set; }

  }
  public class RoleNameAndUser
  {
    public string Id { get; set; }
    public string RoleName { get; set; }
    public string RoleDisplayName { get; set; }
    public int Users { get; set; }
  }

}