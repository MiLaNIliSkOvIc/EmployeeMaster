using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using EmployeeMaster.EmployeeViewModel;
using Microsoft.Win32;

namespace EmployeeMaster.Employee.PersonalInfoScreen
{
    public partial class PersonalInfo : UserControl
    {
        private readonly UserViewModel _userViewModel;

        public PersonalInfo()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
            DataContext = _userViewModel; 

            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
           // string picturePath = _userViewModel.ProfileImage ?? "C:/Users/pc/Desktop/picturesOfCars/profile.jpg";
            string picturePath =  "C:/Users/pc/Desktop/picturesOfCars/profile.jpg";
            ProfileImage.Source = new BitmapImage(new Uri(picturePath));
            FullNameText.Text = _userViewModel.FullName;        
            EmailText.Text = _userViewModel.Email;              
            UsernameText.Text = _userViewModel.Username;        
            PhoneText.Text = _userViewModel.Phone;              
        }
    }
}
