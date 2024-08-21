using Microsoft.AspNetCore.Mvc;
using ProjectAkbas.Models;
using System.Diagnostics;

namespace ProjectAkbas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}