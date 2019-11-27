using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebsiteHttp.Models;

namespace WebsiteHttp.Controllers
{

    public class StudentsController : Controller
    {
        public IActionResult Index(int? studentId, [FromHeader(Name = "Secret-Code")]string secret)
        {
            if (studentId.HasValue)
            {
                var student = GetStudent(studentId.Value);

                return View(student);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult IndexWithObject(AddressViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }

        private StudentViewModel GetStudent(int value)
        {
            return new StudentViewModel
            {
                Name = "Ali",
                Family = "Rastian",
                MeliCode = "2280558756",
                Address = new AddressViewModel
                {
                    City = "Shiraz",
                    Province = "Fars",
                    Street = "Eram St.",
                },
            };
        }

        public IActionResult Register(StudentViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }


        public IActionResult EditStudentAddress(
            [Bind(Prefix = nameof(StudentViewModel.Address))]
            AddressViewModel studentAddress)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}