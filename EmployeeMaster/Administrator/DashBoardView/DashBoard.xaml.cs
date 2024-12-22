using EmployeeMaster.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administator.DashBoardView
{
    public partial class DashBoard : UserControl
    {
        public ObservableCollection<Model.Employee> Employees { get; set; }
        public DashBoard()
        {
            InitializeComponent();
         
            Employees = new ObservableCollection<Model.Employee>
            {
                new Model.Employee { FirstName = "John", LastName = "Doe", Position = "Manager", HireDate = "2021-01-15" },
                new Model.Employee { FirstName = "Jane", LastName = "Smith", Position = "Developer", HireDate = "2022-06-10" },
                new Model.Employee { FirstName = "Alice", LastName = "Brown", Position = "Designer", HireDate = "2020-03-05" }
            };
            DataContext = this;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
          
            MessageBox.Show("Search functionality will be implemented.");
        }

       
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Add New Employee functionality will be implemented.");
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }

    
    
}

