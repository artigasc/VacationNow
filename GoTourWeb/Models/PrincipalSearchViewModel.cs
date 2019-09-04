using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public class PrincipalSearchViewModel {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
    }
    public class SearchElementViewModel {
        public string ValueInput { get; set; }
        public Guid IdLanguage { get; set; }
    }

    public class CitySearchViewModel {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }

        public static List<CitySearchViewModel> ListSearchCity {
            get {
                return new List<CitySearchViewModel>() {
                    new CitySearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Trujillo",
                        Icon = "far fa-building",
                        Url = "City/Info"
                    },
                    new CitySearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Tarapoto",
                        Icon = "far fa-building",
                        Url = "City/Info"
                    },
                    new CitySearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Lima",
                        Icon = "far fa-building",
                        Url = "City/Info"
                    },
                    new CitySearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Pasco",
                        Icon = "far fa-building",
                        Url = "City/Info"
                    }
                };
            }
        }
    }

    public class TourSearchViewModel {
        public string Id { get; set; }
        public string IdCyity { get; set; }
        public string Nombre { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }

        public static List<TourSearchViewModel> ListSearchTour {
            get {
                return new List<TourSearchViewModel> {
                    new TourSearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873123",
                        IdCyity = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Aventura en Zona Historica",
                        Icon = "fas fa-hiking",
                        Url = "Tours/Info"
                    },
                    new TourSearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873124",
                        IdCyity = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Ciudad de Chan Chan",
                        Icon = "fas fa-hiking",
                        Url = "Tours/Info"
                    },
                    new TourSearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873125",
                        IdCyity = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Ciudad de Chan Chan",
                        Icon = "fas fa-hiking",
                        Url = "Tours/Info"
                    },
                    new TourSearchViewModel(){
                        Id = "fe803a7c-2965-11e9-b210-d663bd873126",
                        IdCyity = "fe803a7c-2965-11e9-b210-d663bd873d93",
                        Nombre = "Ciudad de Chan Chan",
                        Icon = "fas fa-hiking",
                        Url = "Tours/Info"
                    }
                };
            }
        }
    }
}
