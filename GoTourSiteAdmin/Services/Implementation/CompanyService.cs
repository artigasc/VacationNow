using GoTourSiteAdmin.Helpers;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Implementation {
    public class CompanyService : ICompanyService {
        private ApiClient _vApiClient;
        public CompanyService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
        }

        public async Task<ClientResponseViewModel> Register(CompanyViewModel vCompany) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Company/", vCompany);//change
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

        public async Task<ClientResponseViewModel> ChangeState(CompanyViewModel vCompany) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Company/", vCompany);
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

        public async Task<ClientResponseViewModel> Update(CompanyViewModel vCompany) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("/Company/Put", vCompany);
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

        public async Task<List<CompanyViewModel>> GetCompany() {
            List<CompanyViewModel> vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/Companies");
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                    vResult = JsonConvert.DeserializeObject<List<CompanyViewModel>>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<CompanyViewModel> GetCompanyById(Guid valId) {
            CompanyViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("/Company/", valId.ToString());
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
    }
}
