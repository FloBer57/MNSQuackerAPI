using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace QuackerMNS.Database
{
    public class DatabaseConnection
    {
        public string ConnectionString { get; set; }

        public DatabaseConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object>? parameters = null)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                DataTable dataTable = new DataTable();
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                return dataTable;
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}