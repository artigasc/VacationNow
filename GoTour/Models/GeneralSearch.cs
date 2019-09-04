using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    //Search
    public class GeneralSearch {
        //ID del Lenguaje
        public Guid IdLanguage { get; set; }
        //Texto de busqueda
        public string SearchText { get; set; }

    }

    //Result
    public class GeneralResult {
        public Guid IdCityTour { get; set; }
        public Guid IdCity { get; set; }
        public string NameCityTour { get; set; }
        public string IconCityTour { get; set; }
        public int Ordering { get; set; }
    }

    public class ListCityLanguage {
        public List<City> ListCity { get; set; }
    }

}