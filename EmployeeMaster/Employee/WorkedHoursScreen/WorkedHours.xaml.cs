using EmployeeMaster.EmployeeViewModel;
using EmployeeMaster.Model;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Employee.WorkedHoursScreen
{
    public partial class WorkedHours : UserControl
    {
        private readonly WorkHourViewModel _viewModel;

        public WorkedHours()
        {
            InitializeComponent();
            int employeeId = CurrentUser.Instance.IdUser;
            _viewModel = new WorkHourViewModel();
            DataContext = _viewModel;
            _viewModel.LoadWorkHours(employeeId);
            WorkHoursList.ItemsSource = _viewModel.WorkHours;
        }

        private void OnShowAllClick(object sender, RoutedEventArgs e)
        {
            WorkHoursList.ItemsSource = _viewModel.WorkHours;
        }

        private void OnShowFilteredClick(object sender, RoutedEventArgs e)
        {
            var selectedDate = FilterDatePicker.SelectedDate;

            if (selectedDate.HasValue)
            {
                
                DateOnly dateToFilter = DateOnly.FromDateTime(selectedDate.Value);

                _viewModel.FilterWorkHoursByDate(dateToFilter);
                WorkHoursList.ItemsSource = _viewModel.FilteredWorkHours;
            }
        }

    }
}
