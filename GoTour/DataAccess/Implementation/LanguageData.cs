using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;


namespace GoTour.DataAccess.Implementation {
    public class LanguageData : ILanguageData {
        public string vConnection = "Master";
        public string SelectAll() {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary(); 
            try {
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(null, "sp_Select_Language", vConnection);
                List<Language> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = JsonConvert.SerializeObject(new List<City>());
            }
            return vResult;
  
        }

        public string SelectById(Guid valId) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
           
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_City", "Master");
                City vData = DataTableToElement(vDatainTable);
                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = string.Empty;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }
        public List<Language> DataTableToList(DataTable table) {
            
            List<Language> vResult = new List<Language>();
            try {
                foreach (DataRow row in table.Rows) {
                    Language vLanguage = new Language {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        Initials = row["Initials"] == DBNull.Value ? string.Empty : Convert.ToString(row["Initials"]),
                        State = row["State"] == DBNull.Value ? 0 : Convert.ToInt32(row["State"]),                       
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = row["UserUpdate"] == DBNull.Value ? string.Empty : Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vLanguage);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Language>();
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

        public bool Insert(City valCity) {
            bool vResult = false;
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                   
                    new SqlParameter("@Id", Guid.NewGuid()),
                    new SqlParameter("@Name", !string.IsNullOrEmpty(valCity.Name) ? valCity.Name : string.Empty),
                    new SqlParameter("@Position", valCity.Position >= 0 ? valCity.Position : 0),
                    new SqlParameter("@State", GlobalValues.vDefaultValueState),
                    new SqlParameter("@UserCreate", !string.IsNullOrEmpty(valCity.UserCreate) ? valCity.UserCreate : string.Empty),
                    new SqlParameter("@DateCreate", DateTime.Now)
                };
                vResult = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Insert_City", "Master");
            } catch (Exception) {
                vResult = false;
            }
            return vResult;
        }

        public bool Update(City valCity) {
            bool vResult = false;
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                if (valCity.Position >= GlobalValues.vZeroValuePosition) {
                    vParameterList.Add(new SqlParameter("@Position", valCity.Position));
                }
                if (!string.IsNullOrEmpty(valCity.Name)) {
                    vParameterList.Add(new SqlParameter("@Name", valCity.Name));
                }
                vParameterList.Add(new SqlParameter("@Id", valCity.Id));
                vParameterList.Add(new SqlParameter("@State", GlobalValues.vDefaultValueState));
                vParameterList.Add(new SqlParameter("@UserUpdate", valCity.UserUpdate));
                vParameterList.Add(new SqlParameter("@DateUpdate", DateTime.Now));
                vResult = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_City", "Master");
            } catch (Exception) {
                vResult = false;
            }
            return vResult;
        }
    }
}