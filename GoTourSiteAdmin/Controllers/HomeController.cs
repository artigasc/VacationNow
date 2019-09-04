using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;

namespace GoTourSiteAdmin.Controllers {
    public class HomeController : Controller {

        private IAccountService _vUserService;
        public HomeController(IAccountService vUserService) {
            _vUserService = vUserService;
        }
        public IActionResult Login(string vReturnUrl = null) {

            Startup._vReturnUrl = vReturnUrl;
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserPortalViewModel vModel) {
            string valUserName = vModel.UserName;
            string valPassword = vModel.Password;
            try {
                UserPortalViewModel vUser = await _vUserService.Login(valUserName.Trim(), valPassword.Trim());
                if (vUser != null) {
                    return Json(new { content = "1" });
                }
            } catch (Exception) {
                return Json(new { content = "2" });
            }
            return Json(new { content = "2" });
        }
        public IActionResult Dashboard() {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
