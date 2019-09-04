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
    public class UserData : IUserData {
        public string vConnection = "Master";

        #region Inserts
        public string Insert(User valUser) {
            string vResult = string.Empty;
            try {
                User vUserExist = SelectByEmail(valUser.Email);
                if (vUserExist != null) {
                    vResult = "3";
                } else if (vUserExist == null) {
                    SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                    List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", Guid.NewGuid()),
                    new SqlParameter("@TypeNumberDocument", !string.IsNullOrEmpty(valUser.TypeNumberDocument) ? valUser.TypeNumberDocument : string.Empty),
                    new SqlParameter("@NumberDocument", !string.IsNullOrEmpty(valUser.NumberDocument) ? valUser.NumberDocument : string.Empty),
                    new SqlParameter("@Nacionality", !string.IsNullOrEmpty(valUser.Nacionality) ? valUser.Nacionality : string.Empty),
                    new SqlParameter("@UserName", valUser.UserName),
                    new SqlParameter("@Password", valUser.Password),
                    new SqlParameter("@FirstName", valUser.FirstName),
                    new SqlParameter("@SecondName", !string.IsNullOrEmpty(valUser.SecondName) ? valUser.SecondName : string.Empty),
                    new SqlParameter("@FirstLastName", valUser.FirstLastName),
                    new SqlParameter("@SecondLastName", !string.IsNullOrEmpty(valUser.SecondLastName) ? valUser.SecondLastName : string.Empty),
                    new SqlParameter("@Email", !string.IsNullOrEmpty(valUser.Email) ? valUser.Email : string.Empty),
                    new SqlParameter("@UrlPhoto", !string.IsNullOrEmpty(valUser.UrlPhoto) ? valUser.UrlPhoto : string.Empty),
                    new SqlParameter("@BirthDate", valUser.BirthDate!=DateTime.MinValue ? valUser.BirthDate : new DateTime(1900,1,1)),
                    new SqlParameter("@IdMasterCity", valUser.IdMasterCity != Guid.Empty ? valUser.IdMasterCity : Guid.Empty),
                    new SqlParameter("@Phone", !string.IsNullOrEmpty(valUser.Phone) ? valUser.Phone : string.Empty),
                    new SqlParameter("@IdCompany", valUser.IdCompany != Guid.Empty ? valUser.IdCompany : Guid.Empty),
                    new SqlParameter("@State", valUser.State),
                    new SqlParameter("@UserCreate", !string.IsNullOrEmpty(valUser.UserCreate) ? valUser.UserCreate : string.Empty),
                    new SqlParameter("@DateCreate", DateTime.Now)
                };
                    bool vInsert = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Insert_UserLogin", vConnection);
                    if (vInsert) {
                        vResult = "1";
                    } else {
                        vResult = "4";
                    }
                }
            } catch (Exception) {
                vResult = "4";

            }
            return vResult;
        }

        #endregion

        #region Updates
        public string UpdateUser(User valUser) {
            string vResult = "4";
            try {
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valUser.Id != Guid.Empty ? valUser.Id : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@TypeNumberDocument", !string.IsNullOrEmpty(valUser.TypeNumberDocument) ? valUser.TypeNumberDocument : string.Empty),
                    new SqlParameter("@NumberDocument", !string.IsNullOrEmpty(valUser.NumberDocument) ? valUser.NumberDocument : string.Empty),
                    new SqlParameter("@Nacionality", !string.IsNullOrEmpty(valUser.Nacionality) ? valUser.Nacionality : string.Empty),
                    new SqlParameter("@Password", !string.IsNullOrEmpty(valUser.Password) ? valUser.Password : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@FirstName", !string.IsNullOrEmpty(valUser.FirstName) ? valUser.FirstName : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@SecondName", !string.IsNullOrEmpty(valUser.SecondName) ? valUser.SecondName : string.Empty),
                    new SqlParameter("@FirstLastName", !string.IsNullOrEmpty(valUser.FirstLastName) ? valUser.FirstLastName : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@SecondLastName", !string.IsNullOrEmpty(valUser.SecondLastName) ? valUser.SecondLastName : string.Empty),
                    new SqlParameter("@BirthDate", valUser.BirthDate!=DateTime.MinValue ? valUser.BirthDate : new DateTime(1900,1,1)),
                    new SqlParameter("@IdMasterCity", valUser.IdMasterCity != Guid.Empty ? valUser.IdMasterCity : Guid.Empty),
                    new SqlParameter("@Phone", !string.IsNullOrEmpty(valUser.Phone) ? valUser.Phone : string.Empty),
                    new SqlParameter("@IdCompany", valUser.IdCompany != Guid.Empty ? valUser.IdCompany : Guid.Empty),
                    new SqlParameter("@UserUpdate", !string.IsNullOrEmpty(valUser.UserCreate) ? valUser.UserCreate : string.Empty),
                    new SqlParameter("@DateUpdate", DateTime.Now)
                };

                bool vUpdate = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_User", vConnection);
                if (vUpdate) {
                    vResult = "1";
                }

            } catch (Exception e) {
                string vMsg = e.Message;
                vResult = "3";

            }
            return vResult;
        }

        public string UpdateStateUser(Guid valId,int valState ) {
            string vResult = "4";
            SQLToolsLibrary vSTools = new SQLToolsLibrary();
            try {
                int vState = VerifyStateFront(valState);
                if (vState<2) {
                    List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId != Guid.Empty ? valId :throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@State", vState != int.MinValue ? vState : 0)
                };

                    bool vUpdate = vSTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_update_User_State", vConnection);
                    if (vUpdate) {
                        vResult = "1";
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        #endregion

        #region Selects
        public string SelectUserWebById(Guid Id) {
            string vResult = "4";
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary VSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id",Id !=Guid.Empty?Id:throw new Exception(GlobalValues.vTextExceptionParameterNull))
                };
                vDatainTable = VSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_UserById", vConnection);
                User vData = DataTableToElement(vDatainTable);
                if (vData.UserName != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        public string SelectUserAll() {
            string vResult = "4";
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_User_All", vConnection);
                List<User> vData = DataTableToListUser(vDatainTable);

                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        #endregion




        #region Common
        public int VerifyStateFront(int valState) {
            int vResult=0;
            if (valState == 1) {
                return vResult;
            } else if (valState==0) {
                vResult = 1;
                return vResult;
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
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
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

        public User SelectByEmail(string valEmail) {
            User vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("Email", valEmail)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_User_Email", vConnection);
                vResult = DataTableToElement(vDatainTable);

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }

        public User DataTableToElement(DataTable table) {

            User vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new User {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
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
                }
            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }
                
        public List<User> DataTableToListUser(DataTable table){
            List<User> vResult = new List<User>();
            try {
                foreach (DataRow row in table.Rows) {
                    User vUser = new User {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        TypeNumberDocument = Convert.ToString(row["TypeNumberDocument"]),
                        NumberDocument = Convert.ToString(row["NumberDocument"]),
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
        #endregion

    }


}