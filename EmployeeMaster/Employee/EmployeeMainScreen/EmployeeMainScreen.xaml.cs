using System.Windows;
using EmployeeMaster.Administrator.SettingsScreen;
using EmployeeMaster.Employee.TaskScreen;
using EmployeeMaster.Employee.NotificationScreen;
using EmployeeMaster.Employee.PersonalInfoScreen;
using EmployeeMaster.Employee.WorkedHoursScreen;

namespace EmployeeMaster.Employee.EmployeeMainScreen
{
    public partial class EmployeeMainScreen : Window
    {
        public EmployeeMainScreen()
        {
            InitializeComponent();
        }

     
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            
            

            
            MainContentArea.Content = new EmployeeTaskScreen();
        }

       
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new SettingsScreen();
        }
        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new EmployeeNotificationScreen();
        }
        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new VacationRequestsScreen();
        }
        private void PersonalInfoButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PersonalInfo();
        }
        private void WorkedHoursButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new WorkedHours();
        }
        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new EmployeeTaskScreen();
        }

    }
}