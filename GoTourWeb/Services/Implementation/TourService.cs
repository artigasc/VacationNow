using System;
using GoTourWeb.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using System.Net;
using Newtonsoft.Json;

namespace GoTourWeb.Services.Implementation {
    public class TourService : ITourService {
        private ApiClient _vApiClient;

        public TourService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<List<TourViewModel>> GetToursByRanking(string valIdLanguage) {
            List<TourViewModel> vResult = null;

            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("tour",valIdLanguage);
                if (vResponse == null) {
                    return vResult;
                }
                var vInfoResult = (ClientResponseViewModel)vResponse.Content;
                if (vInfoResult.Code == (int)HttpStatusCode.OK) {
                    vResult = JsonConvert.DeserializeObject<List<TourViewModel>>(vInfoResult.Result);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }

            return vResult;
        }
    }
}
