using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GoTour.Helper {
    public class SQLToolsLibrary {

        private string ConnectionStrings(string valCS) {
            string connection = ConfigurationManager.ConnectionStrings[valCS].ToString();
            return ConfigurationManager.ConnectionStrings[valCS].ToString();
        }

        public DataTable GetResultSql(List<SqlParameter> valParameterList, string valSql, string valCs) {
            DataTable vResult = new DataTable();
            SqlConnection vConn = new SqlConnection(ConnectionStrings(valCs));
            SqlDataReader vReader;
            vConn.Open();
            SqlCommand vComm = new SqlCommand(valSql, vConn);
            foreach (SqlParameter vData in valParameterList) {
                vComm.Parameters.Add(vData);
            }
            vReader = vComm.ExecuteReader();
            vResult.Load(vReader);
            vReader.Close();
            vConn.Close();
            return vResult;
        }

        public bool ExecuteQuery(string valSql, string valCS) {
            bool vResult = false;
            SqlConnection vConn = new SqlConnection(ConnectionStrings(valCS));
            vConn.Open();
            SqlCommand vComm = new SqlCommand(valSql, vConn);
            vResult = (vComm.ExecuteNonQuery() > 0);
            vConn.Close();
            return vResult;
        }

        public DataTable GetResultSql(string valSql, string valCS) {
            DataTable vResult = new DataTable();
            SqlConnection vConn = new SqlConnection(ConnectionStrings(valCS));
            SqlDataReader vReader;
            vConn.Open();
            SqlCommand vComm = new SqlCommand(valSql, vConn);
            vReader = vComm.ExecuteReader();
            vResult.Load(vReader);
            vReader.Close();
            vConn.Close();
            return vResult;
        }

        public SqlParameter ParameterString(string valName, string valValue, int valSize) {
            SqlParameter vResult = new SqlParameter();
            vResult = GetParameter(valName, SqlDbType.NVarChar, valValue, valSize);
            return vResult;
        }

        public SqlParameter GetParameter(string valName, SqlDbType valSqlDbType, object valValue, int valSize) {
            SqlParameter vResult = new SqlParameter {
                ParameterName = "@" + valName,
                SqlDbType = valSqlDbType,
                Direction = ParameterDirection.Input,
                Value = valValue
            };
            if ((valSqlDbType != SqlDbType.DateTime) || (valSqlDbType != SqlDbType.UniqueIdentifier)) {
                vResult.Size = valSize;
            }
            return vResult;
        }

        public DataTable ExcecuteSelectWithStoreProcedure(List<SqlParameter> valParameterList, string valStoreProcedureName, string valCS) {
            DataTable vResult = new DataTable();
            SqlConnection vConn = new SqlConnection(ConnectionStrings(valCS));
            SqlCommand vComm = new SqlCommand(valStoreProcedureName, vConn) {
                CommandType = CommandType.StoredProcedure
            };

            if (valParameterList != null && valParameterList.Count() > 0) {

                foreach (SqlParameter vData in valParameterList) {
                    vComm.Parameters.Add(vData);
                }
            }
            vConn.Open();
            vResult.Load(vComm.ExecuteReader());

            vConn.Close();
            return vResult;
        }

        public bool ExecuteIUWithStoreProcedure(List<SqlParameter> valParameterList, string valStoreProcedureName, string valCS) {
            bool vResult = false;
            SqlConnection vConn = new SqlConnection(ConnectionStrings(valCS));
            SqlCommand vComm = new SqlCommand(valStoreProcedureName, vConn) {
                CommandType = CommandType.StoredProcedure
            };
            if (valParameterList != null && valParameterList.Count() > 0) {

                foreach (SqlParameter vData in valParameterList) {
                    vComm.Parameters.Add(vData);
                }
            }
            vConn.Open();
            vResult = (vComm.ExecuteNonQuery() > 0);
            vConn.Close();
            return vResult;
        }
    }
}