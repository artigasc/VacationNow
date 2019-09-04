using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class CompanyViewModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }


        public string Phone { get; set; }


        public string Movil { get; set; }

        [Required]
        public string TypeNumberDocument { get; set; }

        [Required]
        public string NumberDocument { get; set; }

        public string Web { get; set; }

        public string UrlPhoto { get; set; }

        public string Address { get; set; }

        public Guid IdDistrict { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public bool IsEnable { get; set; }

        public int State { get; set; }

        public string UserCreate { get; set; }

        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}
