using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Data
{
    public class DatabaseManager : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DatabaseManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LabDevConnection");
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
