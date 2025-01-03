using EmployeeMaster.Model;
using EmployeeMaster.Services;
using EmployeeMaster.Services.EmployeeMaster.Services;
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
        private readonly SettingService _settingService;
      
        private UserModel _user;

        public UserViewModel()
        {
            int userId = CurrentUser.Instance.IdUser;
            _userService = new UserService();
            _settingService = new SettingService();
            LoadUserInfo(userId); 
        }


        public string FullName => _user?.FullName;
        public string Email => _user?.Email;
        public string Username => _user?.Username;
        public string Phone => _user?.Phone;
        public string ProfileImage => _user?.Picture;
        public string date => _user?.dateOfBirth;
        public string language;
        public string usernameText;

        private void LoadUserInfo(int userId)
        {
            _user = _userService.GetUserInfo(userId);
            language=_settingService.GetSettingsByUserId(userId).Language;
           
       
        }

      
    }
}
