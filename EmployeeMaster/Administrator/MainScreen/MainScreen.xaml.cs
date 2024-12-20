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

namespace EmployeeMaster.Administator.MainScreen
{
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();
            MainContentArea.Content = new DashBoard();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {

            MainContentArea.Content = new DashBoard();
        }
        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
           
            MainContentArea.Content = new Vacation(); 
        }
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            

            MainContentArea.Content = new TaskScreen();

        }
        private void BoardButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Notification(); 
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainContentArea.Content = new SettingsScreen();
        }
    }
}
