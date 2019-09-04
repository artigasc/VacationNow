using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class CityService : ICityService {

        private ApiClient _vApiClient;
        public CityService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

     

        public async Task<ClientResponseViewModel> AddCity(CityLanguageViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Cities", vModel);
                if (vResponse == null) {
                    return vResult;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> ChangeState(CityViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Cities", vModel);//change controller
                if (vResponse == null) {
                    return vResult;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> EditCity(CityLanguageViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Cities", vModel);//change controller
                if (vResponse == null) {
                    return vResult;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<List<CityViewModel>> GetCity() {
            List<CityViewModel> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("Cities/SelectCityAll");
                if(vResponse == null) {
                   return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if(vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<CityViewModel>>(vClientResponse.Result);
                }
              
            }
            catch(Exception vEx) {
                var Message = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<CityLanguageViewModel> GetCityById(Guid vId) {
            CityLanguageViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<CityLanguageViewModel>("Cities/SelectCityById");//Change name
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<CityLanguageViewModel>(vClientResponse.Result);
                }

            } catch (Exception vEx) {
                var Message = vEx.Message;
                vResult = null;
            }
            return vResult;
        }
    }
}
