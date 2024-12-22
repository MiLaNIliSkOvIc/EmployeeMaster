using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.Employee.EmployeeMainScreen;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace EmployeeMaster.Administrator.SettingsScreen
{
    public partial class SettingsScreen : UserControl
    {
        private MainScreen _mainScreen;
        private EmployeeMainScreen _employeeMainScreen;
        public SettingsScreen(MainScreen mainScreen)
        {
            InitializeComponent();
            _mainScreen = mainScreen;
        }
        public SettingsScreen(EmployeeMainScreen employee)
        {

            InitializeComponent();
            _employeeMainScreen = employee;
        }

      
        
        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            ApplyTheme(ThemeComboBox.SelectedIndex);

            
            ApplyLanguage(LanguageComboBox.SelectedIndex);

         
            bool areNotificationsEnabled = NotificationsCheckBox.IsChecked.GetValueOrDefault();

         
        }

        private void ApplyTheme(int selectedIndex)
        {
            if (_employeeMainScreen != null)
            {
                if (selectedIndex == 0)
                {

                    EmployeeMainScreen.style = "Styles1";
                    _employeeMainScreen.ChangeStyle();

                }
                else
                {
                    EmployeeMainScreen.style = "Styles2";
                    _employeeMainScreen.ChangeStyle();
                }
            }
            else
            {
                if (selectedIndex == 0)
                {

                    MainScreen.style = "Styles1";
                    _mainScreen.ChangeStyle();

                }
                else
                {
                    MainScreen.style = "Styles2";
                    _mainScreen.ChangeStyle();
                }
            }
        }

        private void ApplyLanguage(int selectedIndex)
        {

            if (_employeeMainScreen != null)
            {
                if (selectedIndex == 0)
                {

                    EmployeeMainScreen.language = "en-US";
                    _employeeMainScreen.ChangeStyle();

                }
                else if (selectedIndex == 1)
                {
                    EmployeeMainScreen.language = "sr-RS";
                    _employeeMainScreen.ChangeStyle();
                }
                else if (selectedIndex == 2)
                {
                    EmployeeMainScreen.language = "de-DE";
                    _employeeMainScreen.ChangeStyle();
                }
                else
                {
                    EmployeeMainScreen.language = "it-IT";
                    _employeeMainScreen.ChangeStyle();
                }
            
            }
            else
            {
                if (selectedIndex == 0)
                {

                    MainScreen.language = "en-US";
                    _mainScreen.ChangeStyle();

                }
                else if(selectedIndex == 1)
                {
                    MainScreen.language = "sr-RS";
                    _mainScreen.ChangeStyle();
                }
                else if(selectedIndex == 2)
                {
                    MainScreen.language = "de-DE";
                    _mainScreen.ChangeStyle();
                }
                else
                {

                    MainScreen.language = "it-IT";
                    _mainScreen.ChangeStyle();
                }
           
            }
        }
    }
}
