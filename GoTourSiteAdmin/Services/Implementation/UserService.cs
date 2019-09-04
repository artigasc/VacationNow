using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class UserService : IUserService {
        private ApiClient _vApiClient;
        public UserService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<List<UserViewModel>> GetUser() {
            List<UserViewModel> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/User/SelectUserAll");
                if(vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<UserViewModel>>(vClientResponse.Result);
                }
            }
            catch(Exception vEx) {
                var Message = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<UserViewModel> GetUserById(Guid Id) {
            UserViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/User", Id.ToString());
                if (vResponse == null) {
                    vResult = null;
                }
                var vClienteResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClienteResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<UserViewModel>(vClienteResponse.Result);
                }
                return vResult;
            } catch (Exception vEx) {
                var vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> Update(UserViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/User/UpdateUserWeb", valUser);
                if(vResponse == null) {
                    return vResult;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;
            } catch (Exception vEx) {
                string Message = vEx.Message;
                return vResult;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> UpdateState(UserViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/User/Put", valUser);
                if(vResponse == null) {
                    return vResult;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;

            } catch (Exception vEx) {
                string Message = vEx.Message;
                return vResult;
            }
            return vResult;
        }

    }
}
