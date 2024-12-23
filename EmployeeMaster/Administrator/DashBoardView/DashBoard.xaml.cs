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
            MessageBox.Show("Add New Employee functionality will be implemented.");
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change logic
        }
    }
}
