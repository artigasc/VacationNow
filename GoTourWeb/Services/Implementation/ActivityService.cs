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
    public class ActivityService : IActivityService {
        private ApiClient _vApiClient;

        public ActivityService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<ActivityResponseViewModel> GetActivities(FilterActivityViewModel valFilter) {
            ActivityResponseViewModel vResult = null;
            try {
 
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("activity", valFilter);
                if (vResponse == null) {
                    return vResult;
                }
                var vInfoResponse = (ClientResponseViewModel)vResponse.Content;
                if (vInfoResponse.Code == (int)HttpStatusCode.OK) {
                    vResult= JsonConvert.DeserializeObject<ActivityResponseViewModel>(vInfoResponse.Result);
                }
            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }

        public async Task<ActivityCompanyViewModel> GetActivityCompany(FilterActivityCompanyViewModel valFilter) {
            ActivityCompanyViewModel vResult = null;
            try {

                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("activitycompany", valFilter);
                if (vResponse == null) {
                    return vResult;
                }
                var vInfoResponse = (ClientResponseViewModel)vResponse.Content;
                if (vInfoResponse.Code == (int)HttpStatusCode.OK) {
                    vResult= JsonConvert.DeserializeObject<ActivityCompanyViewModel>(vInfoResponse.Result);
                   
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }
    }
}
