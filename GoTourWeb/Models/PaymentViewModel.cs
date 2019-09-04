using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class PaymentViewModel {
        public Guid Id { get; set; }
        public Guid IdActivity { get; set; }
        public Guid IdUser { get; set; }
        public string GatewayJsonData { get; set; }
        public Guid IdCurrency { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TypeDocument { get; set; }
        public string NumberDocument { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Persons { get; set; }
        public float TotalMount { get; set; }
        public float Mount { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SecurityCode { get; set; }
        public string IdTransaction { get; set; }
        public string CulquiJsonData { get; set; }
        public string TypeNumberDocument { get; set; }
        public string NameUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public int Ranking { get; set; }
        public int Duration { get; set; }
        public int MinimumPeople { get; set; }
        public int SellTimeAdvance { get; set; }
        public string NameCompany { get; set; }
        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }
       
        public string LanguageInitials { get; set; }
        public string EmailCompany1 { get; set; }
        public string EmailCompany2 { get; set; }
        public string PayMethod { get; set; }
        public DateTime DateReserve { get; set; }
        public int DayReserve { get; set; }
        public int MonthReserve { get; set; }
        public int YearReserve { get; set; }

        public int FreeCancelation { get; set; }
        public Guid IdLanguage { get; internal set; }
        public bool IsRankingForUser { get; set; }
    }
}
