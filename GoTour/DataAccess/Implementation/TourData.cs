using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace GoTour.DataAccess.Implementation {
    public class TourData : ITourData {
        public string vConnection = "Master";
        public string SelectAll() {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {

                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(null, "sp_Select_Tour_All", vConnection);
                List<Tour> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = string.Empty;
                vResult = JsonConvert.SerializeObject(new List<Tour>(), Formatting.Indented);
            }
            return vResult;

        }

        public string SelectByCity(TourSearch valFilter) {
            string vResult = string.Empty;
            try {
                DataTable vDatainTable = new DataTable();
                SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
                if (valFilter.Categories == null)
                    valFilter.Categories = new string[0];
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valFilter.IdLanguage.ToString()),
                    new SqlParameter("@IdCity", valFilter.IdCity.ToString()),
                    new SqlParameter("@IdCurrency", valFilter.IdCurrency.ToString()),
                    new SqlParameter("@Categories", valFilter.Categories.Length > 0 ? string.Join(",", valFilter.Categories) : null),
                    new SqlParameter("@MinPrice", valFilter.MinPrice), 
                    new SqlParameter("@MaxPrice", valFilter.MaxPrice),
                    new SqlParameter("@MinDuration", valFilter.MinDuration),
                    new SqlParameter("@MaxDuration", valFilter.MaxDuration),
                    new SqlParameter("@MinScore", valFilter.MinScore),
                    new SqlParameter("@MaxScore", valFilter.MaxScore),
                    new SqlParameter("@RowsPerPage", valFilter.RowsPerPage),
                    new SqlParameter("@PageNumber", valFilter.PageNumber)
                };
                TourResponse vModel = new TourResponse();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Tour_Filter", vConnection);
                List<Tour> vDataTours = DataTableToListFilter(vDatainTable);
                vModel.Tours = vDataTours;
                vModel.TotalRows = GetTotalRowsFilter(valFilter);
                if (vModel != null && vModel.Tours != null && vModel.Tours.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vModel, Formatting.Indented);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = string.Empty;
            }
            return vResult;
        }

        private long GetTotalRowsFilter(TourSearch valFilter) {
            long vResult = 0;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                if (valFilter.Categories == null)
                    valFilter.Categories = new string[0];
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valFilter.IdLanguage.ToString()),
                    new SqlParameter("@IdCity", valFilter.IdCity.ToString()),
                    new SqlParameter("@IdCurrency", valFilter.IdCurrency.ToString()),
                    new SqlParameter("@Categories", valFilter.Categories.Length > 0 ? string.Join(",", valFilter.Categories) : null),
                    new SqlParameter("@MinPrice", valFilter.MinPrice),
                    new SqlParameter("@MaxPrice", valFilter.MaxPrice),
                    new SqlParameter("@MinDuration", valFilter.MinDuration),
                    new SqlParameter("@MaxDuration", valFilter.MaxDuration),
                    new SqlParameter("@MinScore", valFilter.MinScore),
                    new SqlParameter("@MaxScore", valFilter.MaxScore)
                };
                TourResponse vModel = new TourResponse();
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Tour_Filter_Total", vConnection);
                vResult = DataTableToListFilterTotal(vDatainTable);
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
            }
            return vResult;
        }

        #region Common 
        public List<Tour> DataTableToList(DataTable table) {
            List<Tour> vResult = new List<Tour>();
            try {
                foreach (DataRow row in table.Rows) {
                    Tour vTour = new Tour {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdCity = Guid.Parse(Convert.ToString(row["IdCity"])),
                        IdCategory = Guid.Parse(Convert.ToString(row["IdCategory"])),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        Score = Convert.ToInt32(row["Score"]),
                        Ranking = Convert.ToInt32(row["Ranking"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"])

                    };
                    vResult.Add(vTour);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Tour>();
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

        public string OrderByRanking(Guid valIdLanguaje) {
            string vResult = string.Empty;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valIdLanguaje)
                };
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Tour_Language", vConnection);
                List<Tour> vData = DataTableToListRankingTour(vDatainTable);
                if (vData != null && vData.Count > 0) {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            } catch (Exception) {
                vResult = string.Empty;
            }
            return vResult;
        }

        public List<Tour> DataTableToListFilter(DataTable table) {

            List<Tour> vResult = new List<Tour>();
            try {
                foreach (DataRow row in table.Rows) {
                    Tour vTour = new Tour {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdCity = Guid.Parse(Convert.ToString(row["IdCity"])),
                        IdCategory = Guid.Parse(Convert.ToString(row["IdCategory"])),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        Ranking = Convert.ToInt32(row["Ranking"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"]),
                        AveragePrice = Convert.ToDouble(row["AveragePrice"]),
                        AverageRanking = Convert.ToInt32(row["AverageRanking"])
                    };
                    vResult.Add(vTour);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Tour>();
            }
            return vResult;
        }

        public List<Tour> DataTableToListRankingTour(DataTable table) {

            List<Tour> vResult = new List<Tour>();
            try {
                foreach (DataRow row in table.Rows) {
                    Tour vTour = new Tour {
                        Id = Guid.Parse(Convert.ToString(row["Id"])),
                        IdCity = Guid.Parse(Convert.ToString(row["IdCity"])),
                        IdCategory = Guid.Parse(Convert.ToString(row["IdCategory"])),
                        UrlPhoto = Convert.ToString(row["UrlPhoto"]),
                        Ranking = Convert.ToInt32(row["Ranking"]),
                        State = Convert.ToInt16(row["State"]),
                        UserCreate = Convert.ToString(row["UserCreate"]),
                        DateCreate = Convert.ToDateTime(row["DateCreate"]),
                        UserUpdate = Convert.ToString(row["UserUpdate"]),
                        DateUpdate = Convert.ToString(row["DateUpdate"]) != string.Empty ? Convert.ToDateTime(row["DateUpdate"]) : DateTime.MinValue,
                        Name = Convert.ToString(row["Name"]),
                        Description = Convert.ToString(row["Description"])

                    };
                    vResult.Add(vTour);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<Tour>();
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
        #endregion
    }
}
