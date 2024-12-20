using System.Windows;
using EmployeeMaster.Employee.DashBoardView;
namespace EmployeeMaster.Employee.MainScreen
{
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        
        private void OpenSidebarButton_Click(object sender, RoutedEventArgs e)
        {
          
            Sidebar.Visibility = Visibility.Visible;
        }

     
        private void CloseSidebarButton_Click(object sender, RoutedEventArgs e)
        {
        
            Sidebar.Visibility = Visibility.Collapsed;
        }

     
        private void OpenDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            
            Dashboard dashboard = new Dashboard();

            
            dashboard.Show();

            
        }
    }
}
