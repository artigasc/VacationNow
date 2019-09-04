using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GoTourWeb.Models {
    public class CityViewModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slogan { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int Temperature { get; set; }

        public int Altitude { get; set; }

        public int Population { get; set; }

        public string FarmingProduction { get; set; }

        public string UrlPhoto { get; set; }

        public string DescriptionDistricts { get; set; }
        [Required]
        public int Position { get; set; }


        public Guid IdLanguage { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string UserCreate { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserUpdate { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<TourViewModel> Tours { get; set; }

        public List<OrderViewModel> OrdersTours { get; set; }
    }

    public static class OrderingTourViewModel {
        public static List<OrderViewModel> GetList(string valLanguage) {
            switch (valLanguage) {
                case "EN":
                    return OrderInEnglish;
                case "ES":
                    return OrderInSpanish;
                case "PO":
                    return OrderInPortugues;
                default:
                    return OrderInSpanish;

            }
        }
        public static List<OrderViewModel> OrderInSpanish {
            get {
                return new List<OrderViewModel>() {
                    new OrderViewModel(){
                        Id = 1,
                        Name = "Recomendados"
                    },
                    new OrderViewModel(){
                        Id = 2,
                        Name = "Menor Precio"
                    },
                    new OrderViewModel(){
                        Id = 3,
                        Name = "Mayor Precio"
                    },
                    new OrderViewModel(){
                        Id = 4,
                        Name = "A-Z"
                    },
                    new OrderViewModel(){
                        Id = 5,
                        Name = "Lo más nuevo"
                    }
                };
            }
        }

        public static List<OrderViewModel> OrderInEnglish {
            get {
                return new List<OrderViewModel>() {
                    new OrderViewModel(){
                        Id = 1,
                        Name = "Recommended"
                    },
                    new OrderViewModel(){
                        Id = 2,
                        Name = "Min Price"
                    },
                    new OrderViewModel(){
                        Id = 3,
                        Name = "Max Price"
                    },
                    new OrderViewModel(){
                        Id = 4,
                        Name = "A-Z"
                    },
                    new OrderViewModel(){
                        Id = 5,
                        Name = "Most new"
                    }
                };
            }
        }

        public static List<OrderViewModel> OrderInPortugues {
            get {
                return new List<OrderViewModel>() {
                    new OrderViewModel(){
                        Id = 1,
                        Name = "Recomendado"
                    },
                    new OrderViewModel(){
                        Id = 2,
                        Name = "Preço mais baixo"
                    },
                    new OrderViewModel(){
                        Id = 3,
                        Name = "Preço mais alto"
                    },
                    new OrderViewModel(){
                        Id = 4,
                        Name = "A-Z"
                    },
                    new OrderViewModel(){
                        Id = 5,
                        Name = "O mais novo"
                    }
                };
            }
        }

    }

    public class OrderViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
