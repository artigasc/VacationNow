using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class CompanyViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TypeNumberDocument { get; set; }
        public string Phone { get; set; }
        public string Movil { get; set; }
        public string NumberDocument { get; set; }
        public string UrlPhoto { get; set; }
        public FileViewModel Photo { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string Web { get; set; }
        public int State { get; set; }
        public string UserCreate{ get; set; }
        public DateTime DateCreate { get; set; }
        
    }
}
