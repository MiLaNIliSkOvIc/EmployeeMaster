﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using EmployeeMaster.Model;
using EmployeeMaster.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics.Eventing.Reader;
using EmployeeMaster.Services.EmployeeMaster.Services;
using EmployeeMaster.NotificationDisplay;
using EmployeeMaster.YesNoWindow;


namespace EmployeeMaster.Employee.DashBoardView
{
    public partial class Dashboard : UserControl, INotifyPropertyChanged
    {
        private readonly NotificationService _notificationService;
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly VacationService _vacationService;
        private readonly WorkHourService _workHourService;
        private readonly PositionService _positionService;
        private ObservableCollection<PieSeries<double>> _taskCompletionPieSeries;
        private ObservableCollection<LineSeries<double>> _taskCompletionSeries;
        private string _vacationDates;
        private string _currentDate;
        private string _currentTime;
        private string _job;
        private string _welcomeMessage;
        private int currentWorkHourId; 
        private WorkHourService workHourService = new WorkHourService();
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public ObservableCollection<string> Notifications { get; set; }
        private string _workButtonContent;
        public string WorkButtonContent
        {
            get => _workButtonContent;
            set
            {
                _workButtonContent = value;
                OnPropertyChanged(nameof(WorkButtonContent));
            }
        }
        public string job
        {
            get => _job;
            set
            {
                if (_job != value)
                {
                    _job = value;
                    OnPropertyChanged(nameof(job));
                }
            }
        }
        public string welcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                if (_welcomeMessage != value)
                {
                    _welcomeMessage = value;
                    OnPropertyChanged(nameof(welcomeMessage));
                }
            }
        }
        public string VacationDates
        {
            get => _vacationDates;
            set
            {
                _vacationDates = value;
                OnPropertyChanged(nameof(VacationDates));
            }
        }
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        public ObservableCollection<PieSeries<double>> TaskCompletionPieSeries
        {
            get => _taskCompletionPieSeries;
            set
            {
                _taskCompletionPieSeries = value;
                OnPropertyChanged(nameof(TaskCompletionPieSeries));
            }
        }
        public ObservableCollection<LineSeries<double>> TaskCompletionSeries
        {
            get => _taskCompletionSeries;
            set
            {
                _taskCompletionSeries = value;
                OnPropertyChanged(nameof(TaskCompletionSeries));
            }
        }
        public Dashboard()
        {
            InitializeComponent();

            _notificationService =new NotificationService();
            _taskService = new TaskService();
            _userService = new UserService();
            _vacationService = new  VacationService();
            _workHourService = new WorkHourService();
            _positionService = new PositionService();
            Notifications = new ObservableCollection<string>();

            InitializeDashboard();

            DataContext = this;
           
            WorkHour hours = _workHourService.GetWorkHourByEmployeeIdAndDate(CurrentUser.Instance.IdUser, DateTime.Now.Date);
            
            if (hours==null || hours.FinishDate!=null)
                WorkButtonContent = "Start Work";
            else
                WorkButtonContent = "Finish Work";


            if (hours == null )
            {
                
                CurrentTime = "00:00:00";
            }
            else
            {
                CurrentTime = (DateTime.Now.TimeOfDay - hours.StartDate).ToString(@"hh\:mm\:ss");
                if (WorkButtonContent.Equals("Finish Work"))
                {
                       
                    timer.Start();
                       
                }
                else { CurrentTime = "00:00:00"; }
            }

            TimeSpan timeSpan = TimeSpan.Parse(CurrentTime);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(1));
                CurrentTime = timeSpan.ToString(@"hh\:mm\:ss");
            };
        }

        private async void InitializeDashboard()
        {
            try
            {
               
                string imagePath = _userService.GetUserInfo(CurrentUser.Instance.IdUser).Picture;
               // string imagePath = _userViewModel.ProfileImage ?? "C:/Users/pc/Desktop/picturesOfCars/profile.jpg";
                if (File.Exists(imagePath))
                {
                    
                    ImageBrush profileImageBrush = new ImageBrush();
                    profileImageBrush.ImageSource = new BitmapImage(new Uri(imagePath));

                  
                    ProfileImageEllipse.Fill = profileImageBrush;
                }
                else
                {
                   
                     ProfileImageEllipse.Fill = new SolidColorBrush(Colors.Gray); 
                }
                var user =  _userService.GetUserInfo(CurrentUser.Instance.IdUser);
                job = _positionService.GetLatestPosition(CurrentUser.Instance.IdUser);
                welcomeMessage = $"{user.FullName}";
                
                
                var notifications = _notificationService.GetNotifications();
                Notifications.Clear();
                NotificationsListBox.ItemsSource = Notifications;
                var br = 0;
                foreach (var notification in notifications)
                {
                    if (br == 2) break;
                    br++;
                    Notifications.Add(notification.Content);
                }

                var vacation = _vacationService.GetVacationsByEmployeeId(user.IdUser);
                if (vacation != null && vacation.Count > 0)
                {
                    
                    DateTime startDate = DateTime.ParseExact(vacation[0].StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(vacation[0].EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                  
                    VacationDates = $"{startDate:dd MMM} - {endDate:dd MMM}";
                }
                else
                {
                    VacationDates = "No upcoming vacations";
                }

                var tasks = _taskService.GetTasksByEmployeeId(user.IdUser);
                TaskCardsStackPanel.Children.Clear();
                foreach (var task in tasks)
                {
                    if(task.Status!="Completed")
                         AddTaskCard(task.TaskName);
                }

                var completedTasks = tasks.Count(t => t.Status=="Completed");
                var remainingTasks = tasks.Count - completedTasks;

                TaskCompletionPieSeries = new ObservableCollection<PieSeries<double>>
                {
                    new PieSeries<double> { Values = new ObservableCollection<double> { completedTasks }, Name = "Completed" },
                    new PieSeries<double> { Values = new ObservableCollection<double> { remainingTasks }, Name = "Remaining" }
                };

               

                CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing dashboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTaskCard(string taskName)
        {
            var taskCard = new Border
            {
                CornerRadius = new System.Windows.CornerRadius(10),
                Background = System.Windows.Media.Brushes.LightGray,
                Padding = new System.Windows.Thickness(15),
                Margin = new System.Windows.Thickness(0, 10, 10, 0),
                BorderBrush = System.Windows.Media.Brushes.Gray,
                BorderThickness = new System.Windows.Thickness(1)
            };

            var taskText = new TextBlock
            {
                Text = taskName,
                FontSize = 12,
                FontWeight = System.Windows.FontWeights.Bold
            };
            
            taskCard.Child = taskText;

            TaskCardsStackPanel.Children.Add(taskCard);
        }
        private void WorkButton_Click(object sender, RoutedEventArgs e)
        {
          
          

            var button = sender as Button;

            if (button?.Content?.ToString() == "Start Work")
            {
                   StartWorkButton_Click();
               
               
            }
            else if (button?.Content?.ToString() == "Finish Work")
            {
                 FinishWorkButton_Click();
                WorkButtonContent = "Start Work";
                timer.Stop();
            }
        }
        private void StartWorkButton_Click()
        {
            int employeeId = CurrentUser.Instance.IdUser;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

           
            if (workHourService.IsWorkHourExist(employeeId, date))
            {
                NotificationWindow notif = new NotificationWindow("You already have a work hour entry for today.");
                notif.Show();
          
                return; 
            }
            timer.Start();
            WorkButtonContent = "Finish Work";
            TimeSpan startTime = DateTime.Now.TimeOfDay;
            string shift = DetermineShift(startTime);
           

            var workHour = new WorkHour
            {
                StartDate = startTime,
                Date = date,
                Shift = shift,
                EmployeeId = employeeId
            };

            workHourService.AddWorkHourWithoutFinish(workHour);
            MessageBox.Show("Work started!");
        }

        private void FinishWorkButton_Click()
        {
            int employeeId = CurrentUser.Instance.IdUser;
            DateTime currentDate = DateTime.Now.Date;
            TimeSpan finishTime = DateTime.Now.TimeOfDay;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (!workHourService.IsWorkHourExist(employeeId, date))
            {
                MessageBox.Show("Niste poceli raditi");
                return;
            }



            string message = "Are you sure you want to finish your work?";
          

            var dialog = new YesNoDialog(message);
            var result = dialog.ShowDialog();

            if (result == true) 
            {
                
                workHourService.UpdateWorkHourFinishByDate(CurrentUser.Instance.IdUser, currentDate, finishTime);

                NotificationWindow notif = new NotificationWindow("Work finished");
                notif.Show();
            }
            else 
            {
                
            }
        }

        private string DetermineShift(TimeSpan currentTime)
        {
            if (currentTime >= TimeSpan.FromHours(6) && currentTime < TimeSpan.FromHours(14))
            {
                return "Morning";
            }
            else if (currentTime >= TimeSpan.FromHours(14) && currentTime < TimeSpan.FromHours(22))
            {
                return "Afternoon";
            }
            else
            {
                return "Night";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
