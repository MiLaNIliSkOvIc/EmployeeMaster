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
        public static string style = "Styles2";
        public static string Language = "en-US";
        private int userId {  get; set; }
        public SettingsScreen(int userId, MainScreen mainScreen = null)
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel(userId);
            _mainScreen = mainScreen;
            this.userId = userId;
            LoadSettings();
        }
        public SettingsScreen(int userId, EmployeeMainScreen employeeMainScreen = null)
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel(userId);
            _employeeMainScreen = employeeMainScreen;
            this.userId = userId;
            LoadSettings();
        }

        private void LoadSettings()
        {
            var settings = _viewModel.CurrentSettings;

            if (settings != null)
            {
                
                ThemeComboBox.SelectedIndex = settings.Theme == "Styles1" ? 0 : 1;

                ThemeComboBox.SelectedIndex = settings.Theme switch
                {
                    "Styles1" => 0,
                    "Styles2" => 1,
                    "Styles3" => 2,

                };
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

            var updatedSettings = new SettingModel
            {
                //Theme = ThemeComboBox.SelectedIndex == 0 ? "Styles1" : "Styles2",
                Theme = ThemeComboBox.SelectedIndex switch
                {
                    0 => "Styles1",
                    1 => "Styles2",
                    2 => "Styles3",
               
                },

                Language = LanguageComboBox.SelectedIndex switch
                {
                    0 => "en-US",
                    1 => "sr-RS",
                    2 => "de-DE",
                    _ => "it-IT",
                },
            };

  
            _viewModel.SaveSettings(userId, updatedSettings);


            ApplyTheme(updatedSettings.Theme);
            ApplyLanguage(updatedSettings.Language);
        }

        private void ApplyTheme(string theme)
        {
            style = theme;

            if (_employeeMainScreen != null)
            {
                if (theme == "Styles1")
                {
                    EmployeeMainScreen.style = "Styles1";
                    _employeeMainScreen.ChangeStyle();
                }
                else if(theme == "Styles2")
                {
                    EmployeeMainScreen.style = "Styles2";
                    _employeeMainScreen.ChangeStyle();
                }
                else
                {
                    EmployeeMainScreen.style = "Styles3";
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
                else if (theme == "Styles2")
                {
                    MainScreen.style = "Styles2";
                    _mainScreen.ChangeStyle();
                }
                else
                {
                    MainScreen.style = "Styles3";
                    _mainScreen.ChangeStyle();
                }
            }
        }

        private void ApplyLanguage(string language)
        {
            Language = language;
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
