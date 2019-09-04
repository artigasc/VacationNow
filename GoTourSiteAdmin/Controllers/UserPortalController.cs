using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoTourSiteAdmin.Controllers {
    public class UserPortalController : Controller {
        private ICompanyService _vCompanyService;
        private IUserPortalService _vUserPortalService;

        #region Constructor
        public UserPortalController(ICompanyService vCompanyService, IUserPortalService vUserPortalService) {
            _vCompanyService = vCompanyService;
            _vUserPortalService = vUserPortalService;
        }
        #endregion

        public void UserPortalViewModel() {
            Startup._vDataUserPortal = Models.UserPortalViewModel.ListUser;
        }
        public IActionResult All() {
            return View();
        }
        public async Task<IActionResult> Edit() {
            UserPortalViewModelResponse vUser = new UserPortalViewModelResponse();
            if (Startup._IdUserPortal != Guid.Empty) {
                vUser = await _vUserPortalService.GetUserById(Startup._IdUserPortal.ToString());
                Startup._IdUserPortal = Guid.Empty;

                List<CompanyViewModel> vCompany = await _vCompanyService.GetCompany();
                ViewData["vCompanyList"] = vCompany;

                return View("EditUserPortal", vUser);
            }

            //pemdiente session
            return RedirectToAction("Error", "Home");
        }
        public async Task<IActionResult> AddUserPortal() {
            List<CompanyViewModel> vCompany = await _vCompanyService.GetCompany();
            return View(vCompany);
        }
        public async Task<IActionResult> Detail() {
            UserPortalViewModelResponse vUser = new UserPortalViewModelResponse();
            if (Startup._IdUserPortal != Guid.Empty) {
                vUser = await _vUserPortalService.GetUserById(Startup._IdUserPortal.ToString());
                Startup._IdUserPortal = Guid.Empty;
                return View("Details", vUser);
            }
            return RedirectToAction("Error", "Home");
        }

        #region Get
        public async Task<IActionResult> GetData() {
            UserPortalViewModel();
            List<UserPortalViewModelResponse> vListUser = await _vUserPortalService.GetUser();
            return Json(new { data = vListUser });
        }
        [HttpPost]
        public IActionResult GetDataUserPortal(string valId) {

            if (valId != string.Empty) {
                Startup._IdUserPortal = new Guid(valId);
                return Json(new { content = "True" });
            }
            return Json(new { content = "False" });

        }
        #endregion
        #region Add
        [HttpPost]
        public async Task<IActionResult> AddUser() {
            try {
                UserPortalViewModel vModel = new UserPortalViewModel();
                if (Request.Form.Count != 0) {
                    vModel.UserName = Request.Form["UserName"];
                    vModel.Password = Request.Form["Password"];
                    vModel.Email = Request.Form["Email"];
                    vModel.FirstName = Request.Form["FirstName"];
                    vModel.SecondName = Request.Form["SecondName"];
                    vModel.FirstLastName = Request.Form["FirstLastName"];
                    vModel.SecondLastName = Request.Form["SecondLastName"];
                    vModel.BackMail = Request.Form["BackMail"];
                    vModel.Phone = Request.Form["Phone"];
                    vModel.IdCompany = new Guid(Request.Form["Company"]);
                    vModel.DateCreate = DateTime.Today;
                    vModel.UserCreate = "Admin";
                    vModel.BirthDate = string.Empty;
                    vModel.CompanyName = string.Empty;
                    vModel.UrlPhoto = string.Empty;
                    vModel.State = 1;

                    if (Request.Form.Files != null && Request.Form.Files.Count > 0) {
                        IFormFile file = Request.Form.Files[0];
                        byte[] data;
                        FileViewModel vFile = new FileViewModel();
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        vFile.FileData = data;
                        vFile.NameFile = file.Name;
                        vFile.Size = file.Length;
                        vModel.Photo = vFile;

                    }
                    ClientResponseViewModel vResponse = await _vUserPortalService.Register(vModel);
                    if (vResponse != null) {
                        if (vResponse.Result == "1") {
                            return Json(new { content = "1" });
                        } else if (vResponse.Result == "2") {
                            return Json(new { content = vResponse.Result });
                        } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                            return Json(new { content = vResponse.Result });
                        }
                    }
                }

            } catch (Exception) {
                return Json(new { content = "5", message = "Ha Ocurrido un error, no se puedo agregar" });
            }

            return Json(new { content = "5", message = "Ha Ocurrido un error, no se puedo agregar" });
        }
        #endregion      
        #region Update
        [HttpPost]
        public async Task<IActionResult> EditUser() {
            try {
                UserPortalViewModel vModel = new UserPortalViewModel();
                if (Request.Form.Count != 0) {
                    vModel.Id = new Guid(Request.Form["Id"]);
                    vModel.Password = Request.Form["Password"];
                    vModel.FirstName = Request.Form["FirstName"];
                    vModel.SecondName = Request.Form["SecondName"];
                    vModel.FirstLastName = Request.Form["FirstLastName"];
                    vModel.SecondLastName = Request.Form["SecondLastName"];
                    vModel.BirthDate = Request.Form["BirthDate"];
                    vModel.BackMail = Request.Form["BackMail"];
                    vModel.Phone = Request.Form["Phone"];
                    vModel.IdCompany = new Guid(Request.Form["Company"]);
                    vModel.UserUpdate = "Admin";
                    vModel.UrlPhoto = string.Empty;
                    vModel.State = 1;

                    if (Request.Form.Files != null && Request.Form.Files.Count > 0) {
                        IFormFile file = Request.Form.Files[0];
                        byte[] data;
                        FileViewModel vFile = new FileViewModel();
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        vFile.FileData = data;
                        vFile.NameFile = file.Name;
                        vFile.Size = file.Length;
                        vModel.Photo = vFile;
                    }
                    ClientResponseViewModel vResponse = await _vUserPortalService.Update(vModel);
                    if (vResponse != null) {
                        if (vResponse.Result == "1") {
                            return Json(new { content = "1" });
                        }else if (vResponse.Result == "3" || vResponse.Result == "4") {
                            return Json(new { content = vResponse.Result });
                        }
                    }
                }

            } catch (Exception) {
                return Json(new { content = "5", message = "Ha Ocurrido un error, no se puedo agregar" });
            }

            return Json(new { content = "5", message = "Ha Ocurrido un error, no se puedo agregar" });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeState(string valId, string valState) {
            try {
                UserPortalViewModel vUser = new UserPortalViewModel();
                vUser.Id = new Guid(valId);
                vUser.State = Convert.ToInt32(valState);
                ClientResponseViewModel vResponse = await _vUserPortalService.ChangeState(vUser);
                if (vResponse.Result == "1") {
                    return Json(new { content = "1", state = vUser.State });
                } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                    return Json(new { content = vResponse.Result });
                }
            } catch (Exception) {
                return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });
            }
            return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });

        }

        #endregion

    }
}