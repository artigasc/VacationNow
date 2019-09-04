using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace GoTour.DataAccess.Implementation {
    public class UserPortalAdminData : IUserPortalAdminData {

        public string vConnection = "Master";
        
    
        #region Verificate
        public string VerifyUserPortal(string vUser, string vPassword) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParametersList = new List<SqlParameter>();
                vParametersList.Add(new SqlParameter("@Email", vUser));
                vParametersList.Add(new SqlParameter("@password", vPassword));
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParametersList, "sp_Select_UserPortal_Login", vConnection);
                List<UserPortalAdmin> vData = DataTableToListSearchUserPortal(vDatainTable);
                if (vData != null && vData.Count == 1) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                } else {
                    vResult = null;
                }
            } catch (Exception vEx) {
                vResult = vEx.Message;
            }
            return vResult;
        }

        #endregion

        #region Inserts
        public async Task<string> InsertUserPortal(UserPortalAdmin valUser) {
            string vResult = "4";
            try {
                UserPortalAdmin vUserExist = SelectUserPortalByEmail(valUser.UserName);
                if (vUserExist != null) {
                    vResult = "2";
                } else if (vUserExist == null) {

                    string vUrl = await UploadAzureHelper.UploadFilesToBlobStorageContainer(valUser.Photo?.NameFile, valUser.Photo?.FileData);


                    SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

                    List<SqlParameter> vListofParameters = new List<SqlParameter> {
                        new SqlParameter("@Id",valUser.Id != Guid.Empty ? valUser.Id : Guid.NewGuid()),
                        new SqlParameter("@UserName",!string.IsNullOrEmpty(valUser.UserName)?valUser.UserName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@Password",!string.IsNullOrEmpty(valUser.Password)?valUser.Password:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@FirstName",!string.IsNullOrEmpty(valUser.FirstName)?valUser.FirstName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@SecondName",valUser.SecondName),
                        new SqlParameter("@FirstLastname",!string.IsNullOrEmpty(valUser.FirstLastName)?valUser.FirstLastName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@SecondLastName",valUser.SecondLastName),
                        new SqlParameter("@Email",!string.IsNullOrEmpty(valUser.UserName)?valUser.UserName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@UrlPhoto",!string.IsNullOrEmpty(vUrl) ? vUrl : string.Empty),
                        new SqlParameter("@BirthDate",valUser.BirthDate!=DateTime.MinValue ? valUser.BirthDate : new DateTime(1900,1,1)),
                        new SqlParameter("@Phone",valUser.Phone),
                        new SqlParameter("@backmail",!string.IsNullOrEmpty(valUser.Backmail)?valUser.Backmail:string.Empty),
                        new SqlParameter("@IdCompany",valUser.IdCompany != Guid.Empty?valUser.IdCompany:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@State",valUser.State),
                        new SqlParameter("@UserCreate",valUser.UserCreate /*!string.IsNullOrEmpty(valUser.UserCreate)?valUser.UserCreate:throw new Exception(GlobalValues.vTextExceptionParameterNull)*/),
                        new SqlParameter("@DateCreate",valUser.DateCreate != DateTime.MinValue ? valUser.DateCreate : DateTime.Now)
                    };
                    bool vSuccessInsert = vSqlTools.ExecuteIUWithStoreProcedure(vListofParameters, "sp_Insert_UserPortal", vConnection);
                    if (vSuccessInsert) {
                        vResult = "1";
                    }
                }
            } catch (Exception e) {
                string vMsg = e.Message;
                vResult = "3";
            }
            return vResult;
        }


        #endregion


        #region  Updates

        public string UpdateUserPortal(UserPortalAdmin valUSerPortal) {
            string vResult = "4";
            try {
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                List<SqlParameter> vListofParameters = new List<SqlParameter> {
                        new SqlParameter("@Id",valUSerPortal.Id),
                        new SqlParameter("@Password",!string.IsNullOrEmpty(valUSerPortal.Password)?valUSerPortal.Password:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@FirstName",!string.IsNullOrEmpty(valUSerPortal.FirstName)?valUSerPortal.FirstName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@SecondName",valUSerPortal.SecondName),
                        new SqlParameter("@FirstLastname",!string.IsNullOrEmpty(valUSerPortal.FirstLastName)?valUSerPortal.FirstLastName:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@SecondLastName",valUSerPortal.SecondLastName),
                        new SqlParameter("@UrlPhoto",valUSerPortal.UrlPhoto),
                        new SqlParameter("@BirthDate",valUSerPortal.BirthDate!=DateTime.MinValue ? valUSerPortal.BirthDate : new DateTime(1900,1,1)),
                        new SqlParameter("@Phone",valUSerPortal.Phone),
                        new SqlParameter("@backmail",!string.IsNullOrEmpty(valUSerPortal.Backmail)?valUSerPortal.Backmail:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@IdCompany",valUSerPortal.IdCompany != Guid.Empty?valUSerPortal.IdCompany:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@UserUpdate",!string.IsNullOrEmpty(valUSerPortal.UserUpdate)?valUSerPortal.UserUpdate:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                        new SqlParameter("@DateUpdate",DateTime.Now),
                    };
                bool vSuccessUpdate = vSqlTools.ExecuteIUWithStoreProcedure(vListofParameters, "sp_Update_User_Portal", vConnection);
                if (vSuccessUpdate) {
                    vResult = "1";
                }                 
            } catch (Exception e) {
                string vMsg = e.Message;
                vResult = "3";
                
            }
            return vResult;

        }

        public string UpdateStateUserPortal(UserPortalAdmin valUserPortal) {
            string vResult = "4";
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                int valState = VerifyState(valUserPortal.State);
                if (valState < 2) {

                    List<SqlParameter> valParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id",valUserPortal.Id),
                    new SqlParameter("@State",valState)
                    };
                    bool vSuccessUpdate = vSqlTools.ExecuteIUWithStoreProcedure(valParameterList, "sp_Update_State_User_Portal", vConnection);
                    if (vSuccessUpdate) {
                        vResult = "1";
                    } 
                }
                
            } catch (Exception vEx) {
                string vMsg = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        #endregion

        #region Selects
        public string SelectUserPortalAll() {
            string vResult = "4";
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_User_Portal", vConnection);
                List<UserPortalAdmin> vData = DataTableToListSearchUserPortal(vDatainTable);
                
                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        public string SelectUserPortalById(Guid Id) {
            string vResult = "4";
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary VSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id",Id !=Guid.Empty?Id:throw new Exception(GlobalValues.vTextExceptionParameterNull))
                };
                vDatainTable = VSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_select_UserPortal_ById", vConnection);
                UserPortalAdmin vData = DataTableToSelectUser(vDatainTable);
                if (vData.UserName!=null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        public UserPortalAdmin SelectUserPortalByEmail(string email) {
            UserPortalAdmin vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vListofParameters = new List<SqlParameter> {
                    new SqlParameter("@Email",email)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vListofParameters, "sp_Select_UserPortal_Email", vConnection);
                vResult = DataTableToElementForUserPortal(vDatainTable);
            } catch (Exception) {
                vResult = null;
            }

            return vResult;
        }


        #endregion






        #region Common

                     
        public List<UserPortalAdmin> DataTableToListSearchUserPortal(DataTable table) {
            List<UserPortalAdmin> vResult = new List<UserPortalAdmin>();
            try {
                foreach (DataRow row in table.Rows) {
                    UserPortalAdmin valUserPortal = new UserPortalAdmin {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        UserName = !string.IsNullOrEmpty(Convert.ToString(row["UserName"])) ? Convert.ToString(row["UserName"]) : string.Empty,
                        Password = !string.IsNullOrEmpty(Convert.ToString(row["Password"])) ? Convert.ToString(row["Password"]) : string.Empty,
                        FirstName = !string.IsNullOrEmpty(Convert.ToString(row["FirstName"])) ? Convert.ToString(row["FirstName"]) : string.Empty,
                        SecondName = !string.IsNullOrEmpty(Convert.ToString(row["SecondName"])) ? Convert.ToString(row["SecondName"]) : string.Empty,
                        FirstLastName = !string.IsNullOrEmpty(Convert.ToString(row["FirstLastName"])) ? Convert.ToString(row["FirstLastName"]) : string.Empty,
                        SecondLastName = !string.IsNullOrEmpty(Convert.ToString(row["SecondLastName"])) ? Convert.ToString(row["SecondLastName"]) : string.Empty,
                        Email = !string.IsNullOrEmpty(Convert.ToString(row["Email"])) ? Convert.ToString(row["Email"]) : string.Empty,
                        UrlPhoto = !string.IsNullOrEmpty(Convert.ToString(row["UrlPhoto"])) ? Convert.ToString(row["UrlPhoto"]) : string.Empty,
                        BirthDate = row["BirthDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthDate"]) : DateTime.MinValue,
                        Phone = !string.IsNullOrEmpty(Convert.ToString(row["Phone"])) ? Convert.ToString(row["Phone"]) : string.Empty,
                        Backmail = !string.IsNullOrEmpty(Convert.ToString(row["Backmail"])) ? Convert.ToString(row["Backmail"]) : string.Empty,
                        IdCompany = Guid.Parse(Convert.ToString(row["IdCompany"])),
                        CompanyName = !string.IsNullOrEmpty(Convert.ToString(row["CompanyName"])) ? Convert.ToString(row["CompanyName"]) : string.Empty,
                        State = Convert.ToInt32(row["State"]),
                        UserCreate = !string.IsNullOrEmpty(Convert.ToString(row["UserCreate"])) ? Convert.ToString(row["UserCreate"]) : string.Empty,
                        DateCreate = row["DateCreate"] != DBNull.Value ? Convert.ToDateTime(row["DateCreate"]) : DateTime.MinValue,
                        UserUpdate = !string.IsNullOrEmpty(Convert.ToString(row["UserUpdate"])) ? Convert.ToString(row["UserUpdate"]) : string.Empty,
                        DateUpdate = row["DateUpdate"] != DBNull.Value ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                    };
                    vResult.Add(valUserPortal);
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<UserPortalAdmin>();
            }
            return vResult;
        }
        public UserPortalAdmin DataTableToSelectUser(DataTable table) {
            UserPortalAdmin vResult = new UserPortalAdmin();
            try {
                foreach (DataRow row in table.Rows) {
                    UserPortalAdmin valUserPortal = new UserPortalAdmin {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        UserName = !string.IsNullOrEmpty(Convert.ToString(row["UserName"])) ? Convert.ToString(row["UserName"]) : string.Empty,
                        Password = !string.IsNullOrEmpty(Convert.ToString(row["Password"])) ? Convert.ToString(row["Password"]) : string.Empty,
                        FirstName = !string.IsNullOrEmpty(Convert.ToString(row["FirstName"])) ? Convert.ToString(row["FirstName"]) : string.Empty,
                        SecondName = !string.IsNullOrEmpty(Convert.ToString(row["SecondName"])) ? Convert.ToString(row["SecondName"]) : string.Empty,
                        FirstLastName = !string.IsNullOrEmpty(Convert.ToString(row["FirstLastName"])) ? Convert.ToString(row["FirstLastName"]) : string.Empty,
                        SecondLastName = !string.IsNullOrEmpty(Convert.ToString(row["SecondLastName"])) ? Convert.ToString(row["SecondLastName"]) : string.Empty,
                        Email = !string.IsNullOrEmpty(Convert.ToString(row["Email"])) ? Convert.ToString(row["Email"]) : string.Empty,
                        UrlPhoto = !string.IsNullOrEmpty(Convert.ToString(row["UrlPhoto"])) ? Convert.ToString(row["UrlPhoto"]) : string.Empty,
                        BirthDate = row["BirthDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthDate"]) : DateTime.MinValue,
                        Phone = !string.IsNullOrEmpty(Convert.ToString(row["Phone"])) ? Convert.ToString(row["Phone"]) : string.Empty,
                        Backmail = !string.IsNullOrEmpty(Convert.ToString(row["Backmail"])) ? Convert.ToString(row["Backmail"]) : string.Empty,
                        IdCompany = Guid.Parse(Convert.ToString(row["IdCompany"])),
                        CompanyName = !string.IsNullOrEmpty(Convert.ToString(row["CompanyName"])) ? Convert.ToString(row["CompanyName"]) : string.Empty,
                        State = Convert.ToInt32(row["State"]),
                        UserCreate = !string.IsNullOrEmpty(Convert.ToString(row["UserCreate"])) ? Convert.ToString(row["UserCreate"]) : string.Empty,
                        DateCreate = row["DateCreate"] != DBNull.Value ? Convert.ToDateTime(row["DateCreate"]) : DateTime.MinValue,
                        UserUpdate = !string.IsNullOrEmpty(Convert.ToString(row["UserUpdate"])) ? Convert.ToString(row["UserUpdate"]) : string.Empty,
                        DateUpdate = row["DateUpdate"] != DBNull.Value ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                    };
                    //vResult.Add(valUserPortal);
                    vResult = valUserPortal;
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new UserPortalAdmin();
            }
            return vResult;
        }

        public UserPortalAdmin DataTableToElementForUserPortal(DataTable table) {
            UserPortalAdmin vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new UserPortalAdmin() {
                        Id = Guid.Parse(row["id"].ToString()),
                        UserName = row["username"].ToString(),
                        Password = row["password"].ToString(),
                        FirstName = row["firstname"].ToString(),
                        SecondName = row["secondname"].ToString(),
                        FirstLastName = row["firstLastname"].ToString(),
                        SecondLastName = row["secondLastname"].ToString(),
                        Email = row["email"].ToString(),
                        UrlPhoto = row["urlphoto"].ToString(),
                        BirthDate = Convert.ToDateTime(row["birthdate"]),
                        Phone = row["phone"].ToString(),
                        IdCompany = Guid.Parse(row["idcompany"].ToString()),
                        State = Convert.ToInt32(row["state"]),
                        UserCreate = row["usercreate"].ToString(),
                        DateCreate = Convert.ToDateTime(row["datecreate"])
                    };
                }
            } catch (Exception) {

                vResult = null;
            }
            return vResult;
        }


        public int VerifyState(int valUserPortal) {
            int vState=0;
            if (valUserPortal == 1) {
                return vState;
            } else if (valUserPortal == 0) {
                vState = 1;
                return vState;
            }
            return vState;
        }

        


        #endregion


    }
}