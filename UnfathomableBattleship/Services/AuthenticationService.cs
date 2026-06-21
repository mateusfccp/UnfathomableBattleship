using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using UnfathomableBattleship.Exceptions;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Services
{
    public class AuthenticationService(string connectionString) : IAuthenticationService
    {
        private readonly string _connectionString = connectionString;

        public void CreateUser(string username, string password)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            if (CheckUserExists(connection, username))
            {
                throw new RegistrationFailedException("User name already exits");
            }
            string passwordHash = HashPassword(password);
            string insertQuery = "INSERT INTO User (user_name, password_hash) VALUES (@username, @passwordHash)";
            using var command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@passwordHash", passwordHash);
            command.ExecuteNonQuery();
        }

        public IGameManager Login(string username, string password)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            string query = "SELECT user_id, password_hash FROM User WHERE user_name = @username";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);

                using var reader = command.ExecuteReader();
                if (reader.Read())// If Read() is true, the user exists
                {
                    string storedHash = reader.GetString(1);

                    if (HashPassword(password) == storedHash)
                    {
                        int userId = reader.GetInt32(0);
                        return new GameManager(_connectionString, userId);
                    }
                }
            }
            throw new AuthenticationFailedException();
        }


        private static bool CheckUserExists(SQLiteConnection connection, string userInput)
        {
            var command = new SQLiteCommand("SELECT EXISTS(SELECT 1 FROM User WHERE user_name = @user_input)", connection);
            command.Parameters.AddWithValue("@user_input", userInput);  
            var result = command.ExecuteScalar();
            return Convert.ToBoolean(result);
        }


        /// <summary>
        /// Takes a string and creates a hash with it.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
