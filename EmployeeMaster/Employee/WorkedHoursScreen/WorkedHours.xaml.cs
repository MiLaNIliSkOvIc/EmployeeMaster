using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMaster.Services; // Dodajte referencu na servis
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeMaster.Employee.WorkedHoursScreen
{
    public class WorkedHourEntry
    {
        public string Date { get; set; }
        public string Start { get; set; }
        public string Hour { get; set; }
        public string Lunch { get; set; }
        public string Shift { get; set; }
    }

    public partial class WorkedHours : Window
    {
        public ObservableCollection<WorkedHourEntry> WorkedHoursList { get; set; }

        public WorkedHours()
        {
            InitializeComponent();
            DynamicCard.Content = "04:52:24";

            WorkedHoursList = new ObservableCollection<WorkedHourEntry>
        {
            new WorkedHourEntry { Date = "2024-12-01", Start = "08:00", Hour = "8", Lunch = "30 min", Shift = "Morning" },
            new WorkedHourEntry { Date = "2024-12-02", Start = "09:00", Hour = "8", Lunch = "1 hour", Shift = "Day" },
            new WorkedHourEntry { Date = "2024-12-03", Start = "14:00", Hour = "8", Lunch = "30 min", Shift = "Evening" }
        };

            // Bind the data to the ListBox
            WorkedHoursListBox.ItemsSource = WorkedHoursList;
        }
    }


}
