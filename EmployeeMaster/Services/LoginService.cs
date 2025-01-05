using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.Model;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace EmployeeMaster.Services
{
    public class LoginService
    {
        private readonly string connectionString;

        public LoginService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
        }

        public UserModel Login(string username, string password)
        {
            UserModel user = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT idUser, username, password, email, ime, lastName
                    FROM User
                    WHERE username = @Username AND password = @Password";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Preporučuje se hashiranje lozinki!

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            IdUser = reader.GetInt32("idUser"),
                            FullName = reader.GetString("ime") + " " + reader.GetString("lastname"),
                            Username = reader.GetString("username"),
                            Email = reader.GetString("Email"),
                         
                        };
                    }
                }
            }

            return user; 
        }
    }
}

