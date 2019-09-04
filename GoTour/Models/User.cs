using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class User {
        public Guid Id { get; set; }

        public string TypeNumberDocument { get; set; }

        public string Nacionality { get; set; }

        public string NumberDocument { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string Email { get; set; }

        public string UrlPhoto { get; set; }

        public DateTime BirthDate { get; set; }

        public Guid IdMasterCity { get; set; }
        public string Phone { get; set; }

        public Guid IdCompany { get; set; }

        public int State { get; set; }

        public string UserCreate { get; set; }

        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
    

    



}