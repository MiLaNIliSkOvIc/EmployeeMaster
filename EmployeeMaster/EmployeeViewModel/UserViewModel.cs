using EmployeeMaster.Model;
using EmployeeMaster.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EmployeeMaster.EmployeeViewModel
{
    public class UserViewModel
    {
        private readonly UserService _userService;
        private UserModel _user;

        public UserViewModel()
        {
            int userId = 1;
            _userService = new UserService();
            LoadUserInfo(userId); 
        }


        public string FullName => _user?.FullName;
        public string Email => _user?.Email;
        public string Username => _user?.Username;
        public string Phone => _user?.Phone;
        public string ProfileImage => _user?.Picture;

        private void LoadUserInfo(int userId)
        {
            _user = _userService.GetUserInfo(userId);
       
        }

      
    }
}
