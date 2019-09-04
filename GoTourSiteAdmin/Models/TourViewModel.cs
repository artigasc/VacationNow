using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class TourViewModel {
        public Guid Id { get; set; }
        public Guid IdCity { get; set; }
        public Guid IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; }
        public int Score { get; set; }
        public int Ranking { get; set; }
        public int State { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public Guid IdCompany { get; set; }
        public double AveragePrice { get; set; }
        public int AverageRanking { get; set; }
    }
}
