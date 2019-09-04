using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    
    public class UserPortalAdmin {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public string UrlPhoto { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public Guid IdCompany { get; set; }
        public int State { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Backmail { get; set; }
        public string CompanyName { get; set; }
        public UserPhoto Photo { get; set; }


    }

    public class UserPhoto {
        public string NameFile { get; set; }
        public byte[] FileData { get; set; }
        public long Size { get; set; }
    }








}