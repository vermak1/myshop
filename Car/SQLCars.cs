using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MyShop.Database;

namespace MyShop
{
    internal class SQLCars : ICarRepository
    {
        private readonly SQLQueryExecutor _executor;

        public SQLCars()
        {
            _executor = new SQLQueryExecutor();
        }

        public async Task<List<CarInfo>> ListCarsAsync()
        {
            using (var connection = await SQLFactory.GetConnectionAsync())
            {
                DataSet data = await _executor.RunStoredProcedureReadAsync("ListAllCars", new Dictionary<string, string>(), connection);
                List<CarInfo> list = ConvertToCarInfo(data);

                if(list.Count == 0)
                    Console.WriteLine("There are not cars available");
                return list;
            }
        }

        private List<CarInfo> ConvertToCarInfo(DataSet data)
        {
            List<CarInfo> list = new List<CarInfo>();
            if (data.Tables[0].Rows.Count == 0)
            {
                Console.WriteLine("There is not result parsed from DataSet");
                return list;
            }

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                list.Add(new CarInfo()
                {
                    Id = Guid.Parse(data.Tables[0].Rows[i]["id"].ToString()),
                    Brand = data.Tables[0].Rows[i]["brand"].ToString(),
                    Model = data.Tables[0].Rows[i]["model"].ToString(),
                    Color = data.Tables[0].Rows[i]["color"].ToString(),
                    Country = data.Tables[0].Rows[i]["country"].ToString(),
                    Year = Int32.Parse(data.Tables[0].Rows[i]["year"].ToString())
                });
            }
            return list;
        }
    }
}
