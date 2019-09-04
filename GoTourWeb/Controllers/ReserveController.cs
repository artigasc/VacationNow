using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourWeb.Helpers;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoTourWeb.Controllers {

    public class ReserveController : Controller {
        private IActivityService _vActivityService;
        private IPaymentService _vPaymentService;

        public ReserveController(IActivityService valActivityService, IPaymentService valPaymentService) {
            _vActivityService = valActivityService;
            _vPaymentService = valPaymentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectActivity(string valIdTour) {
            try {
                if (Guid.Parse(valIdTour) == Guid.Empty) {
                    throw new Exception();
                }
                Startup._vDataActivityByPass= Startup._vDataTourByPass.Activities.FirstOrDefault(i => i.Id == Guid.Parse(valIdTour));
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "true" });
        }

       
        public async Task<ActionResult> CheckOut() {
            ActivityCompanyViewModel vModel = new ActivityCompanyViewModel();
            try {
               
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vIdCurrencyCookie = GetCookie(Constants.NameCookieIdCurrencySelected, Constants.CurrencyDefault);
                string vCurrencyInitialsDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                ViewData["CurrencySymbolDefault"] = vCurrencyInitialsDefault;
                Language.MessageLangRead();
                if (Startup._vDataActivityByPass != null) {
                    FilterActivityCompanyViewModel vFilter = new FilterActivityCompanyViewModel() {
                        IdActivity = Startup._vDataActivityByPass.Id,
                        IdCompany = Startup._vDataActivityByPass.IdCompany,
                        IdCurrency = Guid.Parse(vIdCurrencyCookie),
                        IdLanguage = Guid.Parse(vIdLanguageCookie)
                    };
                    vModel = await _vActivityService.GetActivityCompany(vFilter);
                    Startup._vDataActivityCompanyByPass = vModel;
                    UserViewModel UserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                    if (UserSession != null) {
                        ViewData["TypeDocumentPayment"] = SeekListTypeDocument(UserSession.TypeNumberDocument, vInitialsDefaultLanguage);
                    } else {
                        ViewData["TypeDocumentPayment"] = CreateListTypeDocument(vInitialsDefaultLanguage);
                    }
                    
                    ViewData["TypeDocumentList"] = SeekListTypeDocument(vModel.Company.TypeNumberDocument, vInitialsDefaultLanguage);
                    return View(vModel);
                } else {
                    throw new Exception();
                }
            } catch (Exception) {
                return RedirectToAction("Error", "Home");
            }
           
        }

        private List<SelectListItem> CreateListTypeDocument(string valInitialsDefaultLanguage) {
            List<SelectListItem> vResult = new List<SelectListItem>();
            List<TypeDocumentElementViewModel> vListTypeDocument = TypeDocumentViewModel.GetList(valInitialsDefaultLanguage);
            foreach (TypeDocumentElementViewModel item in vListTypeDocument) {
                    vResult.Add(new SelectListItem { Text = item.Name, Value = item.Id, Selected = true });
                   
            }
            vResult.Reverse();
            return vResult;
        }

        private List<SelectListItem> SeekListTypeDocument(string valTypeNumberDocument, string valInitialsDefaultLanguage) {
            List<SelectListItem> vResult = new List<SelectListItem>();
            List<TypeDocumentElementViewModel> vListTypeDocument = TypeDocumentViewModel.GetList(valInitialsDefaultLanguage);
                foreach (TypeDocumentElementViewModel item in vListTypeDocument) {
                   if (item.Id == valTypeNumberDocument) {
                        vResult.Add(new SelectListItem { Text = item.Name, Value = item.Id, Selected = true });
                   } else {
                        vResult.Add(new SelectListItem { Text = item.Name, Value = item.Id });
                   }
                    
                }
                
            return vResult;
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToPayment([FromBody]PaymentViewModel valModel) {
            UserViewModel UserSession = new UserViewModel();
            try {
                UserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vCurrencySymbolDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                
                if (valModel != null) {
                    string vIdCurrencyCookie = GetCookie(Constants.NameCookieIdCurrencySelected, Constants.CurrencyDefault);
                    valModel.Id = Guid.NewGuid();
                    valModel.TotalMount = valModel.Mount * valModel.Persons;
                    valModel.IdUser = UserSession.Id;
                    valModel.PayMethod = Utilities.GetPaymentMethod(valModel.CardNumber);
                    valModel.State = 1;
                    valModel.IdCurrency = Guid.Parse(vIdCurrencyCookie);
                    valModel.LanguageInitials = vInitialsDefaultLanguage;
                    valModel.Symbol = vCurrencySymbolDefault;
                    valModel.UserCreate = UserSession.UserName;
                    valModel.DateCreate = DateTime.Today;
                    valModel.DateReserve = new DateTime(valModel.YearReserve,valModel.MonthReserve,valModel.DayReserve);

                    ClientResponseViewModel vResponse = await _vPaymentService.Register(valModel);
                    if (vResponse != null) {
                        int vValueResponse = Convert.ToInt16(vResponse.Result);
                        if (vValueResponse == 1) {
                            Startup._vIdPaymentRegistered = valModel.Id;
                            return Json(new { content = vValueResponse });
                        } else if (vValueResponse == 2 || vValueResponse == 3 || vValueResponse == 4) {
                            return Json(new {
                                content = vValueResponse,
                                title = GetTitleMessagePaymentFail(vValueResponse.ToString(), vInitialsDefaultLanguage),
                                text = GetTxtMessagePaymentFail("2", vInitialsDefaultLanguage)
                            });
                        } else if (vValueResponse > 4) {
                            Startup._vIdPaymentRegistered = Guid.Empty;
                            return Json(new { content = vValueResponse,
                                              title = GetTitleMessagePaymentFail(vValueResponse.ToString(), vInitialsDefaultLanguage),
                                              text = GetTxtMessagePaymentFail(vValueResponse.ToString(), vInitialsDefaultLanguage) });
                        }
                    }
                    //Startup._vDataActivityCompanyByPass.Payment = valModel;
                } else {
                    throw new Exception();
                }
                
            } catch (Exception) {
                
            }
            return Json(new { content = "2" });
        }

       

        public async Task<IActionResult> Confirmation() {
            PaymentViewModel vModel = new PaymentViewModel();
            
            try {
                UserViewModel vUserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                if (Startup._vIdPaymentRegistered != Guid.Empty) {
                    PaymentSearchViewModel vPaymentSearch = new PaymentSearchViewModel();
                    vPaymentSearch.IdPayment = Startup._vIdPaymentRegistered;
                    vPaymentSearch.IdLanguage = Guid.Parse(vIdLanguageCookie);
                    
                    vModel = await _vPaymentService.SearchPay(vPaymentSearch);
                    if(vModel != null){
                        vModel.NameUser = vUserSession.FirstName + " " + vUserSession.FirstLastName;
                        return View(vModel);
                    }
                }
               
            } catch (Exception) {

            }
            return RedirectToAction("Error", "Home");
        }

        private string GetCookie(string valNameCookie, string valValueDefault) {
            string vResult = valValueDefault;
            string vValueCookie = Request.Cookies[valNameCookie];
            if (!string.IsNullOrEmpty(vValueCookie)) {
                vResult = vValueCookie;
            }

            Response.Cookies.Append(valNameCookie, vResult, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });

            return vResult;
        }

        private object GetTxtMessagePaymentFail(string valValueResponse, string valLanguage) {
            return Language.GetMessageIn("CheckOut", valValueResponse, valLanguage, "Title");
        }

        private object GetTitleMessagePaymentFail(string valValueResponse, string valLanguage) {
            return Language.GetMessageIn("CheckOut", valValueResponse, valLanguage, "Text");
        }
    }


}
