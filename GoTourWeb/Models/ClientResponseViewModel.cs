using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class ClientResponseViewModel {
        public dynamic Result { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
