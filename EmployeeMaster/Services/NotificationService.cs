using EmployeeMaster.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Services
{
    public class NotificationService
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";

        public List<NotificationModel> GetNotifications()
        {
            var notifications = new List<NotificationModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT idNotification, content, Employee_User_idUser FROM Notification";
                var command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notifications.Add(new NotificationModel
                        {
                            Id = reader.GetInt32("idNotification"),
                            Content = reader.GetString("content"),
                            EmployeeId = reader.GetInt32("Employee_User_idUser")
                        });
                    }
                }
            }

            return notifications;
        }

        public void AddNotification(NotificationModel notification)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "INSERT INTO Notification (content, Employee_User_idUser) VALUES (@Content, @EmployeeId)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Content", notification.Content);
                command.Parameters.AddWithValue("@EmployeeId", notification.EmployeeId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteNotification(int notificationId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "DELETE FROM Notification WHERE idNotification = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", notificationId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
