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
    public class AccountData : IAccountData {
        public string vConnection = "Master";

        public string Verify(string vUser, string vPassword) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                vParameterList.Add(new SqlParameter("@UserName", vUser));
                vParameterList.Add(new SqlParameter("@Password", vPassword));
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_User", vConnection);
                List<User> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count == 1) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = string.Empty;
            }
            return vResult;
        }

        public List<User> DataTableToList(DataTable table) {

            List<User> vResult = new List<User>();
            try {
                foreach (DataRow row in table.Rows) {
                    User vUser = new User {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument =  Convert.ToString(row["NumberDocument"]),
                        Nacionality = Convert.ToString(row["Nacionality"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Password = Convert.ToString(row["Password"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        SecondName = Convert.ToString(row["SecondName"]),
                        FirstLastName = Convert.ToString(row["FirstLastName"]),
                        SecondLastName = Convert.ToString(row["SecondLastName"]),
                        Email = Convert.ToString(row["Email"]),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        BirthDate = Convert.ToDateTime(row["BirthDate"]),
                        Phone = Convert.ToString(row["Phone"]),
                        IdCompany = Guid.Parse(Convert.ToString(row["IdCompany"])),
                        IdMasterCity = Guid.Parse(Convert.ToString(row["IdMasterCity"])),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vUser);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<User>();
            }
            return vResult;
        }


    }
}