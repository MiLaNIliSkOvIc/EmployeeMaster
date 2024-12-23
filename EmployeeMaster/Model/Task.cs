namespace EmployeeMaster.Model
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

}
