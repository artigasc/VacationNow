using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace GoTour.DataAccess.Implementation {
    public class CategoryData : ICategoryData {
        public string vConnection = "Master";
        public string SelectAllByLanguage(string valIdLanguage) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary(); 
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valIdLanguage)
                }; 
                                                                                      
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_CategoryLanguage_Language", vConnection);
                List<Category> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = JsonConvert.SerializeObject(new List<Category>(), Formatting.Indented);
            }
            return vResult;
  
        }

        public List<Category> DataTableToList(DataTable table) {

            List<Category> vResult = new List<Category>();
            try {
                foreach (DataRow row in table.Rows) {
                    Category vTour = new Category {
                        Id = Guid.Parse(Convert.ToString(row["IdCategory"])),
                        IdCategory = Guid.Parse(Convert.ToString(row["IdCategory"])),
                        IdLanguage = Guid.Parse(Convert.ToString(row["IdLanguage"])),
                        Name = Convert.ToString(row["Name"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vTour);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Category>();
            }
            return vResult;
        }

        public City DataTableToElement(DataTable table) {

            City vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new City {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        Position = Convert.ToInt16(row["Position"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                }
            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }

        
    }
}