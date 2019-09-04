using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;


namespace GoTour.DataAccess.Implementation {
    public class MenuData : IMenuData {
        

        public string SelectContentMenu(string valIdLanguage) {
            string vResult = string.Empty;
            ICityData vCityData = new CityData();
            ICategoryData vCategoryData = new CategoryData();
            ILanguageData vLanguageData = new LanguageData();
            ICurrencyData vCurrencyData = new CurrencyData();
            ContentMenu vContenMenu = new ContentMenu();
            try {
                string vLanguage = vLanguageData.SelectAll();
                List<Language> vDeserializeLaguages = JsonConvert.DeserializeObject<List<Language>>(vLanguage);
                string vCities = vCityData.SelectByLanguage(valIdLanguage);
                List<City> vDeserializeCities = JsonConvert.DeserializeObject<List<City>>(vCities);
                string vTours = vCategoryData.SelectAllByLanguage(valIdLanguage);
                List<Category> vDeserializeCategory = JsonConvert.DeserializeObject<List<Category>>(vTours);
                string vCurrency = vCurrencyData.SelectAll();
                List<Currency> vDeserializeCurrency = JsonConvert.DeserializeObject<List<Currency>>(vCurrency);
                vContenMenu.Cities = vDeserializeCities;
                vContenMenu.Categories = vDeserializeCategory;
                vContenMenu.Languages = vDeserializeLaguages;
                vContenMenu.Currencies = vDeserializeCurrency;
                if (vContenMenu != null) {
                    vResult = JsonConvert.SerializeObject(vContenMenu, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = string.Empty;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }
        
    }
}