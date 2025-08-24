using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;
        public DatabaseConnection()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")
                               ?? throw new InvalidOperationException("Connection string saknas i appsettings.json!");
        }
        public void TestConnection()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected Successfully");
                Console.WriteLine(_connectionString);
            }
        }

    }
}
