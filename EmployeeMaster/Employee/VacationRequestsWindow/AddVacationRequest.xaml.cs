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


                MessageBox.Show("Vacation added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vacation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
