using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace MyShop.Database
{
    internal class SQLCustomer : ICustomerRepository
    {
        private readonly SQLQueryExecutor _executor;

        public SQLCustomer()
        {
            _executor = new SQLQueryExecutor();
        }
        public CustomerInfo FindCustomerById(Guid id)
        {
            Dictionary<String, String> queryParams = new Dictionary<String, String>()
            {
                ["@id"] = id.ToString()
            };
            DataSet data = _executor.RunStoredProcedureRead("FindCustomerById", queryParams);
            return GetCustomerInfoFromDataSet(data);
        }

        public CustomerInfo FindCustomerByName(String name)
        {
            Dictionary<String, String> queryParams = new Dictionary<String, String>()
            {
                ["@firstname"] = name
            };
            DataSet data = _executor.RunStoredProcedureRead("FindCustomerByName", queryParams);
            return GetCustomerInfoFromDataSet(data);
        }

        private CustomerInfo GetCustomerInfoFromDataSet(DataSet data)
        {
            if (data.Tables[0].Rows.Count == 0)
            {
                Console.WriteLine("There is not result from query");
                return null;
            }
            return new CustomerInfo()
            {
                Id = Guid.Parse(data.Tables[0].Rows[0]["id"].ToString()),
                FirstName = data.Tables[0].Rows[0]["first_name"].ToString(),
                LastName = data.Tables[0].Rows[0]["last_name"].ToString(),
                Address = data.Tables[0].Rows[0]["address"].ToString(),
                City = data.Tables[0].Rows[0]["city"].ToString(),
                Mail = data.Tables[0].Rows[0]["mail"].ToString(),
                Number = data.Tables[0].Rows[0]["number"].ToString(),
            };
        }

    }
}
