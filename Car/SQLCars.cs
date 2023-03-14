using System;
using System.Collections.Generic;
using System.Data;
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

        public List<CarInfo> ListCars()
        {
            DataSet data = _executor.RunStoredProcedureRead("GetAllCars", new Dictionary<string, string>());
            List<CarInfo> list = GetCarsInfo(data);

            return list;
        }

        private List<CarInfo> GetCarsInfo(DataSet data)
        {
            List<CarInfo> list = new List<CarInfo>();
            if (data.Tables[0].Rows.Count == 0)
            {
                Console.WriteLine("There is not result from query");
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
