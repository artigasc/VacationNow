using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class ViewLanguajeViewModel {
 
        public List<ViewTextViewModel> ViewText { get; set; }
        
    }

    public class ViewTextViewModel {
        public string NameView { get; set; }
        public List<Dictionary<string, List<Dictionary<string, string>>>> Elements { get; set; }
        //public dynamic Elements { get; set; }
    }

}
