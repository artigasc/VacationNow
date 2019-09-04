using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class ServiceResponseViewModel {
        public dynamic Content { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        public string StatusDescription { get; set; }
    }
    public class ClientResponseViewModel {
        public dynamic Result { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
