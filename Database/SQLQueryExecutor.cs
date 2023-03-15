using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace MyShop.Database
{
    internal class SQLQueryExecutor
    {

        private async Task<DataSet> ExecuteQueryAsync(string spName, List<SqlParameter> spParams, SqlConnection connection)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var p in spParams)
                command.Parameters.AddWithValue(p.ParameterName, p.Value);
            try
            {
                await command.ExecuteNonQueryAsync();
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Query execution has been failed, exception type: {0}\nError: {1}", ex.GetType(), ex.Message);
                throw;
            }
            return ds;
        }

        public async Task<DataSet> RunStoredProcedureReadAsync(String procedureName, Dictionary<String, String> queryParams, SqlConnection connection)
        {
            if (queryParams == null)
                throw new ArgumentNullException(nameof(queryParams));
            if (String.IsNullOrEmpty(procedureName))
                throw new ArgumentException(nameof(procedureName));
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var p in queryParams)
            {
                parameters.Add(new SqlParameter() { ParameterName = p.Key, Value = p.Value });
            }

            DataSet data = null;
            try
            {
                data = await ExecuteQueryAsync(procedureName, parameters, connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error within query execution: {0}", ex.Message);
            }
            
            return data;
        }
    }
}
