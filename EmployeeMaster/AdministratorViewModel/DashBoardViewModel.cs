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
         

            public void RefreshEmployees()
            {
                Employees.Clear();
                foreach (var employee in _employeeService.GetEmployees())
                {
                    Employees.Add(employee);
                }
            }

        public void DeleteEmployee(int userId)
        {
            try
            {
                _employeeService.DeleteEmployee(userId);
                RefreshEmployees();
            }
            catch (Exception ex)
            {
              
                MessageBox.Show("Error deleting employee: " + ex.Message);
            }
        }
    }
    
}
