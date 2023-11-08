using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace API.Contexts
{
    public class MySqlContext
    {
        readonly string? _host;
        
        readonly string? _port;

        readonly string? _database;

        readonly string? _user;

        readonly string? _password;

        public MySqlContext()
        {
            _host = Environment.GetEnvironmentVariable("MYSQL_HOST");
            _port = Environment.GetEnvironmentVariable("MYSQL_PORT");
            _database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            _user = Environment.GetEnvironmentVariable("MYSQL_USER");
            _password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
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
            var connectionString = $"Server={_host};Port={_port};Database={_database};Uid={_user};Pwd={_password}";
            Console.WriteLine(connectionString);
            return new MySqlConnection(connectionString);
        }

        public static string GetGlobalConnection()
        {
            var _host = Environment.GetEnvironmentVariable("MYSQL_HOST");
            var _port = Environment.GetEnvironmentVariable("MYSQL_PORT");
            var _database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var _user = Environment.GetEnvironmentVariable("MYSQL_USER");
            var _password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            var connectionString = $"server={_host}; port={_port}; database={_database}; uid={_user}; pwd={_password}";

            return connectionString;
        }
    }
}