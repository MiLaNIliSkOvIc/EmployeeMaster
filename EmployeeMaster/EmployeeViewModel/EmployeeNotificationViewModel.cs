using EmployeeMaster.Model;
using EmployeeMaster.NotificationDisplay;
using EmployeeMaster.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EmployeeMaster.EmployeeViewModel
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private readonly NotificationService _notificationService;
       

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<NotificationModel> Notifications { get; set; } = new ObservableCollection<NotificationModel>();

        public List<string> NotificationsString { get; set; } = new List<string>();

        

        public NotificationViewModel()
        {
            _notificationService = new NotificationService();
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
                NotificationWindow notif = new NotificationWindow($"Greška pri učitavanju obavještenja: {ex.Message}");
                notif.Show();
               
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
