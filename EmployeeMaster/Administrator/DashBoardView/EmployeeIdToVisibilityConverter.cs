using EmployeeMaster.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeMaster.Administator.DashBoardView
{
    public class EmployeeIdToVisibilityConverter : IValueConverter
    {
        
        public EmployeeIdToVisibilityConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var employeeId = value?.ToString();


            return employeeId != CurrentUser.Instance.IdUser.ToString() ? Visibility.Visible : Visibility.Collapsed;
            //return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
