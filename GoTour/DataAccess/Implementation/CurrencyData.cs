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
    public class CurrencyData : ICurrencyData {
        public string vConnection = "Master";
        public string SelectAll() {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary(); 
            try {
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(null, "sp_Select_Currency_Active", vConnection);
                List<Currency> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = JsonConvert.SerializeObject(new List<Currency>());
            }
            return vResult;
  
        }

        public Currency SelectById(Guid valId) {
            Currency vResult = new Currency();
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
           
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Currency_Id", vConnection);
                Currency vData = DataTableToElement(vDatainTable);
                if (vData != null) {
                    vResult = vData;
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new Currency();
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }
        public List<Currency> DataTableToList(DataTable table) {
            
            List<Currency> vResult = new List<Currency>();
            try {
                foreach (DataRow row in table.Rows) {
                    Currency vCurrency = new Currency {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Country = Convert.ToString(row["Country"]),
                        Name = Convert.ToString(row["Name"]),
                        Code = Convert.ToString(row["Code"]),
                        Symbol = Convert.ToString(row["Symbol"]),
                        State = row["State"] == DBNull.Value ? 0 : Convert.ToInt32(row["State"]),                       
                    };
                    vResult.Add(vCurrency);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Currency>();
            }
            return vResult;
        }

        public Currency DataTableToElement(DataTable table) {

            Currency vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    Currency vCurrency = new Currency {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Country = Convert.ToString(row["Country"]),
                        Name = Convert.ToString(row["Name"]),
                        Code = Convert.ToString(row["Code"]),
                        Symbol = Convert.ToString(row["Symbol"]),
                        State = row["State"] == DBNull.Value ? 0 : Convert.ToInt32(row["State"]),
                    };
                    vResult = vCurrency;
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