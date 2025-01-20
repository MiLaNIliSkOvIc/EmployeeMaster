using EmployeeMaster.Administrator.SettingsScreen;
using System;
using System.Threading.Tasks;
using System.Windows;
using EmployeeMaster.Translator;

namespace EmployeeMaster.NotificationDisplay
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message)
        {
            InitializeComponent();
            TranslateMessageAsync(message).ContinueWith(task =>
            {
                Dispatcher.Invoke(() =>
                {
                    MessageTextBlock.Text = task.Result;
                });
            });

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

            this.Topmost = true;
        }

        private async Task<string> TranslateMessageAsync(string message)
        {
           
            return await Translator.Translator.TranslateAsync(message);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
