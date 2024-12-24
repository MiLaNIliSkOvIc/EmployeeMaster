using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMaster.ViewModel;
using MySql.Data.MySqlClient;
using System;
using EmployeeMaster.Model;



namespace EmployeeMaster.Services
{
    public class UserService
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";

        public UserModel GetUserInfo(int userId)
        {
            UserModel user = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT idUser, ime, lastname, username, Email, phone, Picture FROM User WHERE idUser = @UserId";
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
                            Picture = reader.GetString("Picture")
                        };
                    }
                }
            }

            return user;
        }
    }
}
