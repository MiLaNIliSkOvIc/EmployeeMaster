using EmployeeMaster.Model;
using EmployeeMaster.NotificationDisplay;
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
                    EmployeeId = CurrentUser.Instance.IdUser

                };

                _notificationService.AddNotification(newNotification);
                Notifications.Add(newNotification);
                NewNotificationContent = string.Empty; 
            }
            else
            {
                NotificationWindow err = new NotificationWindow("Unesite tekst obavještenja prije dodavanja.");
                err.Show();
            }
           
        }

        public void DeleteNotification(NotificationModel notification)
        {
            if (notification != null)
            {
                _notificationService.DeleteNotification(notification.Id);
                Notifications.Remove(notification);
            }
           
        }
    }
}
