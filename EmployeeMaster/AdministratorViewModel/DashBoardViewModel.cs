using System.Collections.ObjectModel;
using EmployeeMaster.Services;
using EmployeeMaster.Model;
using employee = EmployeeMaster.Model.Employee;
using System.Windows;


namespace EmployeeMaster.AdministratorViewModel
{
  
        public class DashBoardViewModel
        {
            private readonly EmployeeService _employeeService;
            public ObservableCollection<employee> Employees { get; set; }

            public DashBoardViewModel()
            {
                _employeeService = new EmployeeService();
                Employees = new ObservableCollection<employee>(_employeeService.GetEmployees());
            }

            public void Search(string searchTerm)
            {
                var searchResults = _employeeService.SearchEmployees(searchTerm);
                Employees.Clear();
                foreach (var employee in searchResults)
                {
                    Employees.Add(employee);
                }
            }
            public void AddEmployee(string firstName, string lastName, string username, string password, string email, string phone, string picture, int? positionId, DateTime hireDate, int salary)
            {
                try
                {
                    _employeeService.AddEmployee(firstName, lastName, username, password, email, phone, picture, positionId, hireDate, salary);
                    RefreshEmployees();
                    MessageBox.Show("New employee added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void RefreshEmployees()
            {
                Employees.Clear();
                foreach (var employee in _employeeService.GetEmployees())
                {
                    Employees.Add(employee);
                }
            }
    }
    
}
