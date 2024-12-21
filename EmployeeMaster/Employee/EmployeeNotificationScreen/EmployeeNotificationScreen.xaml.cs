using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EmployeeMaster.Employee.NotificationScreen
{
    public partial class EmployeeNotificationScreen : UserControl
    {
        public ObservableCollection<string> Notifications { get; set; }

        public EmployeeNotificationScreen()
        {
            InitializeComponent();
            Notifications = new ObservableCollection<string>
            {
                "Meeting at 10:00 AM",
                "Submit your reports by EOD",
                "System maintenance scheduled for 2:00 PM"
            };
            NotificationList.ItemsSource = Notifications;
        }
    }
}
