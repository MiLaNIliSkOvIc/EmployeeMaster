
//using EmployeeMaster.Services; // Dodajte referencu na servis
//using System.Collections.ObjectModel;
//using System.Windows;
//using System.Windows.Controls;
//using EmployeeMaster.Employee.WorkedHoursScreen;

//namespace EmployeeMaster.Employee.DashBoardView
//{
//    public partial class Dashboard : Window
//    {
//        public ObservableCollection<Model.Task> Tasks { get; set; }
//        private readonly TaskService _taskService;

//        public Dashboard()
//        {
//            InitializeComponent();
//            string connectionString = "Server=127.0.0.1;Database=projectmanagement;User Id=root;Password=02100078;";

//            //_taskService = new TaskService(connectionString);
//            LoadTasks();
//            DataGrid.ItemsSource = Tasks;
//            Card5TextBlock.Text = "03:12:51";
//            UserNameTextBlock.Text = "Milan Iliskovic";
//        }

//        private void LoadTasks()
//        {
//            try
//            {
//                //var taskList = _taskService.GetAllTasks();
//             //   Tasks = new ObservableCollection<Model.Task>(taskList);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Greška pri dohvaćanju podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }
//        private void OnCardClick(object sender, RoutedEventArgs e)
//        {




//        }

//        private void OnCardClickWorkedHours(object sender, RoutedEventArgs e)
//        {
//            //WorkedHours workedHours = new WorkedHours();
//            //workedHours.Show();

//        }

//        private void OnCardClickPersonalInfo(object sender, RoutedEventArgs e)
//        {

//        }

//    }
//}