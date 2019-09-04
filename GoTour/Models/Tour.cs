using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class Tour {
        [Required]
        public Guid Id { get; set; }

        public Guid IdCity { get; set; }

        public Guid IdCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

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

    }
}