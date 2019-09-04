using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace GoTour.DataAccess.Implementation {
    public class ActivityData : IActivityData {
        public string vConnection = "Master";
       
        public string SelectByTour(ActivitySearch valFilter) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                if (valFilter.DateStart == DateTime.MinValue)
                    valFilter.DateStart = new DateTime(1753, 1, 1);
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valFilter.IdLanguage.ToString()),
                    new SqlParameter("@IdTour", valFilter.IdTour.ToString()),
                    new SqlParameter("@IdCurrency", valFilter.IdCurrency.ToString()),
                    new SqlParameter("@DateStart", valFilter.DateStart),
                    new SqlParameter("@MinimumPeople", valFilter.MinimumPeople), 
                    new SqlParameter("@RowsPerPage", valFilter.RowsPerPage),
                    new SqlParameter("@PageNumber", valFilter.PageNumber)
                };
                ActivityResponse vModel = new ActivityResponse();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Activity_Filter", vConnection);
                List<Activity> vData = DataTableToListFilter(vDatainTable);
                vModel.Activities = vData;
                vModel.TotalRows = GetTotalRowsFilter(valFilter);
                if (vModel.Activities != null & vModel.Activities.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vModel, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = string.Empty;
            }
            return vResult;
        }

        public string SelectActivityCompany(ActivityCompanySearch valFilter) {
            string vResult = string.Empty;
            ICompanyData vCompanyData = new CompanyData();
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                Activity vActivity = GetById(valFilter.IdActivity, valFilter.IdLanguage, valFilter.IdCurrency);

                Company vCompany = vCompanyData.SelectCompanyById(valFilter.IdCompany);
                ActivityCompany vData = new ActivityCompany();
                vData.Activity = vActivity;
                vData.Company = vCompany;
                vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = string.Empty;
            }
            return vResult;
        }

        private Activity GetById(Guid valIdActivity, Guid valIdLanguage, Guid valIdCurrency) {
            Activity vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valIdLanguage.ToString()),
                    new SqlParameter("@IdActivity", valIdActivity.ToString()),
                    new SqlParameter("@IdCurrency", valIdCurrency.ToString())
                };

                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Activity", vConnection);
                vResult = DataTableToElement(vDatainTable);
                
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new Activity();
            }
            return vResult;
        }

       
        #region Common
        private long GetTotalRowsFilter(ActivitySearch valFilter) {
            long vResult = 0;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                if (valFilter.DateStart == DateTime.MinValue)
                    valFilter.DateStart = new DateTime(1753, 1, 1);
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valFilter.IdLanguage.ToString()),
                    new SqlParameter("@IdTour", valFilter.IdTour.ToString()),
                    new SqlParameter("@IdCurrency", valFilter.IdCurrency.ToString()),
                    new SqlParameter("@DateStart", valFilter.DateStart),
                    new SqlParameter("@MinimumPeople", valFilter.MinimumPeople),
                    new SqlParameter("@RowsPerPage", valFilter.RowsPerPage),
                    new SqlParameter("@PageNumber", valFilter.PageNumber)
                };
                TourResponse vModel = new TourResponse();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Activity_Filter_Total", vConnection);
                vResult = DataTableToListFilterTotal(vDatainTable);
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
            }
            return vResult;
        }

        private long DataTableToListFilterTotal(DataTable valDatainTableCount) {
            long vResult = 0;
            try {
                foreach (DataRow vRow in valDatainTableCount.Rows) {
                    vResult = Convert.ToInt64(vRow["Total_Rows"]);
                };
            } catch (Exception) { }
            return vResult;
        }

        public Activity DataTableToElement(DataTable table) {
            Activity vResult = null;
            try {
                foreach (DataRow row in table.Rows) {
                    vResult = new Activity {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        StartPoint = Convert.ToString(row["StartPoint"]),
                        Itinerary = Convert.ToString(row["Itinerary"]),
                        Includes = Convert.ToString(row["Includes"]),
                        Note = Convert.ToString(row["Note"]),
                        Mount = Convert.ToDouble(row["Mount"]),
                        FreeCancelation = Convert.ToInt32(row["FreeCancelation"]),
                        Duration = Convert.ToInt32(row["Duration"]),
                        MinimumPeople = Convert.ToInt32(row["MinimumPeople"]),
                        SellTimeAdvance = Convert.ToInt32(row["SellTimeAdvance"]),
                        DateStart = Convert.ToDateTime(row["DateStart"]),
                        Score = Convert.ToInt32(row["Score"]),
                        Ranking = Convert.ToInt32(row["Ranking"]),
                        IdTour = Guid.Parse(Convert.ToString(row["IdTour"])),
                        IdCompany = Guid.Parse(Convert.ToString(row["IdCompany"])),
                        IdCurrency = Guid.Parse(Convert.ToString(row["IdCurrency"])),
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

        public List<Activity> DataTableToListFilter(DataTable table) {

            List<Activity> vResult = new List<Activity>();
            try {
                foreach (DataRow row in table.Rows) {
                    Activity vActivity = new Activity {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        StartPoint = Convert.ToString(row["StartPoint"]),
                        Itinerary = Convert.ToString(row["Itinerary"]),
                        Includes = Convert.ToString(row["Includes"]),
                        Note = Convert.ToString(row["Note"]),
                        Mount = Convert.ToDouble(row["Mount"]),
                        FreeCancelation = Convert.ToInt32(row["FreeCancelation"]),
                        Duration = Convert.ToInt32(row["Duration"]),
                        MinimumPeople = Convert.ToInt32(row["MinimumPeople"]),
                        SellTimeAdvance = Convert.ToInt32(row["SellTimeAdvance"]),
                        DateStart = Convert.ToDateTime(row["DateStart"]),
                        Score = Convert.ToInt32(row["Score"]),
                        Ranking = Convert.ToInt32(row["Ranking"]),
                        NameCompany = Convert.ToString(row["NameCompany"]),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        IdTour = Guid.Parse(Convert.ToString(row["IdTour"])),
                        IdCompany = Guid.Parse(Convert.ToString(row["IdCompany"])),
                        IdCurrency = Guid.Parse(Convert.ToString(row["IdCurrency"])),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue
                    };
                    vResult.Add(vActivity);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Activity>();
            }
            return vResult;
        }

        #endregion
    }

}
