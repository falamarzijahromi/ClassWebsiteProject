using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteHttp.Models;

namespace WebsiteHttp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;

        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterUser(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var errors = await CreateUserAsync(model);

            if (!string.IsNullOrEmpty(errors))
            {
                ModelState.AddModelError("", errors);
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> CreateUserAsync(UserRegisterViewModel model)
        {
            var user = new User
            {
                Id = model.UserName,
                Password = model.Password,
            };

            var result = await userManager.CreateAsync(user);

            var errorBuilder = result.Errors.Aggregate(new StringBuilder(), (seed, error) =>
            {
                seed.Append(error.Description + Environment.NewLine);

                return seed;
            });


            return errorBuilder.ToString();
        }
    }
}
