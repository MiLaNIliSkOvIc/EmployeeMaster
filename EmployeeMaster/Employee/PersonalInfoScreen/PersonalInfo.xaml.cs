using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace EmployeeMaster.Employee.PersonalInfoScreen
{
    public partial class PersonalInfo : UserControl
    {
        public PersonalInfo()
        {
            InitializeComponent();
            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
            // Simulating loading data
            ProfileImage.Source = new BitmapImage(new Uri("C:/Users/pc/Desktop/picturesOfCars/profile.jpg"));
            FullNameText.Text = "Milan Ilišković";
            EmailText.Text = "milan.iliskovic@example.com";
            UsernameText.Text = "milan123";
            PhoneText.Text = "+387 65 123 456";
        }

        private void OnChangePictureClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Title = "Select a Profile Picture"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProfileImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void OnChangeThemeClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Theme change functionality not implemented yet.", "Change Theme", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
