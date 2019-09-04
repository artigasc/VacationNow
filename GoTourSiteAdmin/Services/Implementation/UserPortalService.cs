using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class UserPortalService : IUserPortalService {
        private ApiClient _vApiClient;
        public UserPortalService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }
        public async Task<ClientResponseViewModel> Register(UserPortalViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/UserPortal/", valUser);
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
        public async Task<List<UserPortalViewModelResponse>> GetUser() {
            List<UserPortalViewModelResponse> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/UserPortal/SelectUserPortalAll");
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<UserPortalViewModelResponse>>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> ChangeState(UserPortalViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/UserPortal/UpdateStateUserPortal", valUser);
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

        public async Task<UserPortalViewModelResponse> GetUserById(string Id) {
            UserPortalViewModelResponse vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/UserPortal/", Id);
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<UserPortalViewModelResponse>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;

        }

        public async Task<ClientResponseViewModel> Update(UserPortalViewModel valUser) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/UserPortal/Put", valUser);
                if (vResponse == null) {
                    vResult = null;
                } 
                vResult = (ClientResponseViewModel)vResponse.Content;
                
            }
            catch(Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;

        }
    }
}
