using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class LanguageService : ILanguageService {
        private ApiClient _vApiClient;
        public LanguageService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }
        public async Task<List<Language>> GetLanguage() {
            List<Language> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("Cities/SelectLanguageAll");
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<Language>>(vClientResponse.Result);
                }

            } catch (Exception vEx) {
                var Message = vEx.Message;
                vResult = null;
            }
            return vResult;

        }
    }
}
