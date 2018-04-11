using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PracticeGIT.Services
{
    public static class DatabaseHelper
    {
        private static SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["GemInYou"].ToString();
            return new SqlConnection(conString);

        }

        public static void ExecuteNonQuery(SqlCommand sqlCommand)
        {
            using (var conn = GetConnection())
            {
                sqlCommand.Connection = conn;
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static IReadOnlyList<T> GetDataAsList<T>(SqlCommand sqlCommand, Func<IDataRecord, T> itemReaderFunc)
        {
            List<T> dataList = new List<T>();
            using (var conn = GetConnection())
            {
                sqlCommand.Connection = conn;
                conn.Open();
                var dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    dataList.Add(itemReaderFunc(dataReader));
                }
                dataReader.Close();
                conn.Close();
            }
            return dataList;
        }
    }
}