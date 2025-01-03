using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Model
{
    public class Vacation
    {
        public int VacationRequestId { get; set; }  
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string StartDate { get; set; }  
        public string EndDate { get; set; }  
        public string Status { get; set; }
        public Vacation()
        { }
            public Vacation(int employeeId, string startDate, string endDate, string status)
        {
            EmployeeId = employeeId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }

}