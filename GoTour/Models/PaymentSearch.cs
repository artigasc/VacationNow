using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class PaymentSearch {
        public Guid IdPayment { get; set; }
        public Guid IdLanguage { get; set; }
       
    }

    public class PaymentUserSearch {
        public Guid IdUser { get; set; }
        public Guid IdLanguage { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

    }
}