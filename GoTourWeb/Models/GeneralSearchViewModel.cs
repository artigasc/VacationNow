using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class GeneralSearchViewModel {
        
        public Guid IdLanguage { get; set; }
        public string SearchText { get; set; }
 
    }

    public class GeneralResultViewModel {
        public Guid IdCityTour { get; set; }
        public Guid IdCity { get; set; }
        public string NameCityTour { get; set; }
        public string IconCityTour { get; set; }
        public int Ordering { get; set; }
        public string Url {
            get {
                if (Ordering == 1) {
                    return "/Home/SelectSearchCity";
                } else if (Ordering == 2) {
                    return "/Home/SelectSearchTour";
                }
                return "/Home/SelectSearchCity";
            }
        }

    }

    public class GeneralConsultViewModel {
        public Guid IdcityTour { get; set; }
        public Guid IdCity { get; set; }
    }


}
