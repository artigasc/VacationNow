using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class UserPortalViewModel {
        public Guid Id {get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public string UrlPhoto { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
        public string BackMail { get; set; }
        public string UserCreate { get; set; }
        public int State { get; set; }
        public DateTime DateCreate{ get; set; }
        public string CompanyName { get; set; }
        public Guid IdCompany { get; set; }
        public string UserUpdate { get; set; }

        public FileViewModel Photo { get; set; }
        public static List<UserPortalViewModel> ListUser {
            get {
                return new List<UserPortalViewModel>(){
                    new UserPortalViewModel() {

                        Id= new Guid("c195d613-882b-49c7-a16e-6aa9278e04ab"),
                        UserName = "Admin123",
                        Password = "123",
                        FirstName = "Enrique",
                        FirstLastName = "",
                        SecondLastName = "M.",
                        Email = "admin@admin.com",
                        UrlPhoto ="/images/user1.jpg",
                        Phone = "12345678",
                        State = 1,
                        DateCreate= new DateTime(12,11,16),
                        CompanyName = "GoTour",
                        IdCompany = new Guid("0ddf988a-c360-47f3-88bd-2fc5ca16a5fb")
                        
                    },
                    new UserPortalViewModel() {
                        Id= new Guid("3360b88b-898d-4a55-be20-d7a7e3698471"),
                        UserName = "AdminGlobal",
                        Password = "321",
                        FirstName = "Manuel",
                        FirstLastName = "Salas",
                        SecondLastName = "J.",
                        Email = "globaladmin@admin.com",
                        UrlPhoto ="/images/user2.jpg",
                        Phone = "098765436",
                        State = 2,
                        DateCreate= new DateTime(12,11,16),
                        CompanyName = "Viajes Perú",
                        IdCompany = new Guid("8faa666b-d667-4c6d-9bc8-dbeb90625931")

                    }
                };
            }
        }


    }
    public class FileViewModel {
        public string NameFile { get; set; }
        public byte[] FileData { get; set; }
        public long Size{ get; set; }
    }
    public class UserPortalViewModelResponse {
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
        public string CompanyName { get; set; }
        public int State { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Backmail { get; set; }

    }
}
