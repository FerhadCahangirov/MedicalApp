using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public class DBHelper
    {

        private static string constr = ConfigurationManager.ConnectionStrings["MedicalApp.Properties.Settings.MEDICALDBConnectionString"].ConnectionString;

        public static SqlDataReader ExecuteReader(string sqlstr, CommandType commandType, SqlParameter[] parameters = null)
        {
            SqlConnection sqlConnection = new SqlConnection(constr);
            SqlCommand sqlCommand = new SqlCommand(sqlstr, sqlConnection);
            sqlCommand.CommandType = commandType;

            SqlDataReader reader = null;

            if (commandType == CommandType.StoredProcedure)
            {
                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters);
                }
            }

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                throw ex;
            }

            return reader;

        }

        public static int ExecuteNonQuery(string sqlstr, CommandType commandType, SqlParameter[] parameters = null)
        {

            SqlConnection sqlConnection = new SqlConnection(constr);
            SqlCommand sqlCommand = new SqlCommand(sqlstr, sqlConnection)
            {
                CommandType = commandType
            };

            if (commandType == CommandType.StoredProcedure)
                if (parameters != null)
                    sqlCommand.Parameters.AddRange(parameters);

            int numRows = 0;

            try
            {
                sqlConnection.Open();
                numRows = sqlCommand.ExecuteNonQuery();
            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }

            return numRows;
        }

        public static object ExecuteScalar(string sqlstr, CommandType commandType, SqlParameter[] parameters = null)
        {
            SqlConnection sqlConnection = new SqlConnection(constr);
            SqlCommand sqlCommand = new SqlCommand(sqlstr, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            if (sqlCommand.CommandType == CommandType.StoredProcedure)
            {
                if(parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters);
                }
            }

            object result = null;

            try
            {
                sqlConnection.Open();
                result = sqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }

            return result;

        }




    }
}
