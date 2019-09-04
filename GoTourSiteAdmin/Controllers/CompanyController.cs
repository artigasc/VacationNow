using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoTourSiteAdmin.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyService _vCompanyService;
        #region Constructor
        public CompanyController(ICompanyService vCompanyService) {
             _vCompanyService = vCompanyService;
        }
        #endregion
        public IActionResult All() {
            return View();
        }

        public IActionResult AddCompany() {
            return View();
        }
        public IActionResult Edit() {
            return View();
        }
        public async Task<IActionResult> Detail() {
            CompanyViewModel vCompany = new CompanyViewModel();
            var valId = Startup._IdCompany;
            if (valId != Guid.Empty) {
                vCompany = await _vCompanyService.GetCompanyById(valId);
                Startup._IdUserPortal = Guid.Empty;
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        #region Add
        public async Task<IActionResult> Add() {
            try {
                CompanyViewModel vModel = new CompanyViewModel();
                if (Request.Form.Count() != 0) {
                    vModel.Name = Request.Form["Name"];
                    vModel.Address = Request.Form["Address"];
                    vModel.TypeNumberDocument = Convert.ToInt32(Request.Form["TypeNumberDocument"]);
                    vModel.NumberDocument = Request.Form["NumberDocument"];
                    vModel.Phone = Request.Form["Phone"];
                    vModel.Movil = Request.Form["Movil"];
                    vModel.Email = Request.Form["Email"];
                    vModel.OptionalEmail = Request.Form["OptionalEmai"];
                    vModel.Web = Request.Form["Web"];
                    vModel.State = 1;
                    vModel.UserCreate = "Admin";
                    vModel.DateCreate = DateTime.Now;
                    if (Request.Form.Files.Count() != 0 && Request.Form.Files != null) {
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
                    ClientResponseViewModel vResponse = await _vCompanyService.Register(vModel);
                    if (vResponse.Result == "1") {
                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "2") {
                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                        return Json(new { content = vResponse.Result });
                    }

                }


            } catch (Exception) {
                return Json(new { content = "5"});
            }
            return Json(new { content = "5" });
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<IActionResult > ChangeState(string valId, string valState) {
            try {
                CompanyViewModel vCompany = new CompanyViewModel();
                vCompany.Id = new Guid(valId);
                vCompany.State = Convert.ToInt32(valState);
                ClientResponseViewModel vResponse = await _vCompanyService.ChangeState(vCompany);
                if (vResponse.Result == "1") {
                    return Json(new { content = "1", state = vCompany.State });
                } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                    return Json(new { content = vResponse.Result });
                }
            } catch (Exception) {
                return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });
            }
            return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });
        }
        [HttpPost]
        public async Task<IActionResult> Update() {
            try {
                CompanyViewModel vModel = new CompanyViewModel();
                if (Request.Form.Count() != 0) {
                    vModel.Name = Request.Form["Name"];
                    vModel.Address = Request.Form["Address"];
                    vModel.TypeNumberDocument = Convert.ToInt32(Request.Form["TypeNumberDocument"]);
                    vModel.NumberDocument = Request.Form["NumberDocument"];
                    vModel.Phone = Request.Form["Phone"];
                    vModel.Movil = Request.Form["Movil"];
                    vModel.Email = Request.Form["Email"];
                    vModel.OptionalEmail = Request.Form["OptionalEmai"];
                    vModel.Web = Request.Form["Web"];
                    vModel.State = 1;
                    vModel.UserCreate = "Admin";
                    vModel.DateCreate = DateTime.Now;
                    if (Request.Form.Files.Count() != 0 && Request.Form.Files != null) {
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
                    ClientResponseViewModel vResponse = await _vCompanyService.Update(vModel);
                    if (vResponse.Result == "1") {
                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "2") {
                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                        return Json(new { content = vResponse.Result });
                    }

                }
            } catch (Exception) {
                return Json(new { content = "5" });
            }
            return Json(new { content = "5" });
        }
        #endregion

        #region Get
        public async Task<IActionResult> GetData() {
            List<CompanyViewModel> vListComapny = await _vCompanyService.GetCompany();
            return Json(new { data = vListComapny});
        }
        public IActionResult GetDataById(string valId) {
            if (valId != string.Empty) {
                Startup._IdCompany = new Guid(valId);
                return Json(new { content = "True" });
            }
            return Json(new { content = "False" });

        }
        #endregion


    }
}