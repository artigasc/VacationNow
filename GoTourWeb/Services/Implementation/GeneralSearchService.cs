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
    public class GeneralSearchService : IGeneralSearchService {

        private ApiClient _vApiClient;
        public GeneralSearchService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<List<GeneralResultViewModel>> SearchPrincipal(GeneralSearchViewModel valSearchText) {
            List<GeneralResultViewModel> vResult = null;

            var vResponse = await _vApiClient.ExecuteGetWithJson<ClientResponseViewModel>("GeneralSearch", valSearchText);

            if (vResponse == null) {
                return vResult;
            }
            if (vResponse.Status == HttpStatusCode.OK) {
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                vResult = JsonConvert.DeserializeObject<List<GeneralResultViewModel>>(vClientResponse.Result);

            }

            return vResult;
        }
    }
}
