using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class MenuViewModel {
        public List<CityViewModel> Cities { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
        public List<CurrencyViewModel> Currencies { get; set;  }
    }
}
