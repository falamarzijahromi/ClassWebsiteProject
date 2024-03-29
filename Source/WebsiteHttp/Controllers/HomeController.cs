﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebsiteHttp.Models;

namespace WebsiteHttp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string lang, int version)
        {
            var allData = RouteData.Values["catchAllData"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("diff/{message:int?}")]
        public string Different(int? message)
        {
            return message.HasValue ? message.Value.ToString() : "Done" ;
        }
    }
}
