using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Helpers {
	public class Constants {

        public const string UrlBase = "https://gotourapi.azurewebsites.net/api";
        //public const string UrlBase = "http://localhost:51394/api";
        public const string LanguageDefault = "E3C67CB6-2179-4C6F-8425-ECBA11BDA247";
        public const string InitialsLanguageDefault = "ES";
        public const string CityDefault = "7EFB60F8-D80C-4CF8-A6ED-FC0E2DCA365D";
        public const int LongitudeDescriptionTours = 250;
        public const string NameCookieIdLanguage = "LanguageGoTour";
        public const string NameCookieInitialsLanguage = "LanguageInitialsGoTour";
        public const string NameCookieIdCitySelected = "IdCitySelected";
        public const string NameCookieIdCurrencySelected = "CurrencyGoTour";
        public const string NameCookieInitialsCurrencySelected = "CurrencyInitialsGoTour";
        public const string CurrencyDefault = "2AC154DA-120F-4BBA-B4E2-DB728AC89DA0";
        public const int MinNumInputSearch = 3;

        public const string TokenAccesController = "AD3E2BC7-66CB-44DD-A9EE-217338B5EBF4";
        public const int TotalStars = 5;
    }


}
