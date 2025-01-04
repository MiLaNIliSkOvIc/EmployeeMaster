using EmployeeMaster.EmployeeViewModel;
using System.Windows.Controls;

namespace EmployeeMaster.Employee.NotificationScreen
{
    public partial class EmployeeNotificationScreen : UserControl
    {
        private NotificationViewModel _viewModel;
   

        public EmployeeNotificationScreen()
        {
            InitializeComponent();
            _viewModel = new NotificationViewModel();
            DataContext = _viewModel;
            NotificationList.ItemsSource = _viewModel.NotificationsString;
        }

      
    }
}
