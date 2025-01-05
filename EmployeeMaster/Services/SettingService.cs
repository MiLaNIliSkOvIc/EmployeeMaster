using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace EmployeeMaster.Services
{
    public class SettingService
    {
        private readonly string connectionString;

        public SettingService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
        }
        public SettingModel GetSettingsByUserId(int userId)
        {
            SettingModel setting = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT theme, language FROM Setting WHERE User_idUser = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        setting = new SettingModel
                        {
                            Theme = reader.GetString("theme"),
                            Language = reader.GetString("language"),
                          
                        };
                    }
                }
            }

            return setting;
        }


        public void AddSetting(SettingModel setting)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "INSERT INTO Setting (theme, language, User_idUser) VALUES (@Theme, @Language, @UserId)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Theme", setting.Theme);
                command.Parameters.AddWithValue("@Language", setting.Language);
                command.Parameters.AddWithValue("@UserId", setting.UserId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

       
        public void UpdateSettings(int userId, SettingModel setting)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "UPDATE Setting SET theme = @Theme, language = @Language WHERE User_idUser = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Theme", setting.Theme);
                command.Parameters.AddWithValue("@Language", setting.Language);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSetting(int userId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "DELETE FROM Setting WHERE User_idUser = @UserId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
