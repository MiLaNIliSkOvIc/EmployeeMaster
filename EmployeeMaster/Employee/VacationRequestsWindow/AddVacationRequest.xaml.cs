﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
using EmployeeMaster.Administator.MainScreen;
using EmployeeMaster.Employee.EmployeeMainScreen;
using EmployeeMaster.NotificationDisplay;
using System.Resources;

namespace EmployeeMaster.Administrator.VacationRequestsWindow
{
    public partial class AddNewVacation : Window
    {
        private readonly EmployeeService _employeeService;
        private readonly VacationService _vacationService;

        public AddNewVacation()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _vacationService = new VacationService();
            var newResourceDictionary = new ResourceDictionary
            {
                Source = new Uri($"../../Styles/{EmployeeMainScreen.style}.xaml", UriKind.RelativeOrAbsolute)

            };
            var newResourceDictionary2 = new ResourceDictionary
            {

                Source = new Uri($"../../Language/Resources.{EmployeeMainScreen.language}.xaml", UriKind.RelativeOrAbsolute)
            };
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResourceDictionary);
            this.Resources.MergedDictionaries.Add(newResourceDictionary2);
        }

       
        public DateTime? FromDate => FromDatePicker.SelectedDate;
        public DateTime? ToDate => ToDatePicker.SelectedDate;

       

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                _vacationService.AddVacationRequest(new Model.Vacation(
                  
                    CurrentUser.Instance.IdUser,
                    FromDate.Value.ToString("yyyy-MM-dd"),  
                    ToDate.Value.ToString("yyyy-MM-dd"),    
                    "Pending")
                    {
                });

                string message = this.Resources["Addedsuccessfully"]?.ToString();
                
                NotificationWindow notif = new NotificationWindow(message);
                notif.Show();

            }
            catch (Exception ex)
            {
                
                string message = this.Resources["ErrorSaving"]?.ToString();
                NotificationWindow notif = new NotificationWindow($"Error saving vacation: {ex.Message}");
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
