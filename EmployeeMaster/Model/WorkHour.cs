using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Model
{
    public class WorkHour
    {
        public int Id { get; set; } 
        public TimeSpan StartDate { get; set; }
        public TimeSpan? FinishDate { get; set; }
        public DateOnly Date { get; set; }
        public string Shift { get; set; }
        public int EmployeeId { get; set; }
        public string fullName { get; set; }
        public TimeSpan? HoursWorked
        {
            get
            {
                return FinishDate - StartDate;
            }
        }

    }
}
