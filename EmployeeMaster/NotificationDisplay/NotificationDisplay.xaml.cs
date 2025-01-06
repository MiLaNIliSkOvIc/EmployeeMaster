using EmployeeMaster.Administrator.SettingsScreen;
using EmployeeMaster.Employee.EmployeeMainScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace EmployeeMaster.NotificationDisplay
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
            var newResourceDictionary = new ResourceDictionary
            {
                Source = new Uri($"../Styles/{SettingsScreen.style}.xaml", UriKind.RelativeOrAbsolute)

            };
            var newResourceDictionary2 = new ResourceDictionary
            {

                Source = new Uri($"../Language/Resources.{SettingsScreen.Language}.xaml", UriKind.RelativeOrAbsolute)
            };
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResourceDictionary);
            this.Resources.MergedDictionaries.Add(newResourceDictionary2);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
