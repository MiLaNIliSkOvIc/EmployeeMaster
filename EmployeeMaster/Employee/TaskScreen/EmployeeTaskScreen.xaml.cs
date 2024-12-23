using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.Services;
using EmployeeMaster.Model;

namespace EmployeeMaster.Employee.TaskScreen
{
    public partial class EmployeeTaskScreen : UserControl
    {
        public ObservableCollection<Model.Task> Tasks { get; set; }
        private readonly TaskService _taskService;

        public EmployeeTaskScreen()
        {
            InitializeComponent();
            string connectionString = "Server=127.0.0.1;Database=projectmanagement;User Id=root;Password=02100078;";
           // _taskService = new TaskService(connectionString);
            LoadTasks();
            TasksDataGrid.ItemsSource = Tasks;
           
        }

        private void LoadTasks()
        {
            try
            {
               // var taskList = _taskService.GetAllTasks();
               /// Tasks = new ObservableCollection<Model.Task>(taskList);
                // Refresh DataGrid binding after setting Tasks
                TasksDataGrid.ItemsSource = Tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dohvaćanju podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method for the "Done" button click event
        private void OnDoneClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
              //  MessageBox.Show($"Task '{selectedTask.Title}' marked as Done.", "Task Update", MessageBoxButton.OK, MessageBoxImage.Information);
                // Here, you could implement logic to update the task's status in the database
            }
            else
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Method for the "Description" button click event
        private void OnDescriptionClick(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
                MessageBox.Show($"Task Description:\n{selectedTask.Description}", "Task Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}