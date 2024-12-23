using System.Collections.ObjectModel;
using EmployeeMaster.Services;
using EmployeeMaster.Model;
using employee = EmployeeMaster.Model.Employee;


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
        }
    
}
