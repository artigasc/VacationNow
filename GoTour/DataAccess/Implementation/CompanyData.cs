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
    public class CompanyData : ICompanyData {
        public string vConnection = "Master";
        public string SelectAll() {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary(); 
            
            try {
                vDatainTable = vSqlTools.GetResultSql("sp_Select_All_Company", "Master");
                List<Company> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = string.Empty;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
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
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Company", "Master");
                Company vData = DataTableToElement(vDatainTable);
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
        public List<Company> DataTableToList(DataTable table) {
            
            List<Company> vResult = new List<Company>();
            try {
                foreach (DataRow row in table.Rows) {
                    Company vCity = new Company {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
                        Phone = Convert.ToString(row["Phone"]),
                        Movil = Convert.ToString(row["Movil"]),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        Address = Convert.ToString(row["Address"]),
                        IdDistrict = Guid.Parse(Convert.ToString(row["IdDistrict"])),
                        Email1 = Convert.ToString(row["Email1"]),
                        Email2 = Convert.ToString(row["Email2"]),
                        IsEnable = Convert.ToBoolean(row["IsEnable"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vCity);
                }
            } catch (Exception) {
                vResult = new List<Company>();
            }
            return vResult;
        }

        private Company DataTableToElement(DataTable table) {

            Company vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new Company {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
                        Phone = Convert.ToString(row["Phone"]),
                        Movil = Convert.ToString(row["Movil"]),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        Address  = Convert.ToString(row["Address"]),
                        IdDistrict = Guid.Parse(Convert.ToString(row["IdDistrict"])),
                        Email1 = Convert.ToString(row["Email1"]),
                        Email2 = Convert.ToString(row["Email2"]),
                        Web = Convert.ToString(row["Web"]),
                        IsEnable = Convert.ToBoolean(row["IsEnable"]),
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

        public bool Insert(Company valCompany) {
            bool vResult = false;
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", Guid.NewGuid()),
                    new SqlParameter("@Name", !string.IsNullOrEmpty(valCompany.Name) ? valCompany.Name : string.Empty),
                    new SqlParameter("@TypeNumberDocument", !string.IsNullOrEmpty(valCompany.TypeNumberDocument) ? valCompany.TypeNumberDocument : string.Empty),
                    new SqlParameter("@NumberDocument", !string.IsNullOrEmpty(valCompany.NumberDocument) ? valCompany.NumberDocument : string.Empty),
                    new SqlParameter("@Phone", !string.IsNullOrEmpty(valCompany.Phone) ? valCompany.Phone : string.Empty),
                    new SqlParameter("@Movil", !string.IsNullOrEmpty(valCompany.Movil) ? valCompany.Movil : string.Empty),
                    new SqlParameter("@UrlPhoto", !string.IsNullOrEmpty(valCompany.UrlPhoto) ? valCompany.UrlPhoto : string.Empty),
                    new SqlParameter("@Address", !string.IsNullOrEmpty(valCompany.Address) ? valCompany.Address : string.Empty),
                    new SqlParameter("@IdDistrict", valCompany.IdDistrict),
                    new SqlParameter("@Email1", !string.IsNullOrEmpty(valCompany.Email1) ? valCompany.Email1 : string.Empty),
                    new SqlParameter("@Email2", !string.IsNullOrEmpty(valCompany.Email2) ? valCompany.Email1 : string.Empty),
                    new SqlParameter("@IsEnable", valCompany.IsEnable),
                    new SqlParameter("@State", valCompany.State),
                    new SqlParameter("@UserCreate", !string.IsNullOrEmpty(valCompany.UserCreate) ? valCompany.UserCreate : string.Empty),
                    new SqlParameter("@DateCreate", DateTime.Now)
                };
                vResult = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Insert_Company", "Master");
            return vResult;
        }

        public bool Update(Company valCompany) {
            bool vResult = false;
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                Company vCompanyOld = new Company();
                vParameterList.Add(new SqlParameter("@Id", valCompany.Id));
                DataTable vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Company", "Master");
                vCompanyOld = DataTableToElement(vDatainTable);

                if (vCompanyOld != null) {
                    vParameterList.Add(new SqlParameter("@Name", !string.Equals(valCompany.Name,vCompanyOld.Name) ? valCompany.Name : vCompanyOld.Name ));
                    vParameterList.Add(new SqlParameter("@TypeNumberDocument", !string.Equals(valCompany.TypeNumberDocument, vCompanyOld.TypeNumberDocument) ? valCompany.TypeNumberDocument : vCompanyOld.TypeNumberDocument));
                    vParameterList.Add(new SqlParameter("@NumberDocument", !string.Equals(valCompany.NumberDocument, vCompanyOld.NumberDocument) ? valCompany.NumberDocument : vCompanyOld.NumberDocument));
                    vParameterList.Add(new SqlParameter("@Phone", !string.Equals(valCompany.Phone, vCompanyOld.Phone) ? valCompany.Phone : vCompanyOld.Phone));
                    vParameterList.Add(new SqlParameter("@Movil", !string.Equals(valCompany.Movil, vCompanyOld.Movil) ? valCompany.Movil : vCompanyOld.Movil));
                    vParameterList.Add(new SqlParameter("@UrlPhoto", !string.Equals(valCompany.UrlPhoto, vCompanyOld.UrlPhoto) ? valCompany.UrlPhoto : vCompanyOld.UrlPhoto));
                    vParameterList.Add(new SqlParameter("@Address", !string.Equals(valCompany.Address, vCompanyOld.Address) ? valCompany.Address : vCompanyOld.Address));
                    vParameterList.Add(new SqlParameter("@IdDistrict", !Equals(valCompany.IdDistrict, vCompanyOld.IdDistrict) ? valCompany.IdDistrict : vCompanyOld.IdDistrict));
                    vParameterList.Add(new SqlParameter("@Email1", !string.Equals(valCompany.Email1, vCompanyOld.Email1) ? valCompany.Email1 : vCompanyOld.Email1));
                    vParameterList.Add(new SqlParameter("@Email2", !string.Equals(valCompany.Email2, vCompanyOld.Email2) ? valCompany.Email2 : vCompanyOld.Email2));
                    vParameterList.Add(new SqlParameter("@IsEnable", !Equals(valCompany.IsEnable, vCompanyOld.IsEnable) ? valCompany.IsEnable : vCompanyOld.IsEnable));
                    vParameterList.Add(new SqlParameter("@State", !Equals(valCompany.State,vCompanyOld.State) ? valCompany.State : vCompanyOld.State));
                    vParameterList.Add(new SqlParameter("@UserUpdate", valCompany.UserUpdate));
                    vResult = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_Company", "Master");

                }
            } catch (Exception) {
                vResult = false;
            }
            return vResult;
        }

        public Company SelectCompanyById(Guid valId) {
            Company vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Company", "Master");
                vResult = DataTableToElement(vDatainTable);
                
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new Company();
            }
            return vResult;

        }

        public Company SelectCompanyByPayment(Guid valId) {
            Company vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Company_ByPayment", vConnection);
                vResult = DataTableToElement(vDatainTable);

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new Company();
            }
            return vResult;

        }
    }
}