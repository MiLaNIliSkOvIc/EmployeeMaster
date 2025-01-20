using EmployeeMaster.Administrator.SettingsScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeMaster.YesNoWindow
{
    public partial class YesNoDialog : Window
    {
        public YesNoDialog(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;

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

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; 
            this.Close(); 
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; 
            this.Close(); 
        }
    }
}
