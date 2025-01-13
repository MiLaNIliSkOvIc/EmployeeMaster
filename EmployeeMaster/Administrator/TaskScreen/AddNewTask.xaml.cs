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

namespace EmployeeMaster.Administrator.TaskScreen
{
    public partial class AddNewTask : Window
    {
        private readonly EmployeeService _employeeService;
        private readonly TaskService _taskService;

        public AddNewTask()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _taskService = new TaskService();
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
            LoadPriorities();
        }

        public string TaskName => TaskNameTextBox.Text;
        public string Description => DescriptionTextBox.Text;
        public DateTime? DueDate => DueDatePicker.SelectedDate;
        public int? AssignedEmployeeId => EmployeeComboBox.SelectedValue as int?;
        public string Priority => (PriorityComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

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
        private void LoadPriorities()
        {
           
            PriorityComboBox.SelectedIndex = 0; 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskName) ||
                string.IsNullOrWhiteSpace(Description) ||
                DueDate == null ||
                AssignedEmployeeId == null ||
                string.IsNullOrWhiteSpace(Priority) || DueDate.Value < DateTime.Now)
            {
                NotificationWindow notif = new NotificationWindow("Please fill in all required fields correctly.");
                notif.Show();

                return;
            }

            try
            {
                _taskService.AddTask(task: new Model.Task(
                    TaskName,
                    "Pending",
                    Priority,
                    Description,
                    DueDate.Value,
                    AssignedEmployeeId.Value


                    ) 
                );
             
                NotificationWindow notif = new NotificationWindow("Task added successfully");
                notif.Show();
            }
            catch (Exception ex)
            {
                
                NotificationWindow notif = new NotificationWindow("Error saving taks");
                notif.Show();
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
