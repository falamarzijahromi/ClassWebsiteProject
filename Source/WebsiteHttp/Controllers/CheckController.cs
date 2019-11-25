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
    [Route("something/[controller]/[action]")]
    public class CheckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckValidation(CheckViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            return null;
        }
    }
}