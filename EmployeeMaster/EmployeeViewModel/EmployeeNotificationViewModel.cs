using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.Model;
using EmployeeMaster.Services;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EmployeeMaster.ViewModel
{
    public class NotificationViewModel 
    {
        private readonly NotificationService _notificationService;
        private ObservableCollection<NotificationModel> _notifications;

        public ObservableCollection<NotificationModel> Notifications = new ObservableCollection<NotificationModel>();
        public List<string> NotificationsString = new List<string>();



        public NotificationViewModel()
        {
            _notificationService = new NotificationService();
            Notifications = new ObservableCollection<NotificationModel>();
            LoadNotifications();
        }

        public void LoadNotifications()
        {
            try
            {
                var notificationsList = _notificationService.GetNotifications();
                Notifications = new ObservableCollection<NotificationModel>(notificationsList);
               NotificationsString = Notifications.Select(notification => notification.Content).ToList();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju obavještenja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddNotification(string content, int employeeId)
        {
            try
            {
                var newNotification = new NotificationModel
                {
                    Content = content,
                    EmployeeId = employeeId
                };

                _notificationService.AddNotification(newNotification);
                LoadNotifications();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dodavanju obavještenja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteNotification(NotificationModel notification)
        {
            try
            {
                _notificationService.DeleteNotification(notification.Id);
                LoadNotifications();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri brisanju obavještenja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    
    }
}


