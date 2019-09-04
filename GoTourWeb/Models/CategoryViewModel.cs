using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class CategoryViewModel {
        public Guid Id { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdLanguage { get; set; }
        public string Name { get; set; }
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
