using GoTourWeb.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class TourViewModel {
        [Required]
        public Guid Id { get; set; }

        public Guid IdCity { get; set; }

        public Guid IdCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DescriptionShort {
            get {
                try {
                    if (Description.Length > Constants.LongitudeDescriptionTours) {
                        return Description.Substring(0, Constants.LongitudeDescriptionTours) + "...";
                    }
                    return Description;
                } catch (Exception) {
                    return Description;
                }
            }
        }

        public string UrlPhoto { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int Ranking { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }

        public Guid IdCompany { get; set; }

        public double AveragePrice { get; set; }

        public int AverageRanking { get; set; }

        public List<ActivityViewModel> Activities { get; set; }

        
    }
}
