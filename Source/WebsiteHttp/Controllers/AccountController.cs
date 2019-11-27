using Common.Models;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await userManager.FindByIdAsync(userName);

            var model = new UserNationalEditViewModel
            {
                HashedPassword = user.Password,
                UserName = user.Id,
                NationalCode = user.NationalCode,
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditUser(
            [Bind(nameof(UserNationalEditViewModel.NationalCode))]
            UserNationalEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("GetUser", model);
            }

            var user = await userManager.FindByIdAsync(User.Identity.Name);

            user.NationalCode = model.NationalCode;

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> CreateUserAsync(UserRegisterViewModel model)
        {
            var user = new User
            {
                Id = model.UserName,
                Password = model.Password,
                NationalCode = model.NationalCode,
            };

            var result = await userManager.CreateAsync(user, user.Password);

            var errorBuilder = result.Errors.Aggregate(new StringBuilder(), (seed, error) =>
            {
                seed.Append(error.Description + Environment.NewLine);

                return seed;
            });


            return errorBuilder.ToString();
        }
    }
}
