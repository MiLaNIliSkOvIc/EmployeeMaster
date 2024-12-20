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
        public SettingsScreen()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            ApplyTheme(ThemeComboBox.SelectedIndex);

            
            ApplyLanguage(LanguageComboBox.SelectedIndex);

         
            bool areNotificationsEnabled = NotificationsCheckBox.IsChecked.GetValueOrDefault();

            MessageBox.Show("Settings saved!");
        }

        private void ApplyTheme(int selectedIndex)
        {
            
            if (selectedIndex == 0)
            {
                
                Application.Current.Resources["WindowBackgroundColor"] = Brushes.White;
                Application.Current.Resources["TextColor"] = Brushes.Black;
            }
            else
            {
              
                Application.Current.Resources["WindowBackgroundColor"] = Brushes.Black;
                Application.Current.Resources["TextColor"] = Brushes.White;
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
