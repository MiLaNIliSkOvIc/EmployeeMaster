using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.EmployeeViewModel;
using EmployeeMaster.NotificationDisplay;
using EmployeeMaster.Model;
using EmployeeMaster.YesNoWindow;

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
            TotalTasksTextBlock.Text = _viewModel.Tasks.Count.ToString();
            CompletedTasksTextBlock.Text = _viewModel.completedTask.ToString();
        }

        private void OnDoneClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
                
                var confirmationDialog = new YesNoDialog(
                    "Are you sure you want to mark this task as Done?"
                 );
                var result = confirmationDialog.ShowDialog();

                if (result == true) 
                {
                    _viewModel.MarkTaskAsDone(selectedTask); 

               
                    NotificationWindow win = new NotificationWindow($"Task marked as Done.");
                    win.Show();

                  
                    TasksDataGrid.ItemsSource = _viewModel.Tasks;
                    CompletedTasksTextBlock.Text = _viewModel.completedTask.ToString();
                }
         
            }
        }


        private void OnDescriptionClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {

                NotificationWindow win = new NotificationWindow(selectedTask.Description);
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
