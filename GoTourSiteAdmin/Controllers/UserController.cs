using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoTourSiteAdmin.Controllers
{
    public class UserController : Controller {
        private IUserService _vUserService;
        public IActionResult AllUser() {
            return View();
        }
        public IActionResult Details() {
            return View();
        }
        public UserController(IUserService vUserService) {
            _vUserService = vUserService;
        }
        #region Get
        public async Task<IActionResult> GetData() {
            List<UserViewModel> vList = new List<UserViewModel>();
            vList = await _vUserService.GetUser();
            return Json(new { data = vList });
        }
        public async Task<IActionResult> Detail() {
            UserViewModel vUser = new UserViewModel();
            var valId = Startup._IdUserPortal;
            if (valId != Guid.Empty) {
                vUser = await _vUserService.GetUserById(valId);
                Startup._IdUserPortal = Guid.Empty;
                return View("Details", vUser);
            }
            return RedirectToAction("Error", "Home");
        }
        public async Task<IActionResult> Edit() {
            UserViewModel vUser = new UserViewModel();
            var valId = Startup._IdUserPortal;
            if (valId != Guid.Empty) {
                vUser = await _vUserService.GetUserById(valId);
                return View("EditUser", vUser);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public IActionResult GetDataUser(Guid valId) {
            //var valId = new Guid(valId);
            if (valId != Guid.Empty) {
                Startup._IdUserPortal = valId;
                return Json(new { content = "True" });
            }
            return Json(new { content = "False" });
        }
        //public void UserViewModel() {
        //    Startup._vDataUser = Models.UserViewModel.ListUser;
        //}
        #endregion
        #region Update
        [HttpPost]
        public async Task<IActionResult> UpdateState(string valId, string valState) {
            try {
                UserViewModel vModel = new UserViewModel();
                vModel.Id = new Guid(valId);
                vModel.State = Convert.ToInt32(valState);
                ClientResponseViewModel vResponse = await _vUserService.UpdateState(vModel);
                if(vResponse.Result =="1") {
                    return Json( new { content="1" });
                }else if(vResponse.Result == "3" || vResponse.Result == "4") {
                    return Json( new { content = vResponse.Result });
                }
            } catch (Exception) {
                return Json(new { content = "5" });
            }
            return Json(new { content = "5" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserViewModel vModel) {
            try {
                ClientResponseViewModel vResponse = await _vUserService.Update(vModel);
                if(vResponse.Result == "1") {
                    return Json(new { content = vResponse.Result });
                } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                    return Json(new { content= vResponse.Result });
                }
            } catch (Exception) {
                return Json(new { content = "5" });
            }
            return Json(new { content = "5" });
        }
        #endregion
        //[HttpPost]
        // public IActionResult EditUser(string valId) {
        //        UserViewModel ListUser = new UserViewModel();
        //        if(!string.IsNullOrEmpty(valId)) {
        //           ListUser = Startup._vDataUser.FirstOrDefault(i => i.Id.ToString() == valId);
        //        }
        //        return PartialView("_EditUser", ListUser);

        // }
        //[HttpPost]
        //public IActionResult Delete(string valId) {
        //    UserViewModel vUser = new UserViewModel();

        //    vUser = Startup._vDataUser.FirstOrDefault(i => i.Id.ToString() == valId);
        //    vUser.State = 1;

        //    return PartialView("_TableUser", vUser);
        //}

        //[HttpPost]
        //public IActionResult Edit([FromBody] UserViewModel vModel) {

        //    List<UserViewModel> vList = new List<UserViewModel>();
        //    UserViewModel vUser = new UserViewModel();
        //    if (vModel!= null) {
        //        vUser = Startup._vDataUser.FirstOrDefault(i => i.Id == vModel.Id);
        //    }
        //    vUser.Username = vModel.Username;
        //    vUser.FirstName = vModel.FirstName;
        //    vUser.FirstLastName = vModel.FirstLastName;
        //    vUser.Email = vModel.Email;
        //    vUser.Phone = vModel.Phone;
        //    vUser.UrlPhoto = vModel.UrlPhoto;
        //    vUser.State = 1;
        //    vList.Add(vUser);

        //    List<UserViewModel> vListNoEdit = Startup._vDataUser.Where(i => i.Id != vModel.Id).ToList();
        //    vList.AddRange(vListNoEdit);

        //    //foreach (var vItem in vUser.Where(w => w.Id.ToString() == vModel.Id.ToString())) {
        //    //    vItem.Username = vModel.Username;
        //    //    vItem.Phone = vModel.Phone;
        //    //    vItem.Email = vModel.Email;
        //    //}

        //    return PartialView("_TableUser", vList);
        //}

    }
}