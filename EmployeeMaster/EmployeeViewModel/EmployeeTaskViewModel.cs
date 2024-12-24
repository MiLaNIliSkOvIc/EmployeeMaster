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
                Tasks = new ObservableCollection<Model.Task>(taskList);
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
    }
}
