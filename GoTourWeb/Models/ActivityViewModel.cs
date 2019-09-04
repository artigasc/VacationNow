using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class ActivityViewModel {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string StartPoint { get; set; }

        public string Itinerary { get; set; }

        public string Includes { get; set; }

        public string Note { get; set; }

        public float Mount { get; set; }

        public int FreeCancelation { get; set; }

        public int Duration { get; set; }

        public int MinimumPeople { get; set; }

        public int SellTimeAdvance { get; set; }

        public DateTime DateStart { get; set; }

        public int Score { get; set; }

        public string NameCompany { get; set; }

        public string UrlPhoto { get; set; }

        public Guid IdTour { get; set; }

        public Guid IdCompany { get; set; }

        public Guid IdCurrency { get; set; }

        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }

        public int Ranking { get; set; }
    }

    public class ActivityCompanyViewModel {
        public ActivityViewModel Activity { get; set; }
        public CompanyViewModel Company { get; set; }
        
    }


}
