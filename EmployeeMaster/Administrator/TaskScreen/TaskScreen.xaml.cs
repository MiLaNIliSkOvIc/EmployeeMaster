using EmployeeMaster.Administrator.DashBoardView;
using EmployeeMaster.AdministratorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.NotificationDisplay;
using EmployeeMaster.YesNoWindow;

namespace EmployeeMaster.Administrator.TaskScreen
{
    public partial class TaskScreen : UserControl
    {
        private TaskViewModel viewModel;

        public TaskScreen()
        {
            InitializeComponent();
            viewModel = new TaskViewModel();
            DataContext = viewModel;
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }

        
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewTask = new AddNewTask();
            if (addNewTask.ShowDialog() == true)
            {

            }
            viewModel.LoadTasks();
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }

  
        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem is Model.Task selectedTask)
            {
                EditTask edit = new EditTask(selectedTask);
                if (edit.ShowDialog() == true)
                {

                }

                viewModel.LoadTasks();
                TaskDataGrid.ItemsSource = viewModel.FilteredTasks;

            }
           
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }


        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = TaskDataGrid.SelectedItem as Model.Task;

            if (selectedTask != null)
            {
                string message = "Are you sure you want to delete this task?";
                var dialog = new YesNoDialog(message);
                var result = dialog.ShowDialog();

                if (result == true)
                {

                    viewModel.DeleteTask(selectedTask);
                }
               
            }
       
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox)?.Text;
            viewModel.FilterTasks(searchText);
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }
    }
}
