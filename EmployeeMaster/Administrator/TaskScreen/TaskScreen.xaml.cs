using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Administrator.TaskScreen
{
    public partial class TaskScreen : UserControl
    {
        private List<Task> allTasks; 
        private List<Task> filteredTasks; 

        public TaskScreen()
        {
            InitializeComponent();

    
            allTasks = GetTasks();
            filteredTasks = new List<Task>(allTasks); 
            TaskDataGrid.ItemsSource = filteredTasks;
        }

        private List<Task> GetTasks()
        {
            return new List<Task>
            {
                new Task { TaskName = "Task 1", AssignedTo = "John Doe", DueDate = "2024-12-12", Status = "Pending" },
                new Task { TaskName = "Task 2", AssignedTo = "Jane Smith", DueDate = "2025-02-12", Status = "In Progress" },
                new Task { TaskName = "Task 3", AssignedTo = "Emily Davis", DueDate = "2024-11-10", Status = "Completed" },
                  new Task { TaskName = "Task 3", AssignedTo = "Emily Davis", DueDate = "2024-11-10", Status = "Completed" },
                    new Task { TaskName = "Task 3", AssignedTo = "Emily Davis", DueDate = "2024-11-10", Status = "Completed" },
                new Task { TaskName = "Task 4", AssignedTo = "Michael Brown", DueDate = "2024-12-20", Status = "Pending" }
            };
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox)?.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                
                filteredTasks = new List<Task>(allTasks);
            }
            else
            {
                
                filteredTasks = allTasks.Where(task =>
                    task.TaskName.ToLower().Contains(searchText) ||
                    task.AssignedTo.ToLower().Contains(searchText) ||
                    task.Status.ToLower().Contains(searchText)).ToList();
            }

            TaskDataGrid.ItemsSource = filteredTasks;
        }
    }

    public class Task
    {
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
    }
}
