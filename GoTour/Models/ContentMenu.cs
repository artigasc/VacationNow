using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class ContentMenu {
        
        public List<City> Cities { get; set; }

        public List<Category> Categories { get; set; }

        public List<Language> Languages { get; set; }

        public List<Currency> Currencies { get; set; }

    }
}