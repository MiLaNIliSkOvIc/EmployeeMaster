using System.Windows;
using EmployeeMaster.Administrator.SettingsScreen;
using EmployeeMaster.Employee.TaskScreen;
using EmployeeMaster.Employee.NotificationScreen;
using EmployeeMaster.Employee.PersonalInfoScreen;
using EmployeeMaster.Employee.WorkedHoursScreen;
using EmployeeMaster.Model;

using EmployeeMaster.Employee.DashBoardView;

namespace EmployeeMaster.Employee.EmployeeMainScreen
{
    public partial class EmployeeMainScreen : Window
    {
        public static string style = "Styles2";
        public static string language = "en-US";
        public EmployeeMainScreen()
        {
            InitializeComponent();
            int userId = CurrentUser.Instance.IdUser;
            new SettingsScreen(userId, this);
            MainContentArea.Content = new Dashboard();
        }

        public void ChangeStyleForCurrentWindow(string newStylePath)
        {
            var newResourceDictionary = new ResourceDictionary
            {
                Source = new Uri(newStylePath, UriKind.RelativeOrAbsolute)
            };
            var newResourceDictionary2 = new ResourceDictionary
            {

                Source = new Uri($"../../Language/Resources.{language}.xaml", UriKind.RelativeOrAbsolute)
            };
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResourceDictionary);
            this.Resources.MergedDictionaries.Add(newResourceDictionary2);
        }
        public void ChangeStyle()
        {
            ChangeStyleForCurrentWindow($"../../Styles/{style}.xaml");

        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
                         
            MainContentArea.Content = new Dashboard();
        }

       
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = CurrentUser.Instance.IdUser;
            MainContentArea.Content = new SettingsScreen(userId,this);
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
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            

            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}