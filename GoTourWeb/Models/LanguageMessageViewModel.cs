using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class LanguageMessageViewModel {
        public List<ViewTextMessageModel> Messages { get; set; }
    }

    public class ViewTextMessageModel {
        public string NameView { get; set; }
        
        public List<ViewContentModel> Content { get; set; }
    }
    public class ViewContentModel {
        public string ValueResponse { get; set; }

        public List<LanguageContentViewModel> LanguageContent { get; set; }
    }
    public class LanguageContentViewModel {
        public string Language { get; set; }

        public List<Dictionary<string,string>> ListText { get; set; }
    }

    
}
