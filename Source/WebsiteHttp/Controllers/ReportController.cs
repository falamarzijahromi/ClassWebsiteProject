using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebsiteHttp.Controllers
{
    public class ReportController : Controller
    {
        [Authorize(Roles = "ADMIN,MANAGER")]
        public IActionResult Index()
        {
            return View();
        }
    }
}