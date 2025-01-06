using EmployeeMaster.Administator.DashBoardView;
using EmployeeMaster.Administator.VacationScreen;
using EmployeeMaster.Administrator.TaskScreen;
using EmployeeMaster.Administrator.NotificationScreen;
using System.Windows;
using EmployeeMaster.Administrator.SettingsScreen;
using EmployeeMaster.Model;
using EmployeeMaster.Administrator.WorkedHoursScreen;
using System.Windows.Media;

namespace EmployeeMaster.Administator.MainScreen
{
    public partial class MainScreen : Window
    {
      
        public static string style = "Styles1";
        public static string language = "en-US";
        public MainScreen()
        {
            InitializeComponent();
            ChangeStyleForCurrentWindow($"../../Styles/{style}.xaml");
            int userId = CurrentUser.Instance.IdUser;
            new SettingsScreen(userId, this);
          
            MainContentArea.Content = new DashBoard();
            EmployeesButton.Foreground = new SolidColorBrush(Colors.Black);
            ChangeThemeForWindow();
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

        public void ChangeThemeForWindow()
        {
           
            var currentWindow = MainContentArea.Content as FrameworkElement;

            if (currentWindow != null)
            {                
                var newResourceDictionary = new ResourceDictionary
                {
                    Source = new Uri($"../../Styles/{style}.xaml", UriKind.Relative)
                };
                currentWindow.Resources.MergedDictionaries.Clear();
                currentWindow.Resources.MergedDictionaries.Add(newResourceDictionary);
            }
        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new DashBoard();
            ResetButtonColors();
            EmployeesButton.Foreground = new SolidColorBrush(Colors.Black);
            ChangeThemeForWindow();
           
        }
        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
           
            MainContentArea.Content = new VacationScreen.Vacation();
            ResetButtonColors();
            VacationButton.Foreground = new SolidColorBrush(Colors.Black);
            ChangeThemeForWindow();

        }
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            

            MainContentArea.Content = new TaskScreen();
            ResetButtonColors();
            TasksButton.Foreground = new SolidColorBrush(Colors.Black);
            ChangeThemeForWindow();

        }
        private void BoardButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Notification();
            ResetButtonColors();
            BoardButton.Foreground = new SolidColorBrush(Colors.Black);
            ChangeThemeForWindow();
        }
        private void WorkedHoursButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new WorkedHours();
            ResetButtonColors();
            WorkedHoursButton.Foreground = new SolidColorBrush(Colors.Black);
            
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId =  CurrentUser.Instance.IdUser;
            
            MainContentArea.Content = new SettingsScreen(userId,this);
            ResetButtonColors();
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);

        }
        public void ChangeStyle()
        {
            ChangeStyleForCurrentWindow($"../../Styles/{style}.xaml");
            
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
           

            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
        private void ResetButtonColors()
        {
            EmployeesButton.Foreground = new SolidColorBrush(Colors.White);
            VacationButton.Foreground = new SolidColorBrush(Colors.White);
            TasksButton.Foreground = new SolidColorBrush(Colors.White);
            BoardButton.Foreground = new SolidColorBrush(Colors.White);
            WorkedHoursButton.Foreground = new SolidColorBrush(Colors.White);
            LogoutButton.Foreground = new SolidColorBrush(Colors.White);
            SettingsButton.Foreground = new SolidColorBrush(Colors.White);
        }


    }
}
