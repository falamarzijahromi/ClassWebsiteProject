using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebsiteHttp.Models;

namespace WebsiteHttp.Controllers
{
    [Authorize(Policy = "OnlyHasTime")]
    public class ClaimsController : Controller
    {
        private readonly UserManager<User> userManager;

        public ClaimsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(CreateClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await userManager.FindByIdAsync(model.User);

            if (user == null)
            {
                ModelState.AddModelError("", $"User {model.User} Does Not Exists!");

                return View("Index", model);
            }

            var claim = new Claim(model.Type, model.Value);

            await userManager.AddClaimAsync(user, claim);

            return RedirectToAction("Index", "Home");
        }
    }
}