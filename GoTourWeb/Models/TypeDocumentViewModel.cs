using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Models {
    public static class TypeDocumentViewModel {
        public static List<TypeDocumentElementViewModel> GetList(string valLanguage) {
            switch (valLanguage) {
                case "EN":
                    return TypesDocumentEN;
                case "ES":
                    return TypesDocumentES;
                case "PO":
                    return TypesDocumentPO;
                default:
                    return TypesDocumentES;

            }
        }
        public static List<TypeDocumentElementViewModel> TypesDocumentES {
            get {
                return new List<TypeDocumentElementViewModel>() {
                    new TypeDocumentElementViewModel(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "2",
                        Name = "Pasaporte"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "3",
                        Name = "Carnet de Extranjería"
                    }
                };
            }
        }

        public static List<TypeDocumentElementViewModel> TypesDocumentEN {
            get {
                return new List<TypeDocumentElementViewModel>() {
                    new TypeDocumentElementViewModel(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "2",
                        Name = "Passport"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "3",
                        Name = "Immigration Card"
                    }
                };
            }
        }
        public static List<TypeDocumentElementViewModel> TypesDocumentPO {
            get {
                return new List<TypeDocumentElementViewModel>() {
                    new TypeDocumentElementViewModel(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "2",
                        Name = "Passaporte"
                    },
                    new TypeDocumentElementViewModel(){
                        Id = "3",
                        Name = "Cartão de Imigração"
                    }
                };
            }
        }
       
    }
    public class TypeDocumentElementViewModel {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
