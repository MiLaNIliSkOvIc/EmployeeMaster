using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.AdministratorViewModel;

using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administrator.WorkedHoursScreen
{
    public partial class WorkedHours : UserControl
    {
        private readonly WorkHourViewModel _viewModel;

        public WorkedHours()
        {
            InitializeComponent();
            
            _viewModel = new WorkHourViewModel();
            DataContext = _viewModel;
            _viewModel.LoadWorkHours();
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
