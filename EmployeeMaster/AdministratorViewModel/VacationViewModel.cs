using System.Collections.ObjectModel;
using System.ComponentModel;
using EmployeeMaster.Services;
using EmployeeMaster.Model;
using EmployeeMaster.Administator.VacationScreen;

namespace EmployeeMaster.AdministratorViewModel
{
    public class VacationViewModel : INotifyPropertyChanged
    {
        private readonly VacationService vacationService;
        private ObservableCollection<Model.Vacation> vacationRequests;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Model.Vacation> VacationRequests
        {
            get { return vacationRequests; }
            set
            {
                vacationRequests = value;
                OnPropertyChanged(nameof(VacationRequests));
            }
        }

        public VacationViewModel()
        {
            vacationService = new VacationService();
            LoadVacationRequests();
        }

        private void LoadVacationRequests()
        {
            var requests = vacationService.GetVacationRequests();
            VacationRequests = new ObservableCollection<Model.Vacation>(requests);
        }

        public void AcceptRequest(Model.Vacation request)
        {
            vacationService.AcceptRequest(request.VacationRequestId);
            LoadVacationRequests(); 
        }

        public void DenyRequest(Model.Vacation request)
        {
            vacationService.DenyRequest(request.VacationRequestId);
            LoadVacationRequests(); 
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void FilterVacationRequests(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                
                VacationRequests = new ObservableCollection<Model.Vacation>(vacationService.GetVacationRequests());
            }
            else
            {
                
                VacationRequests = new ObservableCollection<Model.Vacation>(
                    vacationService.GetVacationRequests().Where(request =>
                        request.FirstName.ToLower().Contains(searchText.ToLower()) ||
                        request.LastName.ToLower().Contains(searchText.ToLower()) ||
                        request.Status.ToLower().Contains(searchText.ToLower()))
                );
            }
        }



    }
}
