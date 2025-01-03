﻿using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.EmployeeViewModel;
using EmployeeMaster.ErrorDisplay;
using EmployeeMaster.Model;

namespace EmployeeMaster.Employee.TaskScreen
{
    public partial class EmployeeTaskScreen : UserControl
    {
        private readonly EmployeeTaskViewModel _viewModel;

        public EmployeeTaskScreen()
        {
              int employeeId = CurrentUser.Instance.IdUser; 
            InitializeComponent();
            _viewModel = new EmployeeTaskViewModel(employeeId);
            DataContext = _viewModel;
            TasksDataGrid.ItemsSource = _viewModel.Tasks;
        }

        private void OnDoneClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
                _viewModel.MarkTaskAsDone(selectedTask);
             
                ErrorWindow win = new ErrorWindow($"Task '{selectedTask.TaskName}' marked as Done.");
                win.Show();
                TasksDataGrid.ItemsSource = _viewModel.Tasks;

            }
            else
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnDescriptionClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
              
                ErrorWindow win = new ErrorWindow(selectedTask.Description);
                win.Show(); 
            }


        }
        private void OnApplyFiltersClick(object sender, RoutedEventArgs e)
        {
            string selectedStatus = (FilterStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime? selectedDate = FilterDatePicker.SelectedDate;

            _viewModel.ApplyFilters(selectedStatus, selectedDate);
            TasksDataGrid.ItemsSource = _viewModel.Tasks;
        }

    }
}
