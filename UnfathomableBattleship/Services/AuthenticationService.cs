using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using UnfathomableBattleship.Exceptions;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _connectionString;
        public AuthenticationService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void CreateUser(string username, string password)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                if(CheckUserExists(connection, username))
                {
                    throw new RegistrationFailedException("User name already exits");
                }
                string passwordHash = HashPassword(password);
                string insertQuery = "INSERT INTO User (user_name, password_hash) VALUES (@username, @passwordHash)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    command.ExecuteNonQuery();
                }
            }

                
        }

        public IGameManager Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        private bool CheckUserExists(SQLiteConnection connection, string userInput)
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
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
