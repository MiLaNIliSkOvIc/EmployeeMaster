using EmployeeMaster.Model;
using EmployeeMaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeMaster.AdministratorViewModel
{
    public class NotificationViewModel
    {
        private readonly NotificationService _notificationService;

        public ObservableCollection<NotificationModel> Notifications { get; set; }
        public string NewNotificationContent { get; set; }

        public NotificationViewModel()
        {
            _notificationService = new NotificationService();
            Notifications = new ObservableCollection<NotificationModel>(_notificationService.GetNotifications());
        }

        public void AddNotification()
        {
            if (!string.IsNullOrEmpty(NewNotificationContent))
            {
                var newNotification = new NotificationModel
                {
                    Content = NewNotificationContent,
                    EmployeeId = 1 // Zamijenite stvarnim EmployeeId
                };

                _notificationService.AddNotification(newNotification);
                Notifications.Add(newNotification);
                NewNotificationContent = string.Empty; 
            }
            else
            {
                MessageBox.Show("Unesite tekst obavještenja prije dodavanja.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void DeleteNotification(NotificationModel notification)
        {
            if (notification != null)
            {
                _notificationService.DeleteNotification(notification.Id);
                Notifications.Remove(notification);
            }
            else
            {
                MessageBox.Show("Odaberite obavještenje za brisanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
