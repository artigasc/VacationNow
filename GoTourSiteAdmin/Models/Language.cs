using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class Language {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public int State { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
