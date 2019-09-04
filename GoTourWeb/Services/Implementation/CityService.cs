using GoTourWeb.Helpers;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Implementation {
    public class CityService : ICityService {
        private ApiClient _vApiClient;

        public CityService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<TourResponseViewModel> GetTours(FilterToursViewModel valFilter) {
            TourResponseViewModel vResult = null;
            try {
 
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("tour", valFilter);
                if (vResponse == null) {
                    return vResult;
                }
                var vInfoResponse = (ClientResponseViewModel)vResponse.Content;
                if (vInfoResponse.Code == (int)HttpStatusCode.OK) {
                    vResult= JsonConvert.DeserializeObject<TourResponseViewModel>(vInfoResponse.Result);
                }
            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }
    }
}
