using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class FilterToursViewModel {
        public Guid IdCategory { get; set; }
        public Guid IdCity { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public string[] Categories { get; set; }
        public string[] Prices { get; set; }
        public string[] Durations { get; set; }
        public string[] Ranking { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
        public Guid IdTour { get; set; }
        public string Name { get; set; }
    }

    public class FilterActivityViewModel {
        public Guid IdTour { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public DateTime DateStart { get; set; }
        public string DateStartString { get; set; }
        public int MinimumPeople { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
    }

    public class FilterActivityCompanyViewModel {
        public Guid IdActivity { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public Guid IdCompany { get; set; }
    }

    public class PaymentSearchViewModel {
        public Guid IdPayment { get; set; }
        public Guid IdLanguage { get; set; }
    }

    public class PaymentSearchUserViewModel {
        public Guid IdUser { get; set; }
        public Guid IdLanguage { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

    }
}