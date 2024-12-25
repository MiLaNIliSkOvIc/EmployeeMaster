using EmployeeMaster.AdministratorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
          
        }

  
        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem is Model.Task selectedTask)
            {
                viewModel.EditTask(selectedTask); 
                MessageBox.Show("Task updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            TaskDataGrid.ItemsSource = viewModel.FilteredTasks;
        }

      
        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = TaskDataGrid.SelectedItem as Model.Task;
            if (selectedTask != null)
            {
                viewModel.DeleteTask(selectedTask);
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
