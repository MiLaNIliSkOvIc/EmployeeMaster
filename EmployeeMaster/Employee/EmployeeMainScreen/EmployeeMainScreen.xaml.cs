using System.Windows;
using EmployeeMaster.Administrator.SettingsScreen;
using EmployeeMaster.Employee.TaskScreen;
using EmployeeMaster.Employee.NotificationScreen;
using EmployeeMaster.Employee.PersonalInfoScreen;
using EmployeeMaster.Employee.WorkedHoursScreen;
using EmployeeMaster.Model;

using EmployeeMaster.Employee.DashBoardView;
using System.Windows.Media;

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
            ResetButtonColors();
            DashboardButton.Foreground = new SolidColorBrush(Colors.Black);

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
            ResetButtonColors();
            DashboardButton.Foreground = new SolidColorBrush(Colors.Black);

        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = CurrentUser.Instance.IdUser;
            MainContentArea.Content = new SettingsScreen(userId,this);
            ResetButtonColors();
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new EmployeeNotificationScreen();
            ResetButtonColors();
            NotificationsButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new VacationRequestsScreen();
            ResetButtonColors();
            VacationButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void PersonalInfoButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PersonalInfo();
            ResetButtonColors();
            PersonalInfoButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void WorkedHoursButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new WorkedHours();
            ResetButtonColors();
            WorkedHoursButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new EmployeeTaskScreen();
            ResetButtonColors();
            TasksButton.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            

            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
        private void ResetButtonColors()
        {
            
            DashboardButton.Foreground = new SolidColorBrush(Colors.White);
            TasksButton.Foreground = new SolidColorBrush(Colors.White);
            VacationButton.Foreground = new SolidColorBrush(Colors.White);
            WorkedHoursButton.Foreground = new SolidColorBrush(Colors.White);
            NotificationsButton.Foreground = new SolidColorBrush(Colors.White);
            PersonalInfoButton.Foreground = new SolidColorBrush(Colors.White);
            LogoutButton.Foreground = new SolidColorBrush(Colors.White);
            SettingsButton.Foreground = new SolidColorBrush(Colors.White);
        }


    }
}