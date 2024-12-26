using EmployeeMaster.Services.EmployeeMaster.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administrator.DashBoardView
{
    public partial class AddNewEmployee : Window
    {
        private readonly PositionService _positionService;

        public AddNewEmployee()
        {
            InitializeComponent();
            _positionService = new PositionService();

          
            LoadPositions();
        }


        public string FirstName => FirstNameTextBox.Text;
        public string LastName => LastNameTextBox.Text;
        public string Username => UsernameTextBox.Text;
        public string Password => PasswordBox.Password;
        public string Email => EmailTextBox.Text;
        public string Phone => PhoneTextBox.Text;
        public string Picture => PictureTextBox.Text;
        public int? Position => PositionComboBox.SelectedValue as int? ;

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
               
                Salary <= 0)
            {
                MessageBox.Show("Please fill in all required fields correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            DialogResult = false;
            Close();
        }
    }
}
