using GoTour.Helper;
using GoTour.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GoTour.Helper {
    public class GatewayService {
        private ApiClient _vApiClient;

        public GatewayService() {
            _vApiClient = new ApiClient(Constants.UrlGateway);
        }

        public async Task<ResponseGateway> Create(PaymentGateway valModel) {
            ResponseGateway vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ResponseGateway>(valModel);
                if (vResponse == null) {
                    return vResult;
                }
                if (vResponse.Status == HttpStatusCode.OK)
                    vResult = (ResponseGateway)vResponse.Content;
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public async Task<ResponseGatewayRefund> Refund(GatewayRefund valModel) {
            ResponseGatewayRefund vResult = null;
            try {
                var vResponse = await _vApiClient.ExecutePost<ResponseGatewayRefund>(valModel);
                if (vResponse == null) {
                    return vResult;
                }
                if (vResponse.Status == HttpStatusCode.OK)
                    vResult = (ResponseGatewayRefund)vResponse.Content;
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

    }
}
