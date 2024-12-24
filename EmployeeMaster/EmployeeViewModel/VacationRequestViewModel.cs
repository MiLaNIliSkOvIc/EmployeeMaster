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

        public void AcceptVacationRequest(Vacation vacation)
        {
            if (vacation != null)
            {
                _vacationService.AcceptRequest(vacation.VacationRequestId);
                LoadVacations();
            }
        }

        public void DenyVacationRequest(Vacation vacation)
        {
            if (vacation != null)
            {
                _vacationService.DenyRequest(vacation.VacationRequestId);
                LoadVacations();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
