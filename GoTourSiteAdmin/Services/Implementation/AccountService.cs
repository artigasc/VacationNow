using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class AccountService : IAccountService {

        private ApiClient _vApiClient;
        public AccountService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }
        public async Task<UserPortalViewModel> Login(string vUsername, string vPassword) {
            UserPortalViewModel vResult = new UserPortalViewModel();
            try {
                _vApiClient.AddParameter("UserName", vUsername);
                _vApiClient.AddParameter("Password", vPassword);
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("UserPortal/ValidateUserPortal");
                if (vResponse == null || vResponse.Status != System.Net.HttpStatusCode.OK) {
                    return null;
                }

                ClientResponseViewModel vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    IEnumerable<UserPortalViewModel> vListUsers = JsonConvert.DeserializeObject<IEnumerable<UserPortalViewModel>>(vClientResponse.Result);
                    if (vListUsers != null && vListUsers.Count() > 0) {
                        vResult = vListUsers.FirstOrDefault();
                    } else {
                        vResult = null;
                    }
                }

            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }

        public Task<UserPortalViewModel> LoginAsync(string vUsername, string vPassword) {
            throw new NotImplementedException();
        }
    }
}
