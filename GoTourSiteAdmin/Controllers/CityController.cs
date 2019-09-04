using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoTourSiteAdmin.Controllers
{
    public class CityController : Controller {
        private ICityService _vCityService;
        private ILanguageService _vLanguageSerivce;
        #region Constructor
        public CityController(ICityService vCityService, ILanguageService vLanguageService) {
            _vCityService = vCityService;
            _vLanguageSerivce = vLanguageService;
        }
        #endregion
        public IActionResult All() {
            return View();
        }
        public IActionResult AddCity() {
            return View();
        }
        public async Task<IActionResult> EditCity(Guid valId) {
            try {
                CityLanguageViewModel vCity = new CityLanguageViewModel();
                if (Startup._IdCity != Guid.Empty) {
                    vCity.ListCity = new List<CityViewModel>();
                    vCity = await _vCityService.GetCityById(valId);
                    return View(vCity);
                }
                
            } catch (Exception) {

                return View();
            }
            return View();
        }
        #region Add
        [HttpPost]
        public async Task<IActionResult> Add() {
            CityLanguageViewModel vModel = new CityLanguageViewModel();
            try {
                List<Language> vLanguage = await _vLanguageSerivce.GetLanguage();
                List<Guid> vIdLanguage = vLanguage.Select(e => e.Id).ToList();

                if (Request.Form.Count != 0 && vIdLanguage.Count != 0) {
                    vModel.ListCity = new List<CityViewModel>();
                    FileViewModel vFile = new FileViewModel();
                    if (Request.Form.Files != null && Request.Form.Files.Count > 0) {
                        IFormFile file = Request.Form.Files[0];
                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        vFile.FileData = data;
                        vFile.NameFile = file.Name;
                        vFile.Size = file.Length;
                    };
                    for (int i = 0; i <= vIdLanguage.Count - 1; i++) {
                        vModel.ListCity.Add(new CityViewModel {
                            Name = Request.Form["Name"],
                            Icon = Request.Form["Icono"],
                            Temperature = !string.IsNullOrEmpty(Request.Form["Temperature"]) ? Convert.ToInt32(Request.Form["Temperature"]) : 0,
                            Altitude = !string.IsNullOrEmpty(Request.Form["Altitude"]) ? Convert.ToInt32(Request.Form["Altitude"]) : 0,
                            Population = !string.IsNullOrEmpty(Request.Form["Population"]) ? Convert.ToInt32(Request.Form["Population"]) : 0,
                            UserCreate = "Admin",//change for session user
                            DateCreate = DateTime.Now,
                            State = 1,
                            Photo = vFile,
                            IdLanguage = vIdLanguage.ElementAt(i),
                            Slogan = Request.Form["data-slogan" + i],
                            Description = Request.Form["data-description" + i],
                            Location = Request.Form["data-location" + i],
                            FarmingProduction = Request.Form["data-farming-production" + i],
                            DescriptionDistricts = Request.Form["data-description-districts" + i],
                        });

                    }
                    ClientResponseViewModel vResponse = await _vCityService.AddCity(vModel);
                    if (vResponse != null) {
                        if (vResponse.Result == "1") {
                            return Json(new { content = "1" });
                        } else if (vResponse.Result == "2") {
                            return Json(new { content = vResponse.Result });
                        } else if (vResponse.Result == "5") {
                            return Json(new { content = vResponse.Result });
                        } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                            return Json(new { content = vResponse.Result });
                        }
                    }

                }
              
            } catch (Exception) {
                return Json(new { content ="6"});
            }

            return Json(new { content = "6" });
        }
        #endregion
        #region Update
        [HttpPost]
        public async Task<IActionResult> Edit() {
            CityLanguageViewModel vModel = new CityLanguageViewModel();
            try {
                

                if (Request.Form.Count != 0) {
                    vModel.ListCity = new List<CityViewModel>();
                    FileViewModel vFile = new FileViewModel();
                    if (Request.Form.Files != null && Request.Form.Files.Count > 0) {
                        IFormFile file = Request.Form.Files[0];
                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        vFile.FileData = data;
                        vFile.NameFile = file.Name;
                        vFile.Size = file.Length;
                    };
                    for (int i = 0; i <= 2; i++) {
                        vModel.ListCity.Add(new CityViewModel {
                            Name = Request.Form["Name"],
                            Icon = Request.Form["Icono"],
                            Temperature = !string.IsNullOrEmpty(Request.Form["Temperature"]) ? Convert.ToInt32(Request.Form["Temperature"]) : 0,
                            Altitude = !string.IsNullOrEmpty(Request.Form["Altitude"]) ? Convert.ToInt32(Request.Form["Altitude"]) : 0,
                            Population = !string.IsNullOrEmpty(Request.Form["Population"]) ? Convert.ToInt32(Request.Form["Population"]) : 0,
                            UserCreate = "Admin",//change for session user
                            DateCreate = DateTime.Now,
                            State = 1,
                            Photo = vFile,
                            IdLanguage = new Guid(Request.Form["id-language" + i]),
                            Slogan = Request.Form["data-slogan" + i],
                            Description = Request.Form["data-description" + i],
                            Location = Request.Form["data-location" + i],
                            FarmingProduction = Request.Form["data-farming-production" + i],
                            DescriptionDistricts = Request.Form["data-description-districts" + i],
                        });

                    }
                    ClientResponseViewModel vResponse = await _vCityService.AddCity(vModel);
                    if (vResponse != null) {
                        if (vResponse.Result == "1") {
                            return Json(new { content = "1" });
                        } else if (vResponse.Result == "2") {
                            return Json(new { content = vResponse.Result });
                        } else if (vResponse.Result == "5") {
                            return Json(new { content = vResponse.Result });
                        } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                            return Json(new { content = vResponse.Result });
                        }
                    }

                }

            } catch (Exception) {
                return Json(new { content = "6" });
            }

            return Json(new { content = "6" });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeState(string valId, string valState) {
            try {
                CityViewModel vCity = new CityViewModel();
                vCity.Id = new Guid(valId);
                vCity.State = Convert.ToInt32(valState);
                ClientResponseViewModel vResponse = await _vCityService.ChangeState(vCity);
                if (vResponse.Result == "1") {
                    return Json(new { content = "1", state = vCity.State });
                } else if (vResponse.Result == "3" || vResponse.Result == "4") {
                    return Json(new { content = vResponse.Result });
                }
            } catch (Exception) {
                return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });
            }
            return Json(new { content = "4", message = "Ha Ocurrido un error, no se puedo actualizar su estado" });
        }
        #endregion
        #region Get
        public async Task<IActionResult> GetData() {
            List<CityViewModel> vCity = new List<CityViewModel>();
            vCity = await _vCityService.GetCity();
            return Json(new { data = vCity });
        }
        //public async Task<IActionResult> GetDataById(Guid valId) {
        //    try {
        //        CityLanguageViewModel vModel = new CityLanguageViewModel();
        //        vModel.ListCity = new List<CityViewModel>();
        //        vModel = await _vCityService.GetCityById(valId);
        //    } catch (Exception) {

        //        return Json(new { content = "4" });
        //    }
        //    return Json(new { content = "4" });
        //}
        [HttpPost]
        public IActionResult ValidId(Guid vId) {
            if (vId != Guid.Empty) {
                Startup._IdCity = vId;
                return Json(new { content= "true" });
            }
            return Json(new { content = "false" });
        }
        #endregion
    }
}