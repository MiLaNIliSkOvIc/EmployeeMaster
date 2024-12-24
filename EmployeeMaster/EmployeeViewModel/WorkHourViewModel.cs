using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMaster.Model;
using EmployeeMaster.Services;
using System.Collections.ObjectModel;


namespace EmployeeMaster.EmployeeViewModel
{


        public class WorkHourViewModel
        {
            private readonly WorkHourService _workHourService;

            public ObservableCollection<WorkHour> WorkHours { get; set; }
            public ObservableCollection<WorkHour> FilteredWorkHours { get; set; }

            public WorkHourViewModel()
            {
                _workHourService = new WorkHourService();
                WorkHours = new ObservableCollection<WorkHour>();
                FilteredWorkHours = new ObservableCollection<WorkHour>();
            }

            public void LoadWorkHours(int employeeId)
            {
                var workHours = _workHourService.GetWorkHoursByEmployeeId(employeeId);
                WorkHours.Clear();

                foreach (var workHour in workHours)
                {
                    WorkHours.Add(workHour);
                }
            }

        public void FilterWorkHoursByDate(DateOnly date)
        {
            FilteredWorkHours.Clear();

            for (int i = 0; i < WorkHours.Count; i++)
            {
                var workHour = WorkHours[i];

                
                if (workHour.Date.Day == date.Month && workHour.Date.Month == date.Day && workHour.Date.Year == date.Year)
                {
                    FilteredWorkHours.Add(workHour); 
                }
            }
        }





    }


}
