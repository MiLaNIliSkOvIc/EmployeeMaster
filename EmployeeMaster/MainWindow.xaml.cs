using EmployeeMaster.Employee.EmployeeMainScreen;
using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.Employee.DashBoardView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EmployeeMaster.Services;
using EmployeeMaster.Model;
using EmployeeMaster.RoleSelection;

namespace EmployeeMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LoginService _loginService;
        private readonly EmployeeService _employeeService;
        
        

        public MainWindow()
        {
            InitializeComponent();
            _loginService = new LoginService();
            _employeeService = new EmployeeService();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;


            var user = _loginService.Login(username, password);

            if (user != null)
            {
                var roles = _employeeService.GetUserRole(user.IdUser);

                if (roles.Count == 1)
                {

                    CurrentUser.Instance.SetUser(user.IdUser, username, roles[0]);
                    if (roles[0] == "Employee")
                    {
                        EmployeeMainScreen employeeMainScreen = new EmployeeMainScreen();
                        employeeMainScreen.Show();
                    }
                    else
                    {
                        MainScreen adminMainScreen = new MainScreen();
                        adminMainScreen.Show();
                    }
                    this.Close();
                }
                else if (roles.Count > 1)
                {

                    var roleSelectionWindow = new RoleSelectionWindow(roles);
                    if (roleSelectionWindow.ShowDialog() == true)
                    {
                        string selectedRole = roleSelectionWindow.SelectedRole;
                        CurrentUser.Instance.SetUser(user.IdUser, username, selectedRole);
                        if (selectedRole == "Employee")
                        {
                            EmployeeMainScreen employeeMainScreen = new EmployeeMainScreen();
                            employeeMainScreen.Show();
                            
                        }
                        else
                        {
                            MainScreen adminMainScreen = new MainScreen();
                            adminMainScreen.Show();
                        }
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Korisnik nema dodeljene uloge.");
                }
            }
            else
            {
                MessageBox.Show("Neispravni kredencijali");
            }
        }
        
   
        private void ClearPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Username")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void RestorePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Username";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void ClearPasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Password == "Password")
            {
                passwordBox.Password = string.Empty;
                passwordBox.Foreground = Brushes.Black;
            }
        }

        private void RestorePasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = "Password";
                passwordBox.Foreground = Brushes.Gray;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}