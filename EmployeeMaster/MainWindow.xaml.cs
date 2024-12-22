using EmployeeMaster.Employee.EmployeeMainScreen;
using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.Employee.DashBoardView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmployeeMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
         
           
        }

      
        private void Login(object sender, RoutedEventArgs e)
        {
        
        string username = "admin";  
        string password = "password";  

 
        if (username == "admin" && password == "password")
        {

                //MessageBox.Show("Login uspešan!");
                EmployeeMainScreen m = new EmployeeMainScreen();
                m.Show();
                //Console.WriteLine("dasdwad");
                //VacationRequests vacation = new VacationRequests();
                //vacation.Show();
                //this.Close();
                //  WorkedHours workedHours = new WorkedHours();
                ///workedHours.Show();
                //   this.Close();
                MainScreen main = new MainScreen();
                main.Show();
                this.Close();
            }
        else
        {
          
            MessageBox.Show("Neispravno korisničko ime ili lozinka.");
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
    }
}