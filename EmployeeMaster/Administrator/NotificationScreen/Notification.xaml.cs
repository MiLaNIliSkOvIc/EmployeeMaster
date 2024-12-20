using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace EmployeeMaster.Administrator.NotificationScreen
{
    public partial class Notification : UserControl
    {
        public ObservableCollection<string> Notifications { get; set; } = new ObservableCollection<string>();

        public Notification()
        {
            InitializeComponent();
            NotificationList.ItemsSource = Notifications;
        }

        private void AddNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            string newNotification = NewNotificationTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(newNotification))
            {
                Notifications.Add(newNotification);
                NewNotificationTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Unesite tekst obavještenja prije dodavanja.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            string notificationToDelete = (sender as Button)?.Tag as string;

            if (notificationToDelete != null && Notifications.Contains(notificationToDelete))
            {
                Notifications.Remove(notificationToDelete);
            }
        }
    }
}
