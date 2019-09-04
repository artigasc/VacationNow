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
using System.Globalization;

namespace GoTourWeb.Controllers {

    public class ToursController : Controller {
        private IActivityService _vActivityService;
        private IGeneralSearchService _vGeneralService;

        private int _vRowsPerPage = 5;

        #region Constructor
        public ToursController(IActivityService valActivityService,IGeneralSearchService valGeneralService) {
            _vActivityService = valActivityService;
            _vGeneralService = valGeneralService;

        }
        #endregion

        #region Info
        public async Task<IActionResult> Info(int valPage = 1) {
            TourViewModel vModel = new TourViewModel();

            try {
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vCurrencyInitialsDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                string vIdCurrency = GetCookie(Constants.NameCookieIdCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Id.ToString());
                Startup._vPageNumberReservation = 1;
                Startup._vPageNumberTours = 1;
                CreatingViewDatasInfo(vInitialsDefaultLanguage, vCurrencyInitialsDefault, vIdCurrency);
                valPage = Startup._vPageNumberActivity;
                if (Startup._vIdTourSelected == Guid.Empty)
                    throw new Exception();
                SetFilter(Startup._vIdTourSelected, Guid.Parse(vIdLanguageCookie), Guid.Parse(vIdCurrency),0, 
                    DateTime.MinValue, _vRowsPerPage, valPage);
                ActivityResponseViewModel vModelResponse = await ListInfoTour();
                vModel.Activities = vModelResponse.Activities;
                if (vModel != null && vModel.Activities != null && vModel.Activities.Count() > 0) {
                    Startup._vDataTourByPass = vModel;
                    vModel.Activities = vModel.Activities.OrderBy(i => i.Name).ToList();
                    vModel.Id = Startup._vFilterActivity.IdTour;
                    SetInfoRestantTour(ref vModel);
                    double vRows = Math.Ceiling(Convert.ToDouble(vModelResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                    CreateViewDataPagination(valPage, vRows);
                    return View(vModel);
                }
                CreateViewDataPagination(valPage, 5);
              
                return View(vModel);
            } catch (Exception) {
                return RedirectToAction("Index","Home");
            }
        }


        #endregion

        #region SearchActivityFilter
        [HttpPost]
        public async Task<IActionResult> SearchActivity(FilterActivityViewModel valModel) {
            TourViewModel vModel = new TourViewModel();
            try {
                valModel.DateStart = ConvertStringToDate(valModel.DateStartString);
                
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vIdCurrencyCookie = GetCookie(Constants.NameCookieIdCurrencySelected, Constants.CurrencyDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                Startup._vPageNumberActivity = valModel.PageNumber;
                SetFilter(Startup._vIdTourSelected, Guid.Parse(vIdLanguageCookie), Guid.Parse(vIdCurrencyCookie),
                    valModel.MinimumPeople, valModel.DateStart, _vRowsPerPage, valModel.PageNumber);
                ActivityResponseViewModel vModelResponse = await ListInfoTour();
                vModel.Activities = vModelResponse.Activities;
                if (vModel != null && vModel.Activities != null && vModel.Activities.Count() > 0) {
                    Startup._vDataTourByPass = vModel;
                    vModel.Activities = vModel.Activities.OrderBy(i => i.Name).ToList();
                    double vRows = Math.Ceiling(Convert.ToDouble(vModelResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                    CreateViewDataPagination(valModel.PageNumber, vRows);
                    return PartialView("_ActivityList", vModel);
                }
                CreateViewDataPagination(1, 5);
            } catch (Exception) {
                vModel = new TourViewModel();
                vModel.Activities = new List<ActivityViewModel>();
            }
            return PartialView("_ActivityList", vModel);

        }

        #endregion

        #region Checkout
        public IActionResult Checkout() {
            try {
                Language.LanguageViewRead();
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vIdCurrencyCookie = GetCookie(Constants.NameCookieIdCurrencySelected, Constants.CurrencyDefault);
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                return View();
            } catch (Exception) {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

 


        #region Common
        private async Task<ActivityResponseViewModel> ListInfoTour() {
            ActivityResponseViewModel vResult = new ActivityResponseViewModel();
            try {
                vResult = await _vActivityService.GetActivities(Startup._vFilterActivity);
            } catch (Exception) {
                vResult = new ActivityResponseViewModel();
            }
            return vResult;
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
        private void SetFilter(Guid vIdTour, Guid vIdLanguage, Guid valIdCurrency, int valMinimumPeople, DateTime valDateStart,
            int valRowsPerPage, int valPage) {
            Startup._vFilterActivity.IdTour = vIdTour;
            Startup._vFilterActivity.IdLanguage = vIdLanguage;
            Startup._vFilterActivity.IdCurrency = valIdCurrency;
            Startup._vFilterActivity.MinimumPeople = valMinimumPeople;
            Startup._vFilterActivity.DateStart = valDateStart;
            Startup._vFilterActivity.RowsPerPage = valRowsPerPage;
            Startup._vFilterActivity.PageNumber = valPage;
        }

       

        private void CreatingViewDatasInfo(string valInitialsDefaultLanguage, string valCurrencyInitialsDefault, string valIdCurrency) {
            ViewData["CurrencySymbolDefault"] = valCurrencyInitialsDefault;
            ViewData["IdCurrencyDefault"] = valIdCurrency;
            ViewData["LanguageInitialDefault"] = valInitialsDefaultLanguage;
        }

        private void SetInfoRestantTour(ref TourViewModel vModel) {
            TourViewModel vTourViewModel = vModel;
            TourViewModel vTour = Startup._vDataCityTours.Tours.Where(i => i.Id == vTourViewModel.Id).FirstOrDefault();
            vModel.Name = vTour.Name;
            vModel.UrlPhoto = vTour.UrlPhoto;
        }

        private void CreateViewDataPagination(int valPageNumber, double valRows) {
            ViewData["PageNumber"] = valPageNumber;
            ViewData["RowsPerPage"] = valRows;
        }

        private DateTime ConvertStringToDate(string valDateStartString) {
            DateTime vResult = DateTime.MinValue;
            try {
                vResult = DateTime.ParseExact(valDateStartString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return vResult;
            } catch (Exception) { }
            return vResult;
        }

        #endregion


    }
}
