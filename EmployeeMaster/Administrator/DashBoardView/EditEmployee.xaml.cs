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
using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.NotificationDisplay;

namespace EmployeeMaster.Administrator.DashBoardView
{
    public partial class EditEmployee : Window
    {
        private readonly PositionService _positionService;
        private Model.Employee employee;
        public EditEmployee(Model.Employee employee)
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
            this.employee = employee;
          
            EmailTextBox.Text = employee.Email;
            PhoneTextBox.Text = employee.Phone;
            var position = _positionService.GetPositions()
                    .FirstOrDefault(p => p.Name == employee.Position);
            PositionComboBox.SelectedValue = position.Id; 
            SalaryTextBox.Text = employee.Salary.ToString();
        }


       
        public string Email => EmailTextBox.Text;
        public string Phone => PhoneTextBox.Text;
        public int? Position => PositionComboBox.SelectedValue as int?;
     
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
                NotificationWindow notif = new NotificationWindow($"Error loading position: {ex.Message}");
                notif.Show();

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone) ||
                Salary <= 0 ||
                Position == null)
            {
                NotificationWindow err = new NotificationWindow("Please fill in all required fields correctly.");
                err.Show();
                return;
            }
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");


            if (!emailRegex.IsMatch(Email))
            {
                NotificationWindow err = new NotificationWindow("Please enter a valid email address.");
                err.Show();
                EmailTextBox.Focus();
                return;
            }

            

            try
            {

                var employeeService = new EmployeeService();
                employeeService.UpdateEmployee(
                    employee.Id,
                    Email,
                    Phone,
                    Salary,
                    Position
                );

                NotificationWindow err = new NotificationWindow("Employee updated successfully.");
                err.Show();


            }
            catch (Exception ex)
            {
                NotificationWindow err = new NotificationWindow($"Error saving employee: {ex.Message}");
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

            var regex = new Regex(@"^\d*$"); 


            e.Handled = !regex.IsMatch(e.Text);
        }

     
    }
}
