using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Models{
    public class TypeDocument {
        public static List<TypeDocumentElement> GetList(string valLanguage) {
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
        public static List<TypeDocumentElement> TypesDocumentES {
            get {
                return new List<TypeDocumentElement>() {
                    new TypeDocumentElement(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElement(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElement(){
                        Id = "2",
                        Name = "Pasaporte"
                    },
                    new TypeDocumentElement(){
                        Id = "3",
                        Name = "Carnet de Extranjería"
                    }
                };
            }
        }

        public static List<TypeDocumentElement> TypesDocumentEN {
            get {
                return new List<TypeDocumentElement>() {
                    new TypeDocumentElement(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElement(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElement(){
                        Id = "2",
                        Name = "Passport"
                    },
                    new TypeDocumentElement(){
                        Id = "3",
                        Name = "Immigration Card"
                    }
                };
            }
        }
        public static List<TypeDocumentElement> TypesDocumentPO {
            get {
                return new List<TypeDocumentElement>() {
                    new TypeDocumentElement(){
                        Id = "0",
                        Name = "RUC"
                    },
                    new TypeDocumentElement(){
                        Id = "1",
                        Name = "DNI"
                    },
                    new TypeDocumentElement(){
                        Id = "2",
                        Name = "Passaporte"
                    },
                    new TypeDocumentElement(){
                        Id = "3",
                        Name = "Cartão de Imigração"
                    }
                };
            }
        }

    }
    public class TypeDocumentElement {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
