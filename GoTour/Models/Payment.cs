using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoTour.Models {
    public class Payment {
        public Guid Id { get; set; }
        public Guid IdTransaction { get; set; }
        public string GatewayJsonData { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdActivity { get; set; }
        public float Mount { get; set; }
        public Guid IdCurrency { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TypeNumberDocument { get; set; }
        public string NumberDocument { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Persons { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Ranking { get; set; }
        public int Duration { get; set; }
        public string Symbol { get; set; }
        public string NameCompany { get; set; }
        public int MinimumPeople { get; set; }
        public int SellTimeAdvance { get; set; }
        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }

        public float TotalMount { get; set; }

        public string LanguageInitials { get; set; }
        public string EmailCompany1 { get; set; }
        public string EmailCompany2 { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SecurityCode { get; set; }
        public string PayMethod { get; set; }
        public DateTime DateReserve { get; set; }
        public int FreeCancelation { get; set; }
        public Guid IdLanguage { get;  set; }
        public string StateRefund { get; set; }

        public bool IsRankingForUser { get; set; }
        public string GetNameStateRefund(string valInitialsLanguage, string valStateRefund) {
            string vResult = string.Empty;
            if (valStateRefund == "PENDING") {
                switch (valInitialsLanguage) {
                    case "EN":
                        vResult = "PENDING";
                        break;
                    case "ES":
                    case "PO":
                        vResult = "PENDIENTE";
                        break;
                }
            }else if (valStateRefund == "APPROVED") {
                switch (valInitialsLanguage) {
                    case "EN":
                        vResult = "APPROVED";
                        break;
                    case "ES":
                        vResult = "APROBADO";
                        break;
                    case "PO":
                        vResult = "APROVADO";
                        break;
                }
            } else if (valStateRefund == "DECLINED") {
                switch (valInitialsLanguage) {
                    case "EN":
                        vResult = "DECLINED";
                        break;
                    case "ES":
                        vResult = "DECLINADO";
                        break;
                    case "PO":
                        vResult = "DECLINADO";
                        break;
                }
            }


            return vResult;
        }
    }
}