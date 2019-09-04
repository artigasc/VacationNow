using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourWeb.Helpers;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoTourWeb.Controllers {

    public class CityController : Controller {
        private IMenuService _vMenuService;
        private ICityService _vCityService;
        private ITourService _vTourService;
        private IGeneralSearchService _vGeneralService;
        private int _vRowsPerPage = 5;

        #region Constructor
        public CityController(IMenuService valMenurService,IGeneralSearchService valGeneralService, ICityService valCityService, ITourService valTourService) {
            _vMenuService = valMenurService;
            _vCityService = valCityService;
            _vTourService = valTourService;
            _vGeneralService = valGeneralService;
        }
        #endregion

        #region Info
        public async Task<IActionResult> Info(int valPage = 1) {
            CityViewModel vModel = new CityViewModel();
            try {
                List<TourViewModel> vRankingTour = new List<TourViewModel>();
                Startup._vPageNumberReservation = 1;
                Startup._vPageNumberActivity = 1;
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vCurrencyInitialsDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                string vIdCurrency = GetCookie(Constants.NameCookieIdCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Id.ToString());
                Guid vIdCity = Startup._vIdCitySelected;

                vRankingTour = await _vTourService.GetToursByRanking(vIdLanguageCookie);
                ViewData["ListRankingTour"] = vRankingTour;

                if (Startup._vDataMenu.Cities != null || Startup._vDataMenu.Cities.Count() > 0) {
                    vModel = Startup._vDataMenu.Cities.Where(i => i.Id == vIdCity).FirstOrDefault();
                }
                valPage = Startup._vPageNumberTours;
                string[] vCategories = Startup._vDataMenu.Categories.Select(k => k.Id.ToString()).ToArray();
                SetFilter(vIdCity, Guid.Parse(vIdLanguageCookie),0, int.MaxValue,0,
                    int.MaxValue,0, int.MaxValue,Guid.Parse(vIdCurrency), vCategories, valPage);
                
                TourResponseViewModel vModelResponse = await ListInfoCity();
                vModel.Tours = vModelResponse.Tours.OrderBy(i => i.Score).ToList();
                vModel.OrdersTours = OrderingTourViewModel.GetList(vInitialsDefaultLanguage);
                double vRows = 1;
                if(vModel.Tours != null && vModel.Tours.Count > 0) {
                    vRows = Math.Ceiling(Convert.ToDouble(vModelResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                }
                CreateViewDataElementInfo(vCurrencyInitialsDefault, vIdCurrency, vInitialsDefaultLanguage, valPage, vRows, vModel.OrdersTours.FirstOrDefault().Name);
                Startup._vDataCityTours = vModel;
                return View(vModel);
            } catch (Exception) {
                return View(new CityViewModel());
            }
        }


        #endregion

        #region Search Filter and ByPassTours
        [HttpPost]
        public async Task<IActionResult> SearchTours(FilterToursViewModel valModel) {
            CityViewModel vModel = new CityViewModel();
            try {
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vIdCurrency = GetCookie(Constants.NameCookieIdCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Id.ToString());
                ViewData["LanguageInitialDefault"] = vInitialsDefaultLanguage;
                string vIdCity = GetCookie(Constants.NameCookieIdCitySelected, Constants.CityDefault);
                if (Startup._vDataMenu.Cities != null || Startup._vDataMenu.Cities.Count() > 0) {
                    vModel = Startup._vDataMenu.Cities.Where(i => i.Id == Guid.Parse(vIdCity)).FirstOrDefault();
                }
                SetFilter(Guid.Parse(vIdCity), Guid.Parse(vIdLanguageCookie), GetStartElement(valModel.Durations),
                    GetLastElement(valModel.Durations), GetStartElement(valModel.Ranking), GetLastElement(valModel.Ranking),
                    GetStartElement(valModel.Prices), GetLastElement(valModel.Prices),Guid.Parse(vIdCurrency), valModel.Categories, valModel.PageNumber);
                TourResponseViewModel vModelResponse = await ListInfoCity();
                vModel.Tours = vModelResponse.Tours;
                Startup._vDataCityTours = vModel;
                OrderTours(ref vModel, Startup._OrderingTourDefault);
                double vRows = Math.Ceiling(Convert.ToDouble(vModelResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                Startup._vPageNumberTours = valModel.PageNumber;
                ViewData["PageNumber"] = valModel.PageNumber;
                ViewData["RowsPerPage"] = vRows;
            } catch (Exception) {
                vModel = new CityViewModel();
                vModel.Tours = new List<TourViewModel>();
            }
            return PartialView("_TourList", vModel);

        }

        [HttpPost]
        public IActionResult SelectTours(FilterToursViewModel valModel) {
            try {
                Startup._vIdTourSelected = valModel.IdTour;
            } catch (Exception) {
                return Json(new { content = "null" });
            }
            return Json(new { content = "true" });

        }
        #endregion

        #region Ordering
        [HttpPost]
        public IActionResult ChangeOrdering(string valId) {
            int vCriteria = Convert.ToInt32(valId);
            Startup._OrderingTourDefault = vCriteria;
            return Json(new { content = "1" });
        }

        #endregion

        #region SearchCity Searcher

        public async Task<IActionResult> InfoSearch(int valPage = 1) {
            CityViewModel vModel = new CityViewModel();
            try {
                List<TourViewModel> vRankingTour = new List<TourViewModel>();
                Startup._vPageNumberReservation = 1;
                Startup._vPageNumberActivity = 1;
                string vIdLanguageCookie = GetCookie(Constants.NameCookieIdLanguage, Constants.LanguageDefault);
                string vInitialsDefaultLanguage = GetCookie(Constants.NameCookieInitialsLanguage, Constants.InitialsLanguageDefault);
                string vCurrencyInitialsDefault = GetCookie(Constants.NameCookieInitialsCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Symbol.ToString());
                string vIdCurrency = GetCookie(Constants.NameCookieIdCurrencySelected, Startup._vDataMenu.Currencies.FirstOrDefault().Id.ToString());
                Guid vIdCity = Startup._vIdCitySelected;

                vRankingTour = await _vTourService.GetToursByRanking(vIdLanguageCookie);
                ViewData["ListRankingTour"] = vRankingTour;

                if (Startup._vDataMenu.Cities != null || Startup._vDataMenu.Cities.Count() > 0) {
                    vModel = Startup._vDataMenu.Cities.Where(i => i.Id == vIdCity).FirstOrDefault();
                }
                valPage = Startup._vPageNumberTours;
                string[] vCategories = Startup._vDataMenu.Categories.Select(k => k.Id.ToString()).ToArray();
                SetFilter(vIdCity, Guid.Parse(vIdLanguageCookie), 0, int.MaxValue, 0,
                    int.MaxValue, 0, int.MaxValue, Guid.Parse(vIdCurrency), vCategories, valPage);

                TourResponseViewModel vModelResponse = await ListInfoCity();
                vModel.Tours = vModelResponse.Tours.OrderBy(i => i.Score).ToList();
                vModel.OrdersTours = OrderingTourViewModel.GetList(vInitialsDefaultLanguage);
                double vRows = 1;
                if (vModel.Tours != null && vModel.Tours.Count > 0) {
                    vRows = Math.Ceiling(Convert.ToDouble(vModelResponse.TotalRows) / Convert.ToDouble(_vRowsPerPage));
                }
                CreateViewDataElementInfo(vCurrencyInitialsDefault, vIdCurrency, vInitialsDefaultLanguage, valPage, vRows, vModel.OrdersTours.FirstOrDefault().Name);
                Startup._vDataCityTours = vModel;
                return Json(new { content= "1" });
            } catch (Exception) {
                return Json(new { content = "2" });
            }
        }

        #endregion

        #region Common
        private void CreateViewDataElementInfo(string valCurrencyInitialsDefault, string valIdCurrency, string valInitialsDefaultLanguage,
           int valPage, double valRows, string valDefaultTextOrderTours) {
            ViewData["CurrencySymbolDefault"] = valCurrencyInitialsDefault;
            ViewData["IdCurrencyDefault"] = valIdCurrency;
            ViewData["LanguageInitialDefault"] = valInitialsDefaultLanguage;
            ViewData["PageNumber"] = valPage;
            ViewData["RowsPerPage"] = valRows;
            ViewData["DefaultTextOrderTours"] = valDefaultTextOrderTours;
        }

        private string GetCookie(string valNameCookie, string valValueDefault) {
            string vResult = valValueDefault;
            string vValueCookie = Request.Cookies[valNameCookie];
            if (!string.IsNullOrEmpty(vValueCookie)) {
                vResult = vValueCookie;
            }
            Response.Cookies.Append(valNameCookie, vResult, new CookieOptions() { Expires = DateTime.Now.AddHours(24), IsEssential = true });
            return vResult;
        }

        private void SetFilter(Guid valIdCity, Guid valIdLanguageCookie, int valMinDuration,
            int valMaxDuration, int valMinScore, int valMaxScore, int valMinPrice, int valMaxPrice, Guid valIdCurrency,
            string[] valCategories, int valPage) {
            Startup._vFilterTour.IdCity = valIdCity;
            Startup._vFilterTour.IdLanguage = valIdLanguageCookie;
            Startup._vFilterTour.MinDuration = valMinDuration;
            Startup._vFilterTour.MaxDuration = valMaxDuration;
            Startup._vFilterTour.MinScore = valMinScore;
            Startup._vFilterTour.MaxScore = valMaxScore;
            Startup._vFilterTour.MinPrice = valMinPrice;
            Startup._vFilterTour.MaxPrice = valMaxPrice;
            Startup._vFilterTour.IdCurrency = valIdCurrency;
            Startup._vFilterTour.Categories = valCategories;
            Startup._vFilterTour.RowsPerPage = _vRowsPerPage;
            Startup._vFilterTour.PageNumber = valPage;
        }
        private async Task<TourResponseViewModel> ListInfoCity() {
            TourResponseViewModel vResult = new TourResponseViewModel();
            try {

                TourResponseViewModel vDataCity = await _vCityService.GetTours(Startup._vFilterTour);

                if (vDataCity != null && vDataCity.Tours != null && vDataCity.Tours.Count() > 0) {
                    vResult = vDataCity;
                } else {
                    vResult = new TourResponseViewModel();
                }
            } catch (Exception) {
                vResult = new TourResponseViewModel();
            }

            return vResult;
        }

        private void OrderTours(ref CityViewModel valModel, int valOrderingTourCriteria) {
            switch (valOrderingTourCriteria) {
                case 1:
                    valModel.Tours = valModel.Tours.OrderByDescending(i => i.AverageRanking).ToList();
                    break;
                case 2:
                    valModel.Tours = valModel.Tours.OrderBy(i => i.AveragePrice).ToList();
                    break;
                case 3:
                    valModel.Tours = valModel.Tours.OrderByDescending(i => i.AveragePrice).ToList();
                    break;
                case 4:
                    valModel.Tours = valModel.Tours.OrderBy(i => i.Name).ToList();
                    break;
                case 5:
                    valModel.Tours = valModel.Tours.OrderByDescending(i => i.DateCreate).ToList();
                    break;
            }

        }

        private int GetStartElement(string[] valArray) {
            int vResult = 0;
            if (valArray == null || valArray.Length == 0)
                return vResult;

            string vFirstPosition = valArray[0];
            string[] vSplitArray = vFirstPosition.Split('-');
            vResult = Convert.ToInt32(vSplitArray[0]);
            return vResult;
        }

        private int GetLastElement(string[] valArray) {
            int vResult = int.MaxValue;
            if (valArray == null || valArray.Length == 0)
                return vResult;

            string vLastPosition = valArray[valArray.Length - 1];
            string[] vSplitArray = vLastPosition.Split('-');
            vResult = Convert.ToInt32(vSplitArray[1]);
            return vResult;
        }
        #endregion

    }
}