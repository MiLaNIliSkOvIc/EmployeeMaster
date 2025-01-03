using EmployeeMaster.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmployeeMaster.Model;
using EmployeeMaster.Administrator.VacationRequestsWindow;

namespace EmployeeMaster.EmployeeViewModel
{
    public class VacationRequestsViewModel : INotifyPropertyChanged
    {
        private readonly VacationService _vacationService;
        private ObservableCollection<Vacation> _vacationRequests;
        private int _employeeId;

        public ObservableCollection<Vacation> VacationRequests
        {
            get => _vacationRequests;
            set
            {
                _vacationRequests = value;
                OnPropertyChanged();
            }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged();
            }
        }

        public VacationRequestsViewModel(int employeeId)
        {
            _vacationService = new VacationService();
            EmployeeId = employeeId;
            VacationRequests = new ObservableCollection<Vacation>();
            LoadVacations();
        }
        public void addVacation()
        {
            var addNewVacation = new AddNewVacation();
            if (addNewVacation.ShowDialog() == true)
            {

            }
            LoadVacations();
            
        }
        public void LoadVacations()
        {
            try
            {
                var vacationList = _vacationService.GetVacationsByEmployeeId(EmployeeId);
                VacationRequests = new ObservableCollection<Vacation>(vacationList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dohvaćanju podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteVacationRequest(Vacation selectedVacation)
        {
            try
            {
             
                VacationRequests.Remove(selectedVacation);
                var vacationService = new VacationService();
                vacationService.DeleteVacationRequest(selectedVacation.VacationRequestId);

                MessageBox.Show("Vacation request deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
           
                MessageBox.Show("Cant delete it");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
