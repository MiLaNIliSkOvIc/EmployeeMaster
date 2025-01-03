using EmployeeMaster.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.Model;
using EmployeeMaster.Administrator.TaskScreen;
using EmployeeMaster.Administrator.VacationRequestsWindow;

namespace EmployeeMaster.Employee
{
    public partial class VacationRequestsScreen : UserControl
    {
        private VacationRequestsViewModel _viewModel;

        public VacationRequestsScreen()
        {
            int employeeId = CurrentUser.Instance.IdUser;
            InitializeComponent();
            _viewModel = new VacationRequestsViewModel(employeeId);
            DataContext = _viewModel;
            VacationRequestsTable.ItemsSource = _viewModel.VacationRequests;
        }

        private void FilterTable_Click(object sender, RoutedEventArgs e)
        {
            if (FilterDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = FilterDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                var filteredList = _viewModel.VacationRequests
                    .Where(v => v.StartDate == selectedDate || v.EndDate == selectedDate)
                    .ToList();
                VacationRequestsTable.ItemsSource = filteredList;
            }
            else
            {
                MessageBox.Show("Please select a date to filter.", "Filter Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddNewRequest_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.addVacation();
            VacationRequestsTable.ItemsSource = _viewModel.VacationRequests;
        }
        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
          
                var result = MessageBox.Show(
                    "Are you sure you want to delete this vacation request?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            var selectedVacation = VacationRequestsTable.SelectedItem as Model.Vacation;
            if (result == MessageBoxResult.Yes)
                {
                    _viewModel.DeleteVacationRequest(selectedVacation);
                }
            
        }

    }

}
