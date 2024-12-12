using Microsoft.Data.SqlClient;  // Use the correct namespace
using System;

class Program
{
    static void Main(string[] args)
    {
        // Connection string to your SQL Server database
        string connectionString = "Server=DESKTOP-5UHRIGP\\SQLEXPRESS;Database=WorkspaceAutomator;Integrated Security=True;Encrypt=False;";

        // Establish connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connected to database!");

            // Example: Display data from the Environments table
            DisplayEnvironments(connection);

            // Populate the database with test data
            //PopulateTestData(connection);
        }
    }

    static void DisplayEnvironments(SqlConnection connection)
    {
        string query = "SELECT * FROM Environments";

        using (SqlCommand command = new SqlCommand(query, connection))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}");
            }
        }
    }

    static void PopulateTestData(SqlConnection connection)
    {
        // Add 30 environments as test data
        for (int i = 1; i <= 30; i++)
        {
            string insertEnv = "INSERT INTO Environments (Name, Description) VALUES (@Name, @Description)";
            using (SqlCommand command = new SqlCommand(insertEnv, connection))
            {
                command.Parameters.AddWithValue("@Name", $"Environment {i}");
                command.Parameters.AddWithValue("@Description", $"Description for Environment {i}");
                command.ExecuteNonQuery();
            }
        }

        // Add 50 applications as test data
        for (int i = 1; i <= 50; i++)
        {
            string insertApp = "INSERT INTO Applications (Name, Path) VALUES (@Name, @Path)";
            using (SqlCommand command = new SqlCommand(insertApp, connection))
            {
                command.Parameters.AddWithValue("@Name", $"App {i}");
                command.Parameters.AddWithValue("@Path", $"C:\\ProgramFiles\\App{i}.exe");
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Test data inserted!");
    }
}
