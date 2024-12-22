using EmployeeMaster.Administator.MainScreen;
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
        public SettingsScreen(MainScreen mainScreen)
        {
            InitializeComponent();
            _mainScreen = mainScreen;
        }

        public SettingsScreen()
        {
            InitializeComponent();
           
        }
        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            ApplyTheme(ThemeComboBox.SelectedIndex);

            
            ApplyLanguage(LanguageComboBox.SelectedIndex);

         
            bool areNotificationsEnabled = NotificationsCheckBox.IsChecked.GetValueOrDefault();

         
        }

        private void ApplyTheme(int selectedIndex)
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

        private void ApplyLanguage(int selectedIndex)
        {
            
            if (selectedIndex == 0)
            {
                // English
               // Application.Current.CurrentCulture = new CultureInfo("en-US");
            }
            else
            {
                // Serbian
                //Application.Current.CurrentCulture = new CultureInfo("sr-RS");
            }
        }
    }
}
