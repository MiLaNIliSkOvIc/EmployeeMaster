using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeMaster.RoleSelection
{
   
        public partial class RoleSelectionWindow : Window
        {
            public string SelectedRole { get; private set; }

            public RoleSelectionWindow(List<string> roles)
            {
                InitializeComponent();
                RoleListBox.ItemsSource = roles; 
            }

            private void ConfirmButton_Click(object sender, RoutedEventArgs e)
            {
                if (RoleListBox.SelectedItem != null)
                {
                    SelectedRole = RoleListBox.SelectedItem.ToString();
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Please select a role.");
                }
            }
        }
    

}
