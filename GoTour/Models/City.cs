using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class City {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slogan { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }

        public int Temperature { get; set; }

        public int Altitude { get; set; }

        public int Population { get; set; }

        public string FarmingProduction { get; set; }

        public string UrlPhoto { get; set; }

        public string DescriptionDistricts { get; set; }
        [Required]
        public int Position { get; set; }

        public Guid IdLanguage { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }
        public FileCity Photo { get; set; }
        public Guid IdCityLanguage { get; set; }
    }
    public class FileCity {
        public string NameFile { get; set; }
        public byte[] FileData { get; set; }
        public long Size { get; set; }
    }

}