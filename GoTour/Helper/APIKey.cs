using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoTour.Helper {
    public class APIKey : DelegatingHandler {
        private const string api_check = "123";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRM, CancellationToken cancellationT) {
            HttpResponseMessage vResult = null;
            bool validkey = false;

            IEnumerable<string> requestheader;

            bool checkApiExists = httpRM.Headers.TryGetValues("APIKey", out requestheader);

            if (checkApiExists) {
                if (requestheader.FirstOrDefault().Equals(api_check)) {
                    validkey = true;
                }
            }

            if (!validkey) {
                return httpRM.CreateResponse(HttpStatusCode.Forbidden, "APIKEY invalido o vacio");
            }

            vResult = await base.SendAsync(httpRM,cancellationT);
            return vResult;
        }

        

    }
}