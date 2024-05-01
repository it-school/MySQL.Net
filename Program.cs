using MySql.Data.MySqlClient;

using MySqlConnector;

namespace MySQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;database=world;user=test;password=test123";

            using (MySql.Data.MySqlClient.MySqlConnection connection = new (connectionString))
            {
                connection.Open();

                // Getting data from table
                string sql = "SELECT * FROM country";
                using (MySql.Data.MySqlClient.MySqlCommand command = new (sql, connection))
                {
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString("Name");
                            string continent = reader.GetString("Continent");
                            int population = reader.GetInt32("Population");

                            Console.WriteLine($"{name} ({continent}), population: {population}");
                        }
                    }
                }

                // Inserting data to table
                sql = "INSERT INTO country (Name, Continent, Population) VALUES (@name, @continent, @population)";
                using (MySql.Data.MySqlClient.MySqlCommand command = new (sql, connection))
                {
                    command.Parameters.AddWithValue("@name", "New Country");
                    command.Parameters.AddWithValue("@continent", "Antarctica");
                    command.Parameters.AddWithValue("@population", "99");

                    // command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }
    }
}
