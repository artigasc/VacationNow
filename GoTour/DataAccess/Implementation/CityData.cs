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
using System.Threading.Tasks;
using System.Web;


namespace GoTour.DataAccess.Implementation {
    public class CityData : ICityData {
        public string vConnection = "Master";

        #region Selects
        public string SelectByLanguage(string valIdLanguage) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valIdLanguage)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_City_Language", vConnection);
                List<City> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = JsonConvert.SerializeObject(new List<City>());
            }
            return vResult;

        }

        public string SelectById(Guid valId) {
            string vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id", valId)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_City", vConnection);
                List<City> vData = DataTableToList(vDatainTable);
                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;

        }

        public string SelectCityAll() {
            string vResult = string.Empty;
            SQLToolsLibrary vToolsLibrary = new SQLToolsLibrary();
            DataTable vDatainTable = new DataTable();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();

                vDatainTable = vToolsLibrary.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_City_All", vConnection);
                List<City> vData = DataTableToList(vDatainTable);
                if (vData != null) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        public City SelectUserPortalByName(string valName) {
            City vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try {
                List<SqlParameter> vListofParameters = new List<SqlParameter> {
                    new SqlParameter("@Email",valName)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vListofParameters, "sp_Select_City_BYName", vConnection);
                vResult = DataTableToElement(vDatainTable);
            } catch (Exception) {
                vResult = null;
            }
            return vResult;
        }
        #endregion


        #region Inserts
        public async Task<string> Insert(ListCityLanguage valCity) {
            string vResult = "4";
            try {
                City vUserExist = SelectUserPortalByName(valCity.ListCity.FirstOrDefault().Name);
                if (vUserExist != null) {
                    vResult = "5";
                } else if (vUserExist == null) {
                    string vUrl = await UploadAzureHelper.UploadFilesToBlobStorageContainer(valCity.ListCity.FirstOrDefault().Photo?.NameFile, valCity.ListCity.FirstOrDefault().Photo?.FileData);
                    if (vUrl != null) {
                        SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                        Guid IdCity = Guid.NewGuid();
                        List<SqlParameter> vParameterList = new List<SqlParameter> {
                            new SqlParameter("@Id",IdCity!=Guid.Empty?IdCity:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Name",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().Name)?valCity.ListCity.FirstOrDefault().Name:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Icon",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().Icon)?valCity.ListCity.FirstOrDefault().Icon:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Temperature",valCity.ListCity.FirstOrDefault().Temperature !=int.MinValue?valCity.ListCity.FirstOrDefault().Temperature:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Altitude",valCity.ListCity.FirstOrDefault().Altitude !=int.MinValue?valCity.ListCity.FirstOrDefault().Altitude:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Population",valCity.ListCity.FirstOrDefault().Population !=int.MinValue?valCity.ListCity.FirstOrDefault().Population:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@UrlPhoto",!string.IsNullOrEmpty(vUrl) ? vUrl : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Position",valCity.ListCity.FirstOrDefault().Position !=int.MinValue?valCity.ListCity.FirstOrDefault().Position:0),
                            new SqlParameter("@State",valCity.ListCity.FirstOrDefault().State),
                            new SqlParameter("@UserCreate",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().UserCreate) ? valCity.ListCity.FirstOrDefault().UserCreate : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@DateCreate",valCity.ListCity.FirstOrDefault().DateCreate !=DateTime.MinValue ? valCity.ListCity.FirstOrDefault().DateCreate:DateTime.Now)
                        };
                        bool vResponseCityLanguage = InsertarListaCityLanguage(valCity.ListCity, IdCity);
                        bool vResponseCity = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Insert_City", vConnection);

                        if (vResponseCity) {
                            if (vResponseCityLanguage) {
                                return vResult = "1";
                            }
                            return vResult = "2";
                        }
                    }
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return vResult = "3";
            }
            return vResult;
        }
        #endregion

        #region Updates
        public async Task<string> Update(ListCityLanguage valCity) {
            string vResult = "4";
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                string vUrl = await UploadAzureHelper.UploadFilesToBlobStorageContainer(valCity.ListCity.FirstOrDefault().Photo?.NameFile, valCity.ListCity.FirstOrDefault().Photo?.FileData);
                if (vUrl != null)
                {
                    List<SqlParameter> vParameterList = new List<SqlParameter> {
                            new SqlParameter("@Id",valCity.ListCity.FirstOrDefault().Id!=Guid.Empty?valCity.ListCity.FirstOrDefault().Id:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Name",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().Name)?valCity.ListCity.FirstOrDefault().Name:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Icon",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().Icon)?valCity.ListCity.FirstOrDefault().Icon:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Temperature",valCity.ListCity.FirstOrDefault().Temperature !=int.MinValue?valCity.ListCity.FirstOrDefault().Temperature:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Altitude",valCity.ListCity.FirstOrDefault().Altitude !=int.MinValue?valCity.ListCity.FirstOrDefault().Altitude:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Population",valCity.ListCity.FirstOrDefault().Population !=int.MinValue?valCity.ListCity.FirstOrDefault().Population:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@UrlPhoto",!string.IsNullOrEmpty(vUrl) ? vUrl : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@Position",valCity.ListCity.FirstOrDefault().Position !=int.MinValue?valCity.ListCity.FirstOrDefault().Position:0),
                            new SqlParameter("@State",valCity.ListCity.FirstOrDefault().State),
                            new SqlParameter("@UserUpdate",!string.IsNullOrEmpty(valCity.ListCity.FirstOrDefault().UserUpdate) ? valCity.ListCity.FirstOrDefault().UserUpdate : throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                            new SqlParameter("@DateUpdate",valCity.ListCity.FirstOrDefault().DateUpdate !=DateTime.MinValue ? valCity.ListCity.FirstOrDefault().DateUpdate:DateTime.Now)
                };
                    bool vResponseCityLanguage = UpdateListCityLanguage(valCity.ListCity);
                    bool vResponseCity = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_City", vConnection);
                    if (vResponseCity && vResponseCityLanguage)
                    {
                        return vResult = "1";
                    }
                    else if (vResponseCity || vResponseCityLanguage)
                    {
                        return vResult = "2";
                    }
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        public string UpdateState(City valCity) {
            string vResult = "4";
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@Id",valCity.Id),
                    new SqlParameter("@State",valCity.State)
                };
                bool vResponse = vSqlTools.ExecuteIUWithStoreProcedure(vParameterList, "sp_Update_City_State", vConnection);
                if (vResponse) {
                    vResult = "1";
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
            }
            return vResult;
        }

        #endregion





        #region Common

        public bool UpdateListCityLanguage (List<City> valCity){
            bool vResult = false;

            try
            {
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                foreach (var vRow in valCity)
                {
                    List<SqlParameter> vParameterListCL = new List<SqlParameter> {
                    new SqlParameter("@Id",vRow.IdCityLanguage),
                    new SqlParameter("@IdCity",vRow.Id!=Guid.Empty?vRow.Id:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@IdLanguage",vRow.IdLanguage!=Guid.Empty?vRow.IdLanguage:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Slogan",!string.IsNullOrEmpty(vRow.Slogan)?vRow.Slogan:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Description",!string.IsNullOrEmpty(vRow.Description)?vRow.Description:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Location",!string.IsNullOrEmpty(vRow.Location)?vRow.Location:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@FarmingProduction",!string.IsNullOrEmpty(vRow.FarmingProduction)?vRow.FarmingProduction:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@DescriptionDistricts",!string.IsNullOrEmpty(vRow.DescriptionDistricts)?vRow.DescriptionDistricts:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@State",vRow.State),
                    new SqlParameter("@UserUpdate",!string.IsNullOrEmpty(vRow.UserUpdate)?vRow.UserUpdate:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@DateUpdate",vRow.DateUpdate!=DateTime.MinValue?vRow.DateCreate:DateTime.Now),
                    };
                    bool vResponseCityLanguage = vSqlTools.ExecuteIUWithStoreProcedure(vParameterListCL, "sp_Update_Citylanguage", vConnection);
                    if (vResponseCityLanguage)
                    {
                        vResult = true;
                    }
                }

            }
            catch (Exception vEx)
            {
                string vMessage = vEx.Message;
            }

            return vResult;
        }

        public bool InsertarListaCityLanguage(List<City> valCity,Guid valIdCity) {
            bool vResult = false;
            try {
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                foreach (var vRow in valCity) {
                    List<SqlParameter> vParameterListCL = new List<SqlParameter> {
                    new SqlParameter("@Id",Guid.NewGuid()),
                    new SqlParameter("@IdCity",valIdCity!=Guid.Empty?valIdCity:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@IdLanguage",vRow.IdLanguage!=Guid.Empty?vRow.IdLanguage:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Slogan",!string.IsNullOrEmpty(vRow.Slogan)?vRow.Slogan:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Description",!string.IsNullOrEmpty(vRow.Description)?vRow.Description:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@Location",!string.IsNullOrEmpty(vRow.Location)?vRow.Location:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@FarmingProduction",!string.IsNullOrEmpty(vRow.FarmingProduction)?vRow.FarmingProduction:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@DescriptionDistricts",!string.IsNullOrEmpty(vRow.DescriptionDistricts)?vRow.DescriptionDistricts:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@State",vRow.State),
                    new SqlParameter("@UserCreate",!string.IsNullOrEmpty(vRow.UserCreate)?vRow.UserCreate:throw new Exception(GlobalValues.vTextExceptionParameterNull)),
                    new SqlParameter("@DateCreate",vRow.DateCreate!=DateTime.MinValue?vRow.DateCreate:DateTime.Now),
                    };
                    bool vResponseCityLanguage = vSqlTools.ExecuteIUWithStoreProcedure(vParameterListCL, "sp_Insert_CityLanguage", vConnection);
                    if (vResponseCityLanguage) {
                        vResult = true;
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
            }
            return vResult;
        }


        public List<City> DataTableToList(DataTable table) {

            List<City> vResult = new List<City>();
            try {
                foreach (DataRow row in table.Rows) {
                    City vCity = new City {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        Icon = Convert.ToString(row["Icon"]),
                        Temperature = row["Temperature"] == DBNull.Value ? 0 : Convert.ToInt16(row["Temperature"]),
                        Altitude = row["Altitude"] == DBNull.Value ? 0 : Convert.ToInt32(row["Altitude"]),
                        Population = row["Population"] == DBNull.Value ? 0 : Convert.ToInt32(row["Population"]),
                        UrlPhoto = row["UrlPhoto"] == DBNull.Value ? string.Empty : Convert.ToString(row["UrlPhoto"]),
                        Location = row["Location"] == DBNull.Value ? string.Empty : Convert.ToString(row["Location"]),
                        Position = row["Position"] == DBNull.Value ? 0 : Convert.ToInt32(row["Position"]),
                        State = row["State"] == DBNull.Value ? 0 : Convert.ToInt32(row["State"]),
                        IdCityLanguage = Guid.Parse(Convert.ToString(row["IdCityLanguage"])),
                        Slogan = row["Slogan"] == DBNull.Value ? string.Empty : Convert.ToString(row["Slogan"]),
                        Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]),
                        FarmingProduction = row["FarmingProduction"] == DBNull.Value ? string.Empty : Convert.ToString(row["FarmingProduction"]),
                        DescriptionDistricts = row["DescriptionDistricts"] == DBNull.Value ? string.Empty : Convert.ToString(row["DescriptionDistricts"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = row["UserUpdate"] == DBNull.Value ? string.Empty : Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                        IdLanguage = row["IdLanguage"] == DBNull.Value ? Guid.Empty : Guid.Parse(Convert.ToString(row["IdLanguage"]))
                    };
                    vResult.Add(vCity);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<City>();
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

        #endregion


    }
}