using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System;
using EmployeeMaster.Model;
using System.Configuration;


namespace EmployeeMaster.Services
{
    public class UserService
    {
        private readonly string connectionString;

        public UserService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
        }
        public UserModel GetUserInfo(int userId)
        {
            UserModel user = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT idUser, ime, lastname, username, Email, phone, Picture, dateOfBirth FROM User WHERE idUser = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

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
                            Phone = reader.GetString("phone"),
                            Picture = reader.GetString("Picture"),
                            dateOfBirth = reader.GetDateTime("dateOfBirth").ToString("yyyy-MM-dd")


                        };
                    }
                }
            }

            return user;
        }
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT idUser, ime, lastname, username, Email, phone, Picture FROM User";
                var command = new MySqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            IdUser = reader.GetInt32("idUser"),
                            FullName = reader.GetString("ime") + " " + reader.GetString("lastname"),
                            Username = reader.GetString("username"),
                            Email = reader.GetString("Email"),
                            Phone = reader.GetString("phone"),
                            Picture = reader.GetString("Picture")
                        });
                    }
                }
            }

            return users;
        }
    }
}
