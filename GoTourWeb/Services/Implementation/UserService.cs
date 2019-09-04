using GoTourWeb.Helpers;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Implementation {
	public class UserService : IUserService {
		private ApiClient _vApiClient;

		public UserService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
		}

		public async Task<UserViewModel> Login(string vUsername, string vPassword) {
            UserViewModel vResult = new UserViewModel();
			try {
                _vApiClient.AddParameter("username", vUsername);
                _vApiClient.AddParameter("password", vPassword);
				var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("account");
				if (vResponse == null || vResponse.Status != System.Net.HttpStatusCode.OK) {
					return null;
				}

                ClientResponseViewModel vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    IEnumerable<UserViewModel> vListUsers = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(vClientResponse.Result);
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

        public async Task<ClientResponseViewModel> Register(UserViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("user",valUser);
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


        
    }
}
