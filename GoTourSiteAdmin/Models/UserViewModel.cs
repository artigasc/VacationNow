using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Models {
    public class UserViewModel {
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
        //public static List<UserViewModel> ListUser {
        //    get {
        //        return new List<UserViewModel>(){
        //            new UserViewModel() {

        //                Id= new Guid("c195d613-882b-49c7-a16e-6aa9278e04ab"),
        //                UserName = "User1",
        //                Password = "9999999999999",
        //                FirstName = "Juan",
        //                FirstLastName = "Vásquez",
        //                Email = "user@user.com",
        //                Phone = "12345678",
        //                UrlPhoto="/images/user1.jpg",
        //                State = 2
        //            },
        //            new UserViewModel() {
        //                Id= new Guid("ad526875-e7b9-468c-ae3e-d05dcb07c0d9"),
        //                UserName = "User2",
        //                Password = "222222222222",
        //                FirstName = "Mirella",
        //                FirstLastName = "Flores",
        //                Email = "user2@user.com",
        //                Phone = "98765431",
        //                UrlPhoto="/images/user2.jpg",
        //                State = 1

        //            }               
        //        };
        //    }
        //}

        
    }


}