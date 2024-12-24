using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.AdministratorViewModel;
using EmployeeMaster.Employee.EmployeeMainScreen;
using EmployeeMaster.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administrator.SettingsScreen
{
    public partial class SettingsScreen : UserControl
    {
        private SettingsViewModel _viewModel;
        private MainScreen _mainScreen;
        private EmployeeMainScreen _employeeMainScreen;

        public SettingsScreen(int userId, MainScreen mainScreen = null)
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel(userId);
            _mainScreen = mainScreen;
         
            LoadSettings();
        }
        public SettingsScreen(int userId, EmployeeMainScreen employeeMainScreen = null)
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel(userId);
            _employeeMainScreen = employeeMainScreen;
            LoadSettings();
        }

        private void LoadSettings()
        {
            var settings = _viewModel.CurrentSettings;

            if (settings != null)
            {
                
                ThemeComboBox.SelectedIndex = settings.Theme == "Styles1" ? 0 : 1;

             
                LanguageComboBox.SelectedIndex = settings.Language switch
                {
                    "en-US" => 0,
                    "sr-RS" => 1,
                    "de-DE" => 2,
                    _ => 3,
                };
            }
            ApplyTheme(settings.Theme);
            ApplyLanguage(settings.Language);
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the updated settings model
            var updatedSettings = new SettingModel
            {
                Theme = ThemeComboBox.SelectedIndex == 0 ? "Styles1" : "Styles2",
                Language = LanguageComboBox.SelectedIndex switch
                {
                    0 => "en-US",
                    1 => "sr-RS",
                    2 => "de-DE",
                    _ => "it-IT",
                },
            };

            // Save settings to the database using the ViewModel
            _viewModel.SaveSettings(1, updatedSettings);

            // Call your functions to apply the theme and language changes
            ApplyTheme(updatedSettings.Theme);
            ApplyLanguage(updatedSettings.Language);
        }

        private void ApplyTheme(string theme)
        {
            if (_employeeMainScreen != null)
            {
                if (theme == "Styles1")
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
                if (theme == "Styles1")
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

        private void ApplyLanguage(string language)
        {
            if (_employeeMainScreen != null)
            {
                EmployeeMainScreen.language = language;
                _employeeMainScreen.ChangeStyle();
            }
            else
            {
                MainScreen.language = language;
                _mainScreen.ChangeStyle();
            }
        }
    }
}
