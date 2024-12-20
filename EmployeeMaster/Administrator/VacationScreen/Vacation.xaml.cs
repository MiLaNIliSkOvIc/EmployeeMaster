using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace EmployeeMaster.Administator.VacationScreen
{
    public partial class Vacation : UserControl
    {
        public ObservableCollection<VacationRequest> VacationRequests { get; set; }

        public Vacation()
        {
            InitializeComponent();
            LoadVacationRequests();
            DataContext = this;
        }

        private void LoadVacationRequests()
        {
           
            VacationRequests = new ObservableCollection<VacationRequest>
            {
                new VacationRequest { FirstName = "John", LastName = "Doe", StartDate = "2024-12-12", EndDate = "2024-12-21", Status = "Pending" },
                new VacationRequest { FirstName = "Jane", LastName = "Smith", StartDate = "2024-12-12", EndDate ="2024-12-12", Status = "Accept" }
            };

         
            VacationDataGrid.ItemsSource = VacationRequests;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var request = button.DataContext as VacationRequest;
                if (request != null)
                {
                    request.Status = "Accepted";
       
                }
            }
        }

        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var request = button.DataContext as VacationRequest;
                if (request != null)
                {
                    request.Status = "Denied";
                    
                }
            }
        }
    }
    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status && parameter is string expectedStatus)
            {
                return status == expectedStatus ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class VacationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
