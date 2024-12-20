using MySql.Data.MySqlClient;

namespace EmployeeMaster.Services
{
    public class TaskService
    {
        private readonly string _connectionString;

        public TaskService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<EmployeeMaster.Model.Task> GetAllTasks()
        {
            var tasks = new List<EmployeeMaster.Model.Task>();


            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT  Title,Deadline, Description, Priority, Status FROM task";


                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            DateTime deadlineDateTime = reader.GetDateTime(1);


                            string formattedDeadline = deadlineDateTime.ToString("yyyy-MM-dd");

                            tasks.Add(new EmployeeMaster.Model.Task
                            {
                                Title = reader.GetString(0),
                                Deadline = formattedDeadline,
                                Description = reader.GetString(2),
                                Priority = reader.GetString(3),
                                Status = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return tasks;
        }
    }
}
