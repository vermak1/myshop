using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyShop.Database
{
    internal class SQLCustomer : ICustomerRepository
    {
        private readonly SQLQueryExecutor _executor;

        public SQLCustomer()
        {
            _executor = new SQLQueryExecutor();
        }
        public async Task<CustomerInfo> FindCustomerByIdAsync(Guid id)
        {
            using (var connection = await SQLFactory.GetConnectionAsync())
            {
                Dictionary<String, String> queryParams = new Dictionary<String, String>()
                {
                    ["@id"] = id.ToString()
                };
                DataSet data = await _executor.RunStoredProcedureReadAsync("FindCustomerById", queryParams, connection);
                var userInfo = ConvertToCustomerInfo(data);
                if (userInfo == null)
                    Console.WriteLine("There is no user with id {0}", id);
                return userInfo;
            }
        }

        public async Task<CustomerInfo> FindCustomerByNameAsync(String firstName, String lastName)
        {
            using (var connection = await SQLFactory.GetConnectionAsync()) 
            {
                Dictionary<String, String> queryParams = new Dictionary<String, String>()
                {
                    ["@firstname"] = firstName,
                    ["@lastname"] = lastName
                };
                DataSet data = await _executor.RunStoredProcedureReadAsync("FindCustomerByName", queryParams, connection);
                var userInfo = ConvertToCustomerInfo(data);
                if (userInfo == null)
                    Console.WriteLine("There is no user with first name {0} and last name {1}", firstName, lastName);
                return userInfo;
            }
        }

        private CustomerInfo ConvertToCustomerInfo(DataSet data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Tables[0].Rows.Count == 0)
            {
                Console.WriteLine("There is not user returned from DB");
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
