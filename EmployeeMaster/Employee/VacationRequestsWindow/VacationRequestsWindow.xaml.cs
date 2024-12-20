﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeMaster.Employee
{
    public partial class VacationRequestsScreen : UserControl
    {
        // Collection to hold the vacation requests data
        public ObservableCollection<Model.Vacation> VacationRequestsList { get; set; }

        public VacationRequestsScreen()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
        }

        private void LoadData()
        {
            // Sample data for vacation requests
            VacationRequestsList = new ObservableCollection<Model.Vacation>
            {
                new Model.Vacation { Id = 1, StartDate = "2024-12-20", EndDate = "2024-12-25", Status = "Approved" },
                new Model.Vacation { Id = 2, StartDate = "2025-01-10", EndDate = "2025-01-15", Status = "Pending" },
                new Model.Vacation { Id = 3, StartDate = "2025-02-01", EndDate = "2025-02-05", Status = "Rejected" }
            };
        }

        // Filter button click handler
        private void FilterTable_Click(object sender, RoutedEventArgs e)
        {
            if (FilterDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = FilterDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                var filteredList = VacationRequestsList
                    .Where(v => v.StartDate == selectedDate || v.EndDate == selectedDate)
                    .ToList();
                VacationRequestsTable.ItemsSource = filteredList;
            }
            else
            {
                MessageBox.Show("Please select a date to filter.", "Filter Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Add new vacation request button click handler
        private void AddNewRequest_Click(object sender, RoutedEventArgs e)
        {
            var newRequest = new Model.Vacation
            {
                Id = VacationRequestsList.Count + 1,
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"),
                Status = "Pending"
            };

            VacationRequestsList.Add(newRequest);
            MessageBox.Show("New vacation request added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
