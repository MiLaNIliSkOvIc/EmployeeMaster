﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeMaster.Administator.VacationScreen
{
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
}
