using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTourWeb.Models;
using GoTourWeb.Helpers;
using GoTourWeb.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GoTourWeb.Controllers {

    public class HomeController : Controller {
        private IMenuService _vMenuService;
        private ITourService _vTourService;
        private IGeneralSearchService _vGeneralService;
        private ICityService _vCityService;
        private int _vRowsPerPage = 5;

        #region Constructor
        public HomeController(IMenuService valMenurService, ICityService valCityService, ITourService valTourService, IGeneralSearchService valGeneralSearchService) {
            _vMenuService = valMenurService;
            _vTourService = valTourService;
            _vCityService = valCityService;
            _vGeneralService = valGeneralSearchService;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index() {

            MenuViewModel vElementMenu = new MenuViewModel();
            List<TourViewModel> vTourRanking = new List<TourViewModel>();
            try {

                GetTextLanguage();
                Startup._vPageNumberReservation = 1;
                Startup._vPageNumberTours = 1;
                Startup._vPageNumberActivity = 1;
                string vIdLanguage = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefault = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefault;

                vElementMenu = await _vMenuService.GetElements(vIdLanguage);
                vTourRanking = await _vTourService.GetToursByRanking(vIdLanguage);
                ViewData["ListRankingTour"] = vTourRanking;

                if (vElementMenu == null) {
                    return View(new MenuViewModel());
                }
                vElementMenu.Languages.RemoveAll(x => x.Initials == vInitialsDefault);

                Startup._vDataMenu = vElementMenu;

                string vCurrencyInitialsDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                string vIdCurrency = GetCookie(Constants.NameCookieIdCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Id.ToString());
                ViewData["CurrencySymbolDefault"] = vCurrencyInitialsDefault;
                ViewData["IdCurrencyDefault"] = vIdCurrency;
                return View();
            } catch (Exception) {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region ChangeLanguage
        [HttpPost]
        public IActionResult ChangeLanguage([FromBody]LanguageViewModel valModel) {
            try {
                Response.Cookies.Append("LanguageGoTour", valModel.Id.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });
                Response.Cookies.Append("LanguageInitialsGoTour", valModel.Initials, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "1" });
        }
        #endregion

        #region ChangeCurrency
        [HttpPost]
        public IActionResult ChangeCurrency([FromBody]CurrencyViewModel valModel) {
            try {
                Response.Cookies.Append("CurrencyGoTour", valModel.Id.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });
                Response.Cookies.Append("CurrencyInitialsGoTour", valModel.Code, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "1" });
        }
        #endregion

        #region City
        [HttpPost]
        public IActionResult SelectCity([FromBody]CityViewModel valModel) {
            try {
                Startup._vIdCitySelected = valModel.Id;
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "1" });
        }


        #endregion

        #region  Search
        

        [HttpPost]
        public async Task<ActionResult> Search(SearchElementViewModel valModel) {
            GeneralSearchViewModel vListSearch = new GeneralSearchViewModel();
            List<GeneralResultViewModel> vListGeneralSearch = new List<GeneralResultViewModel>();
            string vIdLanguage = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
            try {
                vListSearch.IdLanguage = Guid.Parse(vIdLanguage);
                vListSearch.SearchText = valModel.ValueInput;
                if (vListSearch != null) {
                    vListGeneralSearch = await _vGeneralService.SearchPrincipal(vListSearch);
                }

            } catch (Exception) {

            }

            return PartialView("_SearchPrincipal", vListGeneralSearch);
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            CookieLanguage();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Common
        private void GetTextLanguage() {
            Language.LanguageViewRead();
            Language.MessageLangRead();
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

        private string GetCookie(string valNameCookie, string valValueDefault) {
            string vResult = valValueDefault;
            string vValueCookie = Request.Cookies[valNameCookie];
            if (!string.IsNullOrEmpty(vValueCookie)) {
                vResult = vValueCookie;
            }

            Response.Cookies.Append(valNameCookie, vResult, new CookieOptions() { Expires = DateTime.Now.AddMinutes(50), IsEssential = true });

            return vResult;
        }
        #endregion
    }
}
