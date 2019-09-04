using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GoTour.Helper {
    public static class GatewayUtils {

        public static string ProccessingResponseGateway(string valTextResponse) {
            string vResult = string.Empty;
            List<GatewayErrorMessage> vResponse = null;
            string vNameFile = @"\Files\GatewayErrors.json";
            string vJsonFile = FileHelper.ReadFile(vNameFile);
            if (!string.IsNullOrEmpty(vJsonFile)) {
                vResponse = JsonConvert.DeserializeObject<List<GatewayErrorMessage>>(vJsonFile);
                if (vResponse != null && vResponse.Count > 0) {
                    vResult = vResponse.Where(i => i.Key == valTextResponse).FirstOrDefault().Value;
                }
            }
            return vResult;
        }

    }
}