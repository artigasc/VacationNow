using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GoTour.DataAccess.Implementation
{
    public class GeneralSearchData : IGeneralSearchData {
        
        string vConnection = "Master";
        public string SearchText(GeneralSearch valSearchText)
        {
            string vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();

            try
            {
                List<SqlParameter> vParameterList = new List<SqlParameter> {
                    new SqlParameter("@IdLanguage", valSearchText.IdLanguage.ToString()),
                    new SqlParameter("@SearchText", valSearchText.SearchText.ToString())
                };

                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_SearchText", vConnection);
                List<GeneralResult> vData = DataTableToElement(vDatainTable);

                if (vData != null)
                {
                    vResult = JsonConvert.SerializeObject(vData, Formatting.Indented);
                }
            }
            catch (Exception vEx)
            {
                string vMessage = vEx.Message;
                vResult = null;
            }
            //var vDes = JsonConvert.DeserializeObject<List<City>>(vResult);
            return vResult;
        }


        public List<GeneralResult> DataTableToElement(DataTable table)
        {

            List<GeneralResult> vResult = new  List<GeneralResult>();
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    GeneralResult vgeneralResult=new GeneralResult
                    {
                        IdCityTour = Guid.Parse(Convert.ToString(row["id"])),
                        IdCity=Guid.Parse(Convert.ToString(row["idcity"])),
                        NameCityTour = Convert.ToString(row["name"]),                        
                        IconCityTour = Convert.ToString(row["icon"]),
                        Ordering = Convert.ToInt32(row["ordering"])
                    };
                    vResult.Add(vgeneralResult);
                }
            }
            catch (Exception vEx)
            {
                string vMessage = vEx.Message;
                vResult = new List<GeneralResult>();
            }
            return vResult;
        }

    }
}