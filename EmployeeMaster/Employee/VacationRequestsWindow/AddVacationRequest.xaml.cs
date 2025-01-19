using System;
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
            if (!FromDatePicker.SelectedDate.HasValue || !ToDatePicker.SelectedDate.HasValue)
            {            
                NotificationWindow notif = new NotificationWindow("Please select both start and end dates.");
                notif.Show();
                return;
            }

            DateTime fromDate = FromDatePicker.SelectedDate.Value;
            DateTime toDate = ToDatePicker.SelectedDate.Value;

            if (fromDate < DateTime.Today)
            {
                NotificationWindow notif = new NotificationWindow("Start date cannot be earlier than today.");
                notif.Show();
                return;
            }

            if (toDate < fromDate)
            {
                NotificationWindow notif = new NotificationWindow("End date cannot be earlier than start date.");
                notif.Show();
                return;
            }

            try
            {
                _vacationService.AddVacationRequest(new Model.Vacation(
                  
                    CurrentUser.Instance.IdUser,
                    FromDate.Value.ToString("yyyy-MM-dd"),  
                    ToDate.Value.ToString("yyyy-MM-dd"),    
                    "Pending")
                    {
                });

                string message = "Vacation added succesfully";
                
                NotificationWindow notif = new NotificationWindow(message);
                notif.Show();

            }
            catch (Exception ex)
            {
                
                
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
