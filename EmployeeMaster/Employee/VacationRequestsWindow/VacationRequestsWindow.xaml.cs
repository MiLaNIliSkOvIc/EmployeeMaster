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

namespace EmployeeMaster.Employee
{
    public partial class VacationRequestsScreen : UserControl
    {
        private VacationRequestsViewModel _viewModel;

        public VacationRequestsScreen()
        {
            int employeeId = 1;
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
            var newRequest = new Vacation
            {
                VacationRequestId = _viewModel.VacationRequests.Count + 1,
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"),
                Status = "Pending"
            };

            _viewModel.VacationRequests.Add(newRequest);
            MessageBox.Show("New vacation request added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}
