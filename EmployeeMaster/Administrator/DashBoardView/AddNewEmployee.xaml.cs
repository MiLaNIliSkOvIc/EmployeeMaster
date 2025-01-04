using EmployeeMaster.Model;
using EmployeeMaster.Services.EmployeeMaster.Services;
using Microsoft.Win32;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EmployeeMaster.ErrorDisplay;
using EmployeeMaster.Administator.MainScreen;

namespace EmployeeMaster.Administrator.DashBoardView
{
    public partial class AddNewEmployee : Window
    {
        private readonly PositionService _positionService;

        public AddNewEmployee()
        {
            InitializeComponent();
            _positionService = new PositionService();

            var newResourceDictionary = new ResourceDictionary
            {
                Source = new Uri($"../../Styles/{MainScreen.style}.xaml", UriKind.RelativeOrAbsolute)

            };
            var newResourceDictionary2 = new ResourceDictionary
            {

                Source = new Uri($"../../Language/Resources.{MainScreen.language}.xaml", UriKind.RelativeOrAbsolute)
            };
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResourceDictionary);
            this.Resources.MergedDictionaries.Add(newResourceDictionary2);
            LoadPositions();
        }


        public string FirstName => FirstNameTextBox.Text;
        public string LastName => LastNameTextBox.Text;
        public string Username => UsernameTextBox.Text;
        public string Password => PasswordBox.Password;
        public string Email => EmailTextBox.Text;
        public string Phone => PhoneTextBox.Text;
        public string Picture;// => PictureTextBox.Text;
        public int? Position => PositionComboBox.SelectedValue as int? ;
        public int? Role => RoleComboBox.SelectedValue as int? ;

        public DateTime HireDate => DateTime.Now;
        public int Salary => int.TryParse(SalaryTextBox.Text, out var salary) ? salary : 0;

        private void LoadPositions()
        {
            try
            {
                var positions = _positionService.GetPositions(); 
                PositionComboBox.ItemsSource = positions;       
                PositionComboBox.DisplayMemberPath = "Name";    
                PositionComboBox.SelectedValuePath = "Id";
                var roles = new List<Role>
                {
                new Role { idRole = 1, nameRole = "Admin" },
                new Role { idRole = 2, nameRole = "Employee" }
                  };

                
                RoleComboBox.ItemsSource = roles;
                RoleComboBox.DisplayMemberPath = "nameRole";
                RoleComboBox.SelectedValuePath = "idRole";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading positions: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone) ||
                Salary <= 0 ||
                Position == null || Role == null)
            {
                ErrorWindow err = new ErrorWindow("Please fill in all required fields correctly.");
                err.Show();
                return;
            }
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
           

            if (!emailRegex.IsMatch(Email))
            {
                ErrorWindow err = new ErrorWindow("Please enter a valid email address.");
                err.Show();
                EmailTextBox.Focus(); 
                return; 
            }

            if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                ErrorWindow err = new ErrorWindow("Passwords do not match.");
                err.Show();
              
                return; 
            }

            string picturePath = Picture; 
            if (string.IsNullOrEmpty(picturePath) && PicturePreview.Source == null)
            {
                ErrorWindow err = new ErrorWindow("Please select a picture.");
                err.Show();

                return;
            }
            else if (!string.IsNullOrEmpty(picturePath))
            {
               
            }

            try
            {
               
                var employeeService = new EmployeeService(); 
                employeeService.AddEmployee(
                    FirstName,
                    LastName,
                    Username,
                    Password,
                    Email,
                    Phone,
                    picturePath,
                    Position,
                    HireDate.Date,
                    Salary,
                    Role.Value 
                );

                ErrorWindow err = new ErrorWindow("Employee added successfully.");
                err.Show();


            }
            catch (Exception ex)
            {
                ErrorWindow err = new ErrorWindow($"Error saving employee: {ex.Message}");
                err.Show();
            }
        

        DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            DialogResult = false;
            Close();
        }
        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            var regex = new Regex(@"^0\d{0,11}$");
            var newText = PhoneTextBox.Text + e.Text;

            
            e.Handled = !regex.IsMatch(newText);
        }
        private void SalaryTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            var regex = new Regex(@"^\d*$"); // Only digits are allowed

           
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void BrowsePictureButton_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

           
            if (openFileDialog.ShowDialog() == true)
            {
                
                string imagePath = openFileDialog.FileName;

                BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                Picture = imagePath;
                
                PicturePreview.Source = bitmap;
                
            }            
        }
    }
}
