using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace API.Context
{
    public class MySqlContext
    {
        readonly string? _connectionString;

        public MySqlContext()
        {
            _connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
        }

        public async Task Init()
        {
            var randomNumber = new Random().Next(1, 1000);
            var sql = $"CREATE TABLE IF NOT EXISTS employees_{randomNumber} (employee_id INT, employee_name VARCHAR(50));";
            Console.WriteLine(sql);
            var connection = GetConnection();
            await connection.ExecuteAsync(sql);
        }

        public IDbConnection GetConnection()
        {
            Console.WriteLine(_connectionString);
            return new MySqlConnection(_connectionString);
        }
    }
}