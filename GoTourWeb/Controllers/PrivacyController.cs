using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoTourWeb.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
    }
}