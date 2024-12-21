using System;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EmployeeMaster.Employee.WorkedHoursScreen
{
    public partial class WorkedHours : UserControl
    {
        // Kolekcija za sve radne sate
        public ObservableCollection<WorkHour> WorkHours { get; set; }

        // Kolekcija za filtrirane radne sate
        public ObservableCollection<WorkHour> FilteredWorkHours { get; set; }

        public WorkedHours()
        {
            InitializeComponent();
            WorkHours = new ObservableCollection<WorkHour>();
            FilteredWorkHours = new ObservableCollection<WorkHour>();

           
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddHours(-8), FinishDate = DateTime.Now.AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddHours(-6), FinishDate = DateTime.Now.AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-3).AddHours(-8), FinishDate = DateTime.Now.AddDays(-3).AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-3).AddHours(-6), FinishDate = DateTime.Now.AddDays(-3).AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });

            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-2).AddHours(-8), FinishDate = DateTime.Now.AddDays(-2).AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-2).AddHours(-6), FinishDate = DateTime.Now.AddDays(-2).AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });

            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-1).AddHours(-8), FinishDate = DateTime.Now.AddDays(-1).AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-1).AddHours(-6), FinishDate = DateTime.Now.AddDays(-1).AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });

            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-5).AddHours(-8), FinishDate = DateTime.Now.AddDays(-5).AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-5).AddHours(-6), FinishDate = DateTime.Now.AddDays(-5).AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });

            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-4).AddHours(-8), FinishDate = DateTime.Now.AddDays(-4).AddHours(-4), HoursWorked = 4, Shift = "Morning" });
            WorkHours.Add(new WorkHour { StartDate = DateTime.Now.AddDays(-4).AddHours(-6), FinishDate = DateTime.Now.AddDays(-4).AddHours(-2), HoursWorked = 4, Shift = "Afternoon" });


            WorkHoursList.ItemsSource = WorkHours;
        }

        
        private void OnShowAllClick(object sender, RoutedEventArgs e)
        {
            WorkHoursList.ItemsSource = WorkHours;
        }

        
        private void OnShowFilteredClick(object sender, RoutedEventArgs e)
        {
            var selectedDate = FilterDatePicker.SelectedDate;

            if (selectedDate.HasValue)
            {
                var filtered = WorkHours.Where(x => x.StartDate.Date == selectedDate.Value.Date).ToList();
                FilteredWorkHours.Clear();
                foreach (var workHour in filtered)
                {
                    FilteredWorkHours.Add(workHour);
                }

                WorkHoursList.ItemsSource = FilteredWorkHours;
            }
        }
    }


    public class WorkHour
    {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double HoursWorked { get; set; }
        public string Shift { get; set; }
    }
}
