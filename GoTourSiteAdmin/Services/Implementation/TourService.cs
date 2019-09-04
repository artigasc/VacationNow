using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {

    public class TourService : ITourService {
        private ApiClient _vApiClient;
        public TourService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }
        public async Task<ClientResponseViewModel> AddTour(TourViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Tour/", vModel);
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

        public async Task<ClientResponseViewModel> ChangeState(TourViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Tour/", vModel);
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

        public async Task<List<TourViewModel>> GetTour() {
            List<TourViewModel> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/Tour/");//change
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<TourViewModel>>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<List<TourViewModel>> GetTourById(Guid vId) {
            List<TourViewModel> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/Tour/");//change
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<TourViewModel>>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> UpdateTour(TourViewModel vModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Tour/Put", vModel);
                if (vResponse == null) {
                    vResult = null;
                }
                vResult = (ClientResponseViewModel)vResponse.Content;

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }
    }
}
