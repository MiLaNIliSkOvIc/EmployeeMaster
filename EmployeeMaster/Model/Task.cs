using System.ComponentModel;

namespace EmployeeMaster.Model
{
    public class Task : INotifyPropertyChanged
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }

        public string Status { get; set; }
        public string priority { get; set; }
        public string Description { get; set; }
        private string _assignedTo;

        public string AssignedTo
        {
            get => _assignedTo;
            set
            {
                _assignedTo = value;
                OnPropertyChanged(nameof(AssignedTo));
            }
        }

        private int _assignedToId;
        public int AssignedToId
        {
            get => _assignedToId;
            set
            {
                _assignedToId = value;
                OnPropertyChanged(nameof(AssignedToId));
            }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Task() { }
        public Task(string taskName, string status, string priority, string description, DateTime dueDate, int assignedToId)
        {

            TaskName = taskName;
            Status = status;
            this.priority = priority;
            Description = description;
            AssignedToId = assignedToId;
            DueDate = dueDate;
        }

        
    }

}
