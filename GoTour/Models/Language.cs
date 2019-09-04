using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class Language {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Initials { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}