using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace GoTour.Models {
    public class ServiceResponse {
        public dynamic Content { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        public string StatusDescription { get; set; }
    }
    public class PaymentResponse {
        public long TotalRows { get; set; }
        public List<Payment> Payments { get; set; }
    }

    public class TourResponse {
        public long TotalRows { get; set; }
        public List<Tour> Tours { get; set; }
    }

    public class ActivityResponse {
        public List<Activity> Activities { get; set; }
        public long TotalRows { get; set; }
    }
}