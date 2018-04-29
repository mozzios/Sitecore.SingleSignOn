using Sitecore.SingleSignOn.DataAccess.ConnectionString;
using Sitecore.SingleSignOn.DataAccess.GenericClassHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.DataAccess.SqlHelper
{
    public class DataManager : IDisposable
    {
        private static SqlConnection _conn;
        public DataManager()
        {
            _conn = new SqlConnection(SqlConnectionString.ConnectionString);
            _conn.Open();
        }

        public SqlDataReader ExecuteReader(string query, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                foreach(SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }

                return command.ExecuteReader();
            }
        }

        public int ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }

                return command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
