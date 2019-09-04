using System;
using System.Threading.Tasks;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GoTourWeb.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace GoTourWeb.Controllers {
    public class AccountController : Controller {

        private IUserService _vUserService;
        private IPaymentService _vPaymentService;
        private int _vRowsPerPage = 5;

        public AccountController(IUserService vUserService, IPaymentService valPaymentService) {
            _vUserService = vUserService;
            _vPaymentService = valPaymentService;
        }
        public IActionResult Login(string vReturnUrl = null) {
            CookieLanguage();


            UserViewModel vUser = new UserViewModel();
            Startup._vReturnUrl = vReturnUrl;

            string vUserNameCookie = Request.Cookies["UserName"];
            string vPasswordCookie = Request.Cookies["Password"];
            if (!string.IsNullOrEmpty(vUserNameCookie)) {
                vUser.UserName = vUserNameCookie;
            }
            if (!string.IsNullOrEmpty(vPasswordCookie)) {
                vUser.Password = vPasswordCookie;
            }

            return View(vUser);
        }

        

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserViewModel valUser) {
            try {
                UserViewModel vUserResponse = await _vUserService.Login(valUser.UserName.Trim(), valUser.Password.Trim());
                
                if (vUserResponse != null) {
                    HttpContext.Session.Set<UserViewModel>("UserSesion", vUserResponse);
                   
                    if (valUser.RememberMe) {

                        Response.Cookies.Append("UserName",valUser.UserName,new CookieOptions() { Expires = DateTime.Now.AddMinutes(30),IsEssential = true});
                        Response.Cookies.Append("Password", valUser.Password, new CookieOptions() { Expires = DateTime.Now.AddMinutes(30), IsEssential = true });

                    }
                    if (!string.IsNullOrEmpty(Startup._vReturnUrl)) {
                        return Json(new { content = "2" });
                    }
                    //return Redirect("/Reserve/CheckOut");
                    return Json(new { content = "1" });
                }
            } catch (Exception) {
                HttpContext.Session.Remove("UserSesion");
                return Json(new { content = "3" });
            }
            return Json(new { content = "3" });
        }

        [Route("signin/{provider}")]
        public IActionResult SignIn(string provider, string returnUrl = null) {

            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);

        } 
        public IActionResult Register() {
            Language.MessageLangRead();
            Language.LanguageViewRead();
            MenuViewModel vElementMenu = new MenuViewModel();
            CookieLanguage();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserViewModel valUser) {
            try {
                valUser.UserName = valUser.Email;
                ClientResponseViewModel vResponse = await _vUserService.Register(valUser);
                if (vResponse != null) {

                    if (vResponse.Result == "1") {

                            Response.Cookies.Append("UserName", valUser.UserName, new CookieOptions() { Expires = DateTime.Now.AddMinutes(30), IsEssential = true });
                            Response.Cookies.Append("Password", valUser.Password, new CookieOptions() { Expires = DateTime.Now.AddMinutes(30), IsEssential = true });

                        if (!string.IsNullOrEmpty(Startup._vReturnUrl)) {
                            return Json(new { content = "5" });
                        }

                        return Json(new { content = vResponse.Result});
                    } else if (vResponse.Result == "2") {
                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "3") {
                        return Json(new { content = vResponse.Result });
                    }


                }
            } catch (Exception) {
                return Json(new { content = "4", message = "Ha ocurrido un error en el registro", text = "" });
            }
            return Json(new { content = "4", message = "Ha ocurrido un error en el registro", text = "" });
        }

        public async Task<IActionResult> MyReservation(int valPage = 1) {
            PaymentResponseViewModel vPaymentResponse = new PaymentResponseViewModel();
            List<PaymentViewModel> vListPayment = new List<PaymentViewModel>();
            try {
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                UserViewModel vUserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                ViewData["UserDataSession"] = vUserSession;
                Startup._vPageNumberReservation = 1;
                Startup._vPageNumberTours = 1;
                Startup._vPageNumberActivity = 1;
                valPage = Startup._vPageNumberReservation;
                GetFilterReservation(Guid.Parse(vIdLanguageCookie), vUserSession.Id, valPage);
                vPaymentResponse= await _vPaymentService.SearchPayByUser(Startup._vFilterReservation);
                vListPayment = vPaymentResponse.Payments;
                double vRows = 1;
                if (vListPayment != null && vListPayment.Count > 0) {
                    vRows = Math.Ceiling(Convert.ToDouble(vPaymentResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                }
                ViewData["PageNumber"] = valPage;
                ViewData["RowsPerPage"] = vRows;
                if (vListPayment != null && vListPayment.Count > 0)
                    return View(vListPayment);
            } catch(Exception){
                return RedirectToAction("Index", "Home");
            }
            return View(new List<PaymentViewModel>());
        }

        private void GetFilterReservation(Guid valLaguage, Guid valUserIdSession, int valPage) {
            Startup._vFilterReservation.IdLanguage = valLaguage;
            Startup._vFilterReservation.IdUser = valUserIdSession;
            Startup._vFilterReservation.PageNumber = valPage;
            Startup._vFilterReservation.RowsPerPage = _vRowsPerPage;
        }

        public IActionResult Logout() {
            HttpContext.Session.Remove("UserSesion");
            return Json(new {
                content = "Ok",
                title = "¿Estás seguro?",
                text = "",
                secondtitle = "Su sesion ha terminado!",
                secondtext = "",
                confirm = "Sí",
                cancel = "no"
            }
            );

        }
        public void CookieLanguage() {
            Language.LanguageViewRead();
            MenuViewModel vElementMenu = new MenuViewModel();
            string vIdLanguage = Constants.LanguageDefault;
            string vInitialsDefault = Constants.InitialsLanguageDefault;


            string vLanguageCookie = Request.Cookies["LanguageGoTour"];
            string vInitialsCookie = Request.Cookies["LanguageInitialsGoTour"];

            if (!string.IsNullOrEmpty(vLanguageCookie)) {
                vIdLanguage = vLanguageCookie;
            }
            if (!string.IsNullOrEmpty(vInitialsCookie)) {
                vInitialsDefault = vInitialsCookie;
            }
            Response.Cookies.Append("LanguageGoTour", vIdLanguage, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });
            Response.Cookies.Append("LanguageInitialsGoTour", vInitialsDefault, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });

            ViewData["LanguageInitialDefault"] = vInitialsDefault;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterByExternalLogin([FromBody]UserViewModel valUser) {
            try {
                valUser.UserName = valUser.Email;
                ClientResponseViewModel vResponse = await _vUserService.Register(valUser);
                if (vResponse != null) {

                    if (vResponse.Result == "1") {
                        HttpContext.Session.Set<UserViewModel>("UserSesion", valUser);

                        return Json(new { content = vResponse.Result });
                    } else if (vResponse.Result == "2") {
                        return Json(new { content = vResponse.Result, title = "Datos incorrectos", text = "Algunos datos básicos no han sido proporcionados" });
                    } else if (vResponse.Result == "3") {
                        return Json(new { content = vResponse.Result, title = "Email existe", text = "El email ya se encuentra registrado" });
                    }


                }
            } catch (Exception) {
                return Json(new { content = "4", message = "Ha ocurrido un error en el registro", text = "" });
            }
            return Json(new { content = "4", message = "Ha ocurrido un error en el registro", text = "" });
        }

        [HttpPost]
        public async Task<IActionResult> LoginExternal([FromBody]UserViewModel valUser) {
            try {
                UserViewModel vUserResponse = await _vUserService.Login(valUser.Email.Trim(), valUser.Password.Trim());
                if (vUserResponse != null) {
                    HttpContext.Session.Set<UserViewModel>("UserSesion", vUserResponse);
                    return Json(new { content = "1" });
                }
            } catch (Exception) {
                HttpContext.Session.Remove("UserSesion");
                return Json(new { content = "null" });
            }
            return Json(new { content = "2" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRanking(PaymentViewModel valModel) {
            try {
                UserViewModel vUserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                valModel.UserUpdate = vUserSession.UserName;
                await _vPaymentService.UpdateRanking(valModel);
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "true" });

        }

        [HttpPost]
        public async Task<IActionResult> CancelAndRefund([FromBody]PaymentViewModel valModel) {
            UserViewModel UserSession = new UserViewModel();
            try {
                UserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vCurrencySymbolDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                if (valModel != null) {
                    string vIdCurrencyCookie = GetCookie(Constants.NameCookieIdCurrencySelected, Constants.CurrencyDefault);
                    valModel.IdLanguage = Guid.Parse(vIdLanguageCookie);
                    valModel.UserUpdate = UserSession.UserName;
                    valModel.State = 0;
                    valModel.IdCurrency = Guid.Parse(vIdCurrencyCookie);
                    valModel.LanguageInitials = vInitialsDefaultLanguage;
                    valModel.DateUpdate = DateTime.Now;
                   
                }
                 ClientResponseViewModel vResponse = await _vPaymentService.CancelAndRefund(valModel);
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
                         return Json(new {
                             content = vValueResponse,
                             title = GetTitleMessagePaymentFail(vValueResponse.ToString(), vInitialsDefaultLanguage),
                             text = GetTxtMessagePaymentFail(vValueResponse.ToString(), vInitialsDefaultLanguage)
                         });
                     }
                 
                 //Startup._vDataActivityCompanyByPass.Payment = valModel;
                 } else {
                     throw new Exception();
                 }
             
            } catch (Exception) {

            }
            return Json(new { content = "2" });
        }

        [HttpPost]
        public async Task<IActionResult> SearchReservation(PaymentSearchUserViewModel valModel) {
            PaymentResponseViewModel vPaymentResponse = new PaymentResponseViewModel();
            List<PaymentViewModel> vListPayment = new List<PaymentViewModel>();
            try {
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                UserViewModel vUserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                ViewData["UserDataSession"] = vUserSession;
                GetFilterReservation(Guid.Parse(vIdLanguageCookie), vUserSession.Id, valModel.PageNumber);
                vPaymentResponse = await _vPaymentService.SearchPayByUser(Startup._vFilterReservation);
                vListPayment = vPaymentResponse.Payments;
                double vRows = 1;
                if (vListPayment != null && vListPayment.Count > 0) {
                    vRows = Math.Ceiling(Convert.ToDouble(vPaymentResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                }
                Startup._vPageNumberReservation = valModel.PageNumber;
                ViewData["PageNumber"] = valModel.PageNumber;
                ViewData["RowsPerPage"] = vRows;
                if (vListPayment != null && vListPayment.Count > 0)
                    return PartialView("_ReservationList",vListPayment);
            } catch (Exception) {
                return RedirectToAction("Error", "Home");
            }
            return View(new List<PaymentViewModel>());
        }

        #region Detail Reservation
        [HttpPost]
        public IActionResult DetailsReservation(Guid valIdPAyment) {
            string vResult = "0";
            if (valIdPAyment != Guid.Empty) {
                Startup._vIdPayReservationsDetails = valIdPAyment;
                vResult = "1";
            }
            return Json(new { content = vResult.ToString() });
        }
        public async Task<IActionResult> Details() {
            PaymentViewModel vModel = new PaymentViewModel();

            try {
                UserViewModel vUserSession = HttpContext.Session.Get<UserViewModel>("UserSesion");
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                if (Startup._vIdPayReservationsDetails != Guid.Empty) {
                    PaymentSearchViewModel vPaymentSearch = new PaymentSearchViewModel();
                    vPaymentSearch.IdPayment = Startup._vIdPayReservationsDetails;
                    vPaymentSearch.IdLanguage = Guid.Parse(vIdLanguageCookie);

                    vModel = await _vPaymentService.SearchPay(vPaymentSearch);
                    if (vModel != null) {
                        vModel.NameUser = vUserSession.FirstName + " " + vUserSession.FirstLastName;
                        return View(vModel);
                    }
                }

            } catch (Exception) {

            }
            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region Common
        private void GetFilterReservation(int valPage) {
            Startup._vFilterReservation.PageNumber = valPage;
            Startup._vFilterReservation.RowsPerPage = _vRowsPerPage;
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
            return Language.GetMessageIn("MyReservation", valValueResponse, valLanguage, "Title");
        }

        private object GetTitleMessagePaymentFail(string valValueResponse, string valLanguage) {
            return Language.GetMessageIn("MyReservation", valValueResponse, valLanguage, "Text");
        }

        #endregion
    }
}