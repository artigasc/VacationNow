using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class CurrencyViewModel {
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public  string Country { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
        public int State { get; set; }
    }
}
