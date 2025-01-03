using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Model
{
    public class UserModel
    {
        public int IdUser { get; set; }
    
        public string FullName{ get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; }
        public string dateOfBirth { get; set; }
    }
}
