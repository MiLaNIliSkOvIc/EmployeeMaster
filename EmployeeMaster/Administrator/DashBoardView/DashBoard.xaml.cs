using EmployeeMaster.Administrator.DashBoardView;
using EmployeeMaster.AdministratorViewModel;
using EmployeeMaster.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.Model;
using System.Globalization;
using System.Windows.Data;

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
                
            }
            _viewModel.RefreshEmployees();
        }




        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Model.Employee selectedEmployee)
            {
               
                string message = $"Are you sure you want to delete the employee: {selectedEmployee.FullName}?";
                string translatedMessage = await EmployeeMaster.Translator.Translator.TranslateAsync(message);

             
                var result = MessageBox.Show(
                    translatedMessage,
                    await EmployeeMaster.Translator.Translator.TranslateAsync("Confirm Delete"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    int userId = selectedEmployee.Id;
                    _viewModel.DeleteEmployee(userId);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            if (EmployeeDataGrid.SelectedItem is Model.Employee selectedEmployee)
            {

                EditEmployee edit = new EditEmployee(selectedEmployee);
                if (edit.ShowDialog() == true)
                {
                }
                _viewModel.RefreshEmployees();


            }
        }
    

    }
   

}
