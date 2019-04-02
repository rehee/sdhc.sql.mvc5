using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SDHC.Common.Entity.Models;
using Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Admin.Areas.Admin.Controllers
{
  public class RolesController : Controller
  {
    // GET: Admin/Roles
    private ApplicationRoleManager roleManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); } }
    private ApplicationUserManager userManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
    private IEnumerable<RoleNameAndUser> getRoleAndUsers()
    {
      return roleManager.Roles.Select(b => new RoleNameAndUser() { RoleName = b.Name, RoleDisplayName = b.Name, Users = b.Users.Count, Id = b.Id }).ToList();
    }
    public ActionResult Index(string id = "")
    {
      //var role = roleManager.Roles
      //  .Where(b => String.Equals(b.Name, id, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
      //var tabNameAndUser = new List<RoleNameAndUser>();
      //var allUsers = userManager.Users.ToList();
      //var otherUsers = userManager.Users.Where(b => b.Roles.Count <= 0).ToList();
      //tabNameAndUser.Add(new RoleNameAndUser() { RoleName = "", RoleDisplayName = "All", Users = allUsers.Count });

      //var roles = getRoleAndUsers();
      //tabNameAndUser.AddRange(roles);
      //tabNameAndUser.Add(new RoleNameAndUser() { RoleName = "_other", RoleDisplayName = "Registered", Users = otherUsers.Count });
      //IEnumerable<SDHCUser> users = Enumerable.Empty<SDHCUser>();

      //if (string.IsNullOrEmpty(id))
      //{
      //  users = allUsers;
      //}
      //if (string.Equals("_other", id, StringComparison.CurrentCultureIgnoreCase))
      //{
      //  users = otherUsers;
      //}
      //var role = roleManager.Roles.Where(b => b.Name == id).FirstOrDefault();
      //if (role != null)
      //{
      //  users = userManager.Users.Where(b => b.Roles.Where(c => c.RoleId == role.Id).FirstOrDefault() != null).ToList();
      //}
      //return View(new RolesIndexView()
      //{
      //  RoleAndUsers = tabNameAndUser,
      //  Users = users
      //});
      return View(new RolesIndexView()
      {
        RoleAndUsers = Enumerable.Empty<RoleNameAndUser>(),
        Users = userManager.Users.Where(b => b.Roles.Count()==0).ToList()
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
    return RedirectToAction("RoleList", "Roles", new { @area = G.AdminPath });
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
    return RedirectToAction("RoleList", "Roles", new { @area = G.AdminPath });
  }

  private UserCreateView GetUserView(UserCreateView input = null)
  {
    var user = Activator.CreateInstance(SDHCUserManager.BaseUser) as SDHCUser;
    var model = new UserCreateView();

    model.Properties = user.ConvertUserToPost().Properties;
    model.Roles = getRoleAndUsers();
    if (input != null)
    {
      model.Properties = input.Properties;
    }
    return model;
  }
  public ActionResult CreateUser()
  {
    return View(GetUserView());
  }
  [HttpPost]
  public async Task<ActionResult> CreateUser(UserCreateView model)
  {
    if (model.ConfirmPassword != model.Password)
    {
      return View(GetUserView(model));
    }
    SDHCUser user = Activator.CreateInstance(SDHCUserManager.BaseUser) as SDHCUser;
    var m = user.ConvertUserToPost();
    m.Properties = model.Properties;
    var mu = m.ConvertPostToUser(user);
    if (G.UserNameIsNotEmail)
    {
      user.UserName = model.UserName;
    }
    else
    {
      user.UserName = model.Email;
    }
    user.Email = model.Email;
    var result = await userManager.CreateAsync(user, model.Password);
    if (!result.Succeeded)
    {
      return View(GetUserView(model));
    }
    await setRolesForUser(user, model.SelectedRoles);
    return RedirectToAction("Index", "Roles", new { @area = G.AdminPath });
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
    try
    {
      var passModel = user.ConvertUserToPost();
      passModel.Properties = model.Properties;

      passModel.ConvertPostToUser(user);
      await userManager.UpdateAsync(user);
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
        var allRoles = G.RoleManager().Roles.ToList();
        var removeRoleName = allRoles.Where(b => userRemoveRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
        var addRoleName = allRoles.Where(b => userAddRoles.Contains(b.Id)).Select(b => b.Name).ToArray();
        await G.UserManager().RemoveFromRolesAsync(user.Id, removeRoleName);
        await G.UserManager().AddToRolesAsync(user.Id, addRoleName);
      }
        
    }
    catch { }
  }
}
}

namespace System
{
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
    public IEnumerable<SDHCUser> Users { get; set; }

  }
  public class RoleNameAndUser
  {
    public string Id { get; set; }
    public string RoleName { get; set; }
    public string RoleDisplayName { get; set; }
    public int Users { get; set; }
  }

}