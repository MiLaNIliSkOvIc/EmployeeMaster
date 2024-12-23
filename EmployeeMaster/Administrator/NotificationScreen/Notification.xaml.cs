using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using EmployeeMaster.AdministratorViewModel;
using EmployeeMaster.Model;

namespace EmployeeMaster.Administrator.NotificationScreen
{
    public partial class Notification : UserControl
    {
        private readonly NotificationViewModel _viewModel;

        public Notification()
        {
            InitializeComponent();
            _viewModel = new NotificationViewModel();
            DataContext = _viewModel;
        }

        private void AddNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NewNotificationContent = NewNotificationTextBox.Text.Trim();
            _viewModel.AddNotification();
            NewNotificationTextBox.Clear();
        }

        private void DeleteNotificationButton_Click(object sender, RoutedEventArgs e)
        {
        
            if ((sender as Button)?.Tag is NotificationModel notificationToDelete)
            {
                _viewModel.DeleteNotification(notificationToDelete);
            }
           
        }
    }
}
