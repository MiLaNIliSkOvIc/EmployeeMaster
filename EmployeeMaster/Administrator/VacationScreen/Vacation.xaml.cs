﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EmployeeMaster.AdministratorViewModel;
using EmployeeMaster.Model;
using EmployeeMaster.YesNoWindow;

namespace EmployeeMaster.Administator.VacationScreen
{
    public partial class Vacation : UserControl
    {
        public ObservableCollection<Model.Vacation> VacationRequests { get; set; }
        private VacationViewModel viewModel;

        public Vacation()
        {
            InitializeComponent();
            viewModel = new VacationViewModel();
            DataContext = viewModel;
            LoadVacationRequests();
        }

        private void LoadVacationRequests()
        {
            
            
            VacationDataGrid.ItemsSource = viewModel.VacationRequests; ;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var request = button.DataContext as Model.Vacation;
                if (request != null)
                {
                    
                    var confirmationDialog = new YesNoDialog(
                        "Are you sure you want to accept this vacation request?"
                        );
                    var result = confirmationDialog.ShowDialog();

                    if (result == true)
                    {
                        viewModel.AcceptRequest(request); 
                        VacationDataGrid.ItemsSource = viewModel.VacationRequests; 
                    }
                   
                }
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchText = (sender as TextBox)?.Text;
            viewModel.FilterVacationRequests(searchText);
            VacationDataGrid.ItemsSource = viewModel.VacationRequests;
        }
        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var request = button.DataContext as Model.Vacation;
                if (request != null)
                {
                   
                    var confirmationDialog = new YesNoDialog(
                        "Are you sure you want to deny this vacation request?"
                       );
                    var result = confirmationDialog.ShowDialog();

                    if (result == true) 
                    {
                        viewModel.DenyRequest(request); 
                        VacationDataGrid.ItemsSource = viewModel.VacationRequests; 
                    }
                 
                }
            }
        }
    }
}
