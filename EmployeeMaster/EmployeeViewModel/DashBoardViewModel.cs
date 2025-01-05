

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using EmployeeMaster.Model;
using EmployeeMaster.Services;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Threading;
using EmployeeMaster.Services.EmployeeMaster.Services;

namespace EmployeeMaster.EmployeeViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly NotificationService _notificationService;
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly VacationService _vacationService;
        private readonly WorkHourService _workHourService;
        private readonly PositionService _positionService;

        private DispatcherTimer _timer;
        private TimeSpan _currentWorkTime;
        private string _currentTime;
        private string _workButtonContent;
        private string _vacationDates;
        private string _currentDate;

        public ObservableCollection<string> Notifications { get; set; }
        public ObservableCollection<string> MonthLabels { get; set; }
        public ObservableCollection<PieSeries<double>> TaskCompletionPieSeries { get; set; }

        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
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
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public string WorkButtonContent
        {
            get => _workButtonContent;
            set
            {
                _workButtonContent = value;
                OnPropertyChanged(nameof(WorkButtonContent));
            }
        }

        public DashboardViewModel()
        {
            _notificationService = new NotificationService();
            _taskService = new TaskService();
            _userService = new UserService();
            _vacationService = new VacationService();
            _workHourService = new WorkHourService();
            _positionService = new PositionService();

            Notifications = new ObservableCollection<string>();
            MonthLabels = new ObservableCollection<string>();

            InitializeDashboard();
            SetupTimer();
        }

        private void SetupTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) =>
            {
                _currentWorkTime = _currentWorkTime.Add(TimeSpan.FromSeconds(1));
                CurrentTime = _currentWorkTime.ToString("hh\\:mm\\:ss");
            };
        }

        private void InitializeDashboard()
        {
            try
            {
                WorkHour hours = _workHourService.GetWorkHourByEmployeeIdAndDate(CurrentUser.Instance.IdUser, DateTime.Now.Date);
                if (hours == null)
                {
                    CurrentTime = "00:00:00";
                    _currentWorkTime = TimeSpan.Zero;
                    WorkButtonContent = "Start Work";
                }
                else
                {
                    if (hours.FinishDate == null)
                    {
                        _currentWorkTime = DateTime.Now.TimeOfDay - hours.StartDate;
                        CurrentTime = _currentWorkTime.ToString("hh\\:mm\\:ss");
                        WorkButtonContent = "Finish Work";
                        _timer.Start();
                    }
                    else
                    {
                        CurrentTime = "00:00:00";
                        _currentWorkTime = TimeSpan.Zero;
                        WorkButtonContent = "Start Work";
                    }
                }

                var user = _userService.GetUserInfo(CurrentUser.Instance.IdUser);
                var notifications = _notificationService.GetNotifications();
                Notifications.Clear();
                foreach (var notification in notifications)
                {
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
                TaskCompletionPieSeries = new ObservableCollection<PieSeries<double>>
            {
                new PieSeries<double> { Values = new ObservableCollection<double> { tasks.Count(t => t.Status == "Completed") }, Name = "Completed" },
                new PieSeries<double> { Values = new ObservableCollection<double> { tasks.Count(t => t.Status != "Completed") }, Name = "Remaining" }
            };

                CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing dashboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void StartWork()
        {
            int employeeId = CurrentUser.Instance.IdUser;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (_workHourService.IsWorkHourExist(employeeId, date))
            {
                MessageBox.Show("You already have a work hour entry for today.");
                return;
            }

            TimeSpan startTime = DateTime.Now.TimeOfDay;
            string shift = "Morning"; // popraviti

            var workHour = new WorkHour
            {
                StartDate = startTime,
                Date = date,
                Shift = shift,
                EmployeeId = employeeId
            };

            _workHourService.AddWorkHourWithoutFinish(workHour);
            _currentWorkTime = TimeSpan.Zero;
            CurrentTime = _currentWorkTime.ToString("hh\\:mm\\:ss");
            WorkButtonContent = "Finish Work";
            _timer.Start();
        }

        public void FinishWork()
        {
            int employeeId = CurrentUser.Instance.IdUser;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (!_workHourService.IsWorkHourExist(employeeId, date))
            {
                MessageBox.Show("You have not started work yet.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to finish your work?", "Finish Work", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var finishTime = DateTime.Now.TimeOfDay;
                _workHourService.UpdateWorkHourFinishByDate(employeeId, DateTime.Now.Date, finishTime);

                WorkButtonContent = "Start Work";
                _timer.Stop();
                _currentWorkTime = TimeSpan.Zero;
                CurrentTime = _currentWorkTime.ToString("hh\\:mm\\:ss");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


