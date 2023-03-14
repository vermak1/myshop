using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MyShop.Database
{
    internal class SQLContext : IDisposable
    {
        public SqlConnection DatabaseConnection { get; }
        public SQLContext()
        {
            DatabaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shop1"].ConnectionString);
        }

        public void OpenConnection()
        {
            if (DatabaseConnection.State == ConnectionState.Closed)
                DatabaseConnection.Open();
        }

        public void Dispose()
        {
            DatabaseConnection?.Dispose();
        }

    }
}
