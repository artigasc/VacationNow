using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class ActivitySearch {
        public Guid IdTour { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public DateTime DateStart { get; set; }
        public int MinimumPeople { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
    }
    public class ActivityCompanySearch {
        public Guid IdActivity { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public Guid IdCompany { get; set; }
    }
}