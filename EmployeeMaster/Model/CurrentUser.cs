using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Model
{
    public class CurrentUser
    {
        private static CurrentUser _instance;
        private static readonly object _lock = new object();

        public int IdUser { get; private set; }
  
        public string Username { get; private set; }
        public string Role { get; private set; }


        private CurrentUser() { }

        public static CurrentUser Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CurrentUser();
                    }
                    return _instance;
                }
            }
        }

        public void SetUser(int idUser, string username, string role)
        {
            IdUser = idUser;
          
            Username = username;
            Role = role;
        }
    }

}
