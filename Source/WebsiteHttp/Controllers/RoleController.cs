using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebsiteHttp.Models;

namespace WebsiteHttp.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.Select(rl => new RoleViewModel() { Name = rl.Id }).ToList();

            return View(roles);
        }

        public IActionResult CreateRoles()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateRoles", model);
            }

            var role = new Role
            {
                Id = model.Name,
            };

            await roleManager.CreateAsync(role);

            return RedirectToAction("Index");
        }

        public IActionResult AssignUsers()
        {
            var roles = roleManager.Roles.ToList();

            var users = userManager.Users.ToList();

            ViewBag.Roles = roles;
            ViewBag.Users = users;

            return View();
        }

        public async Task<IActionResult> AssignUser(UserToRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AssignUsers", model);
            }

            var role = await roleManager.FindByIdAsync(model.RoleName);

            if (role == null)
            {
                ModelState.AddModelError("", $"Role name {model.RoleName} does not exists");

                return View("AssignUsers", model);
            }

            var user = await userManager.FindByIdAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", $"User name {model.RoleName} does not exists");

                return View("AssignUsers", model);
            }

            role.Users.Add(user);

            await roleManager.UpdateAsync(role);

            return RedirectToAction("Index");
        }
    }
}