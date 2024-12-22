using EmployeeMaster.Administator.DashBoardView;
using EmployeeMaster.Administator.VacationScreen;
using EmployeeMaster.Administrator.TaskScreen;
using EmployeeMaster.Administrator.NotificationScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmployeeMaster.Administrator.SettingsScreen;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

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
            MainContentArea.Content = new DashBoard();
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
            ChangeThemeForWindow();
           
        }
        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
           
            MainContentArea.Content = new Vacation();
            ChangeThemeForWindow();

        }
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            

            MainContentArea.Content = new TaskScreen();
            ChangeThemeForWindow();

        }
        private void BoardButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Notification();
            ChangeThemeForWindow();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainContentArea.Content = new SettingsScreen(this);
           
        }
        public void ChangeStyle()
        {
            ChangeStyleForCurrentWindow($"../../Styles/{style}.xaml");
            
        }

    }
}
