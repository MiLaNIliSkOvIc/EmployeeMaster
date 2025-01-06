using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.Model;
using EmployeeMaster.Services.EmployeeMaster.Services;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmployeeMaster.Services;
using System.Text.RegularExpressions;
using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.NotificationDisplay;
using Google.Protobuf.WellKnownTypes;
using System.Numerics;
using EmployeeMaster.AdministratorViewModel;
using Microsoft.VisualBasic;

namespace EmployeeMaster.Administrator.TaskScreen
{
    public partial class EditTask : Window
    {
        private readonly EmployeeService _employeeService;
        private readonly TaskService _taskService;
        private readonly TaskViewModel taskViewModel;
        Model.Task editTask;

        public EditTask(Model.Task selectedTask)
        {
            editTask = selectedTask;
            InitializeComponent();
            _employeeService = new EmployeeService();
            _taskService = new TaskService();
            taskViewModel = new TaskViewModel();
            var newResourceDictionary = new ResourceDictionary
            {
                Source = new Uri($"../../Styles/{MainScreen.style}.xaml", UriKind.RelativeOrAbsolute)

            };
            var newResourceDictionary2 = new ResourceDictionary
            {

                Source = new Uri($"../../Language/Resources.{MainScreen.language}.xaml", UriKind.RelativeOrAbsolute)
            };
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResourceDictionary);
            this.Resources.MergedDictionaries.Add(newResourceDictionary2);
            LoadEmployees();
     

            TaskNameTextBox.Text = editTask.TaskName;
            DescriptionTextBox.Text = editTask.Description;
            DueDatePicker.SelectedDate = editTask.DueDate;
            EmployeeComboBox.SelectedValue = editTask.AssignedToId;
          

        }

        public string TaskName => TaskNameTextBox.Text;
        public string Description => DescriptionTextBox.Text;
        public DateTime? DueDate => DueDatePicker.SelectedDate;
        public int? AssignedEmployeeId => EmployeeComboBox.SelectedValue as int?;
        

        private void LoadEmployees()
        {
            try
            {
                var employees = _employeeService.GetEmployees();
                EmployeeComboBox.ItemsSource = employees;
                EmployeeComboBox.DisplayMemberPath = "FullName";
                EmployeeComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {

                NotificationWindow notif = new NotificationWindow("Error loading employees");
                notif.Show();

            }
        }

        private void DueDatePicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            string datePattern = @"^(0[1-9]|1[0-2])\/([0-2][1-9]|3[01])\/\d{4}$";
            Regex regex = new Regex(datePattern);

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
     

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            editTask.TaskName = TaskName;
            editTask.Description = Description;
           
             editTask.DueDate = DueDate.Value;
            editTask.AssignedToId = AssignedEmployeeId.Value;
           
            taskViewModel.EditTask(editTask);
            NotificationWindow notif = new NotificationWindow("Task updated successfully.");
            notif.Show();

         
         //   DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
