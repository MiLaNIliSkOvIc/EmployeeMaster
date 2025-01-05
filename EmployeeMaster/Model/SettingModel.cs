using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Model
{
    public class SettingModel
    {
        public int UserId { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }

        public SettingModel() {
            UserId = CurrentUser.Instance.IdUser;
            Theme = "Styles1";
            Language="en-US";
        }
        public SettingModel(int userId)
        {
            UserId = userId;
            Theme = "Styles1";
            Language = "en-US";
        }
    }
}
