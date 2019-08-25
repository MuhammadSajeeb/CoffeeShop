using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Persistancis.Repositories
{
    public class MainRepository
    {
        public string ConnectionString()
        {
            string connectionString =
                @"Data Source=localhost;Initial Catalog=CoffeShopDb;Integrated Security=True";
            return connectionString;
        }

        public int ExecuteNonQuery(string query, string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();

                var command = new SqlCommand(query, connection);
                int success = command.ExecuteNonQuery();
                connection.Close();
                return success;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public decimal ExecuteScalar(string query, string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();

                var command = new SqlCommand(query, connection);
                object success = command.ExecuteScalar();
                connection.Close();
                return Convert.ToDecimal(success);
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public SqlDataReader Reader(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }
    }
}
