using EmployeeMaster.Administrator.DashBoardView;
using EmployeeMaster.AdministratorViewModel;
using EmployeeMaster.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administator.DashBoardView
{
    public partial class DashBoard : UserControl
    {
        private DashBoardViewModel _viewModel;

        public DashBoard()
        {
            InitializeComponent();
            _viewModel = new DashBoardViewModel();
            DataContext = _viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTextBox.Text;
            _viewModel.Search(searchTerm);
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddNewEmployee();
            if (addEmployeeWindow.ShowDialog() == true)
            {
                try
                {
                    string firstName = addEmployeeWindow.FirstName;
                    string lastName = addEmployeeWindow.LastName;
                    string username = addEmployeeWindow.Username;
                    string password = addEmployeeWindow.Password;
                    string email = addEmployeeWindow.Email;
                    string phone = addEmployeeWindow.Phone;
                    string picture = addEmployeeWindow.Picture;
                    int? positionId = addEmployeeWindow.Position; 
                    DateTime hireDate = addEmployeeWindow.HireDate;
                    int salary = addEmployeeWindow.Salary;

                    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                    {
                        MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var viewModel = (DashBoardViewModel)DataContext;
                    viewModel.AddEmployee(firstName, lastName, username, password, email, phone, picture, positionId, hireDate, salary);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change logic
        }
    }
}
