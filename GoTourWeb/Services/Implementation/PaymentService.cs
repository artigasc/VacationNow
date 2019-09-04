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
	public class PaymentService : IPaymentService {
		private ApiClient _vApiClient;

		public PaymentService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
		}

		

        public async Task<ClientResponseViewModel> Register(PaymentViewModel valModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("payment", valModel);
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

        public async Task<PaymentViewModel> SearchPay(PaymentSearchViewModel valPaymentSearch) {
            PaymentViewModel vResult = null;
            try {
                _vApiClient.AddParameter("IdPayment", valPaymentSearch.IdPayment);
                _vApiClient.AddParameter("IdLanguage", valPaymentSearch.IdLanguage);
                var vResponse = await _vApiClient.ExecuteGetWithJson<ClientResponseViewModel>("payment", valPaymentSearch);
                if (vResponse == null) {
                    return vResult;
                }
                if (vResponse.Status == HttpStatusCode.OK) {
                    var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                    vResult = JsonConvert.DeserializeObject<PaymentViewModel>(vClientResponse.Result);
                   
                } 

               
                
            } catch (Exception ex) {
                string vMessage = ex.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<PaymentResponseViewModel> SearchPayByUser(PaymentSearchUserViewModel valPaymentSearch) {
            PaymentResponseViewModel vResult = null;
            try {
              
                var vResponse = await _vApiClient.ExecuteGetWithJson<ClientResponseViewModel>("payment/ByUser", valPaymentSearch);
                if (vResponse == null) {
                    return vResult;
                }
                if (vResponse.Status == HttpStatusCode.OK) {
                    var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                    vResult = JsonConvert.DeserializeObject<PaymentResponseViewModel>(vClientResponse.Result);

                }
            } catch (Exception ex) {
                string vMessage = ex.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ClientResponseViewModel> UpdateRanking(PaymentViewModel valModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("payment/updateranking", valModel);
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

        public async Task<ClientResponseViewModel> CancelAndRefund(PaymentViewModel valModel) {
            ClientResponseViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ClientResponseViewModel>("payment/CancelAndRefund", valModel);
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
