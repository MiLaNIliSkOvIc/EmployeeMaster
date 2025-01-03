using EmployeeMaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMaster.Model;

namespace EmployeeMaster.AdministratorViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly TaskService taskService;
        private readonly UserService _userService;
        private List<Model.Task> allTasks;
        private ObservableCollection<Model.Task> filteredTasks;
        public ObservableCollection<UserModel> Users { get; set; }


       

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskViewModel()
        {
            taskService = new TaskService();
            _userService = new UserService();
            Users = new ObservableCollection<UserModel>(_userService.GetAllUsers());
            LoadTasks();
        }

        public ObservableCollection<Model.Task> FilteredTasks
        {
            get { return filteredTasks; }
            set
            {
                filteredTasks = value;
                OnPropertyChanged(nameof(FilteredTasks));
            }
        }

       
        public void LoadTasks()
        {
            allTasks = taskService.GetTasks();
            FilteredTasks = new ObservableCollection<Model.Task>(allTasks);
        }

        
        public void FilterTasks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                FilteredTasks = new ObservableCollection<Model.Task>(allTasks);
            }
            else
            {
                FilteredTasks =new ObservableCollection<Model.Task>(allTasks.Where(task =>
                    task.TaskName.ToLower().Contains(searchText.ToLower()) ||
                    task.AssignedTo.ToLower().Contains(searchText.ToLower()) ||
                    task.Status.ToLower().Contains(searchText.ToLower())).ToList());
            }
        }

        
        public void AddTask(Model.Task newTask)
        {
            taskService.AddTask(newTask);
            LoadTasks(); 
        }

        
        public void EditTask(Model.Task taskToEdit)
        {
            taskService.EditTask(taskToEdit);
            LoadTasks(); 
        }

        
        public void DeleteTask(Model.Task taskToDelete)
        {
            taskService.DeleteTask(taskToDelete.TaskId);
            if (FilteredTasks.Contains(taskToDelete))
            {
                FilteredTasks.Remove(taskToDelete);
            }

        }

       
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
