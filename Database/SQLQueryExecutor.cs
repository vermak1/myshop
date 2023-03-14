using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


//1) ЗАВЕСТИ ДЛЛЬКУ(СБОРКУ) С ХРАНИЛИЩЕМ КОМАНД (СПЕК) - общий интерфейс для клиента и сервера
//2) ЭНАМ ДЛЯ КОМАНД ЗАВЕСТИ В ЭТОЙ ДЛЛЬКЕ
//3) проверки добавить на null и т.д. в публичных методах - хорошее правило добавлять это в публичные методы, которые будут использовать другие клиенты, в private которые используются точечно это необязательно
//4) как правило транзакции нужны когда несколько запросов с разными таблицами
//5) Обертка для транзакций (возможно интерфейс) - нужно обернуть публичными методами, чтобы пользователь класса сам определял границы транзакции
//6) Пул коннекций к базе - щас сделано странно, из любой точки можно вызвать открытие\закрытие коннекций - лучше бы сделать единную точку
//7) Паралелльность, нужно запросы к базе обрабатывать параллельно

namespace MyShop.Database
{
    internal class SQLQueryExecutor : IDisposable
    {
        private readonly SQLContext _context;

        public SQLQueryExecutor()
        {
            _context = new SQLContext();
        }

        private DataSet ExecuteQueryWithTransaction(string spName, List<SqlParameter> spParams)
        {
            _context.OpenConnection();
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand(spName, _context.DatabaseConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Transaction = _context.DatabaseConnection.BeginTransaction();

            foreach (var p in spParams)
                command.Parameters.AddWithValue(p.ParameterName, p.Value);

            try
            {
                command.ExecuteNonQuery();
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                command.Transaction.Commit();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Transaction has been failed, exception type: {0}\nError: {1}", ex.GetType(), ex.Message);
                command.Transaction.Rollback();
                throw;
            }
            return ds;
        }

        public DataSet RunStoredProcedureRead(String procedureName, Dictionary<String, String> queryParams)
        {
            //null verifications
            //String.IsNullOrEmpty()
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var p in queryParams)
            {
                parameters.Add(new SqlParameter() { ParameterName = p.Key, Value = p.Value });
            }

            DataSet data = null;
            try
            {
                data = ExecuteQueryWithTransaction(procedureName, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error within query execution: {0}", ex.Message);
            }
            
            return data;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
