using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDHC.Common.Entity.Models;
using SDHC.Common.Responses;
using SDHC.NetCore.Models.Attributes;
using SDHC.NetCore.Models.Models;
using SDHC.NetCore.Models.Models.ViewModels;
using SDHC.NetCore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class RolesController : Controller
  {
    // GET: Admin/Roles
    private ISDHCUserManager userManager { get; }
    public RolesController(ISDHCUserManager userManager)
    {
      this.userManager = userManager;
    }
    [Admin(adminRole: "RolesIndex")]
    public async Task<IActionResult> Index(string id = "")
    {
      return View(await userManager.GetRolesIndexViewByRoleName(id));
    }
    [Admin(adminRole: "RolesRoleList")]
    public ActionResult RoleList()
    {
      return View(userManager.GetRoleAndUsers());
    }
    [HttpPost]
    [Admin(adminRole: "RolesCreateRole")]
    public async Task<ActionResult> CreateRole(string name)
    {
      await userManager.CreateRole(name);
      return RedirectToAction("RoleList", "Roles", new { @area = "Admin" });
    }
    [HttpPost]
    [Admin(adminRole: "RolesDeleteRole")]
    public async Task<ActionResult> DeleteRole(string id)
    {
      await userManager.DeleteRole(id);
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
      var response = new MethodResponse();
      await userManager.CreateUser(model, response);
      if (!response.IsSuccess)
      {
        return View(userManager.GetUserView(model));
      }
      return RedirectToAction("Index", "Roles", new { @area = "Admin" });
    }
    [Admin(adminRole: "RolesEditUser")]
    public async Task<ActionResult> EditUser(string id)
    {
      var user = await userManager.GetUserCreateViewById(id);
      if (user == null)
      {
        return RedirectToAction("Index");
      }
      return View(user);
    }
    [HttpPost]
    [Admin(adminRole: "RolesEditUser")]
    public async Task<ActionResult> EditUser(UserCreateView model)
    {
      await userManager.UpdateUserCreateView(model);
      return RedirectToAction("Index");
    }
  }
}