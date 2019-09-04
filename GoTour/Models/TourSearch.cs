using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class TourSearch {
        public Guid IdCity { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdCurrency { get; set; }
        public string[] Categories { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
    }
}