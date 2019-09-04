using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class CityViewModel {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Temperature { get; set; }
        public int Altitude { get; set; }
        public int Population { get; set; }
        public string UrlPhoto { get; set; }
        public int Position { get; set; }
        public int State { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public FileViewModel Photo { get; set; }
        public Guid IdLanguage { get; set; }
        public string Slogan { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string FarmingProduction { get; set; }
        public string DescriptionDistricts { get; set; }

        //public List<CityLanguageViewModel> CityLanguage { get; set; }
    }
    public class CityLanguageViewModel {
        public List<CityViewModel> ListCity { get; set; }

    }
  
}
