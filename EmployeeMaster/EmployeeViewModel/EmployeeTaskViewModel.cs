using EmployeeMaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeMaster.EmployeeViewModel
{
    public class EmployeeTaskViewModel
    {
        private readonly TaskService _taskService;
        public ObservableCollection<Model.Task> Tasks { get; private set; }
        int employeeId { get; set; }
        public int completedTask = 0;
        public EmployeeTaskViewModel(int employeeId)
        {
            _taskService = new TaskService();
            Tasks = new ObservableCollection<Model.Task>();
            this.employeeId = employeeId;
            LoadTasks();
        }

        public void LoadTasks()
        {
            try
            {
                var taskList = _taskService.GetTasksByEmployeeId(employeeId);
                var sortedTasks = taskList.OrderBy(task => task.Status != "In Progress").ToList();
                Tasks = new ObservableCollection<Model.Task>(sortedTasks);
              
                completedTask = taskList.Count(task => task.Status == "Completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dohvaćanju podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MarkTaskAsDone(Model.Task task)
        {
            if (task != null)
            {
                task.Status = "Completed";
                _taskService.EditTask(task);
                LoadTasks();


            }
        }
        public void ApplyFilters(string status, DateTime? date)
        {
            try
            {
                var allTasks = _taskService.GetTasksByEmployeeId(employeeId);

                if (!string.IsNullOrEmpty(status) && status != "All")
                {
                    allTasks = allTasks.Where(task => task.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
                }
    
                if (date.HasValue)
                {
                    allTasks = allTasks.Where(task => task.DueDate.Date == date.Value.Date).ToList();
                }
                Tasks = new ObservableCollection<Model.Task>(allTasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri filtriranju zadataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
