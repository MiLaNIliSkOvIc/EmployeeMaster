using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeMaster.Model;
using EmployeeMaster.Services;
using System;


namespace EmployeeMaster.AdministratorViewModel
{
    public class SettingsViewModel
    {
        private readonly SettingService _settingService;

        public SettingModel CurrentSettings { get; private set; }

        public SettingsViewModel(int userId)
        {
            _settingService = new SettingService();
            LoadSettings(userId);
        }

        public void LoadSettings(int userId)
        {
            CurrentSettings = _settingService.GetSettingsByUserId(userId);
        }

        public void SaveSettings(int userId, SettingModel setting)
        {
            _settingService.UpdateSettings(userId, setting);
            LoadSettings(userId);  
        }
    }
}


