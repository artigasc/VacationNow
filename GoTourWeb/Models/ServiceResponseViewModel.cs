using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class ServiceResponseViewModel {
        public dynamic Content { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        public string StatusDescription { get; set; }
    }

    public class PaymentResponseViewModel {
        public PaymentResponseViewModel() {
            Payments = new List<PaymentViewModel>();
        }
        public long TotalRows { get; set; }
        public List<PaymentViewModel> Payments { get; set; }
    }

    public class TourResponseViewModel {
        public TourResponseViewModel() {
            Tours = new List<TourViewModel>();
        }
        public long TotalRows { get; set; }
        public List<TourViewModel> Tours { get; set; }
    }

    public class ActivityResponseViewModel {
        public ActivityResponseViewModel() {
            Activities = new List<ActivityViewModel>();
        }
        public List<ActivityViewModel> Activities { get; set; }
        public long TotalRows { get; set; }
    }
}
