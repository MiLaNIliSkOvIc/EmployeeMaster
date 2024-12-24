using MySql.Data.MySqlClient;

namespace EmployeeMaster.Services
{
    public class TaskService
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";

        public List<Model.Task> GetTasks()
        {
            var tasks = new List<Model.Task>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT 
                        t.idTask AS TaskId, 
                        t.title AS TaskName, 
                        u.ime AS AssignedTo, 
                        t.deadline AS DueDate, 
                        t.status AS Status, 
                        t.description AS Description
                    FROM Task t
                    JOIN Employee e ON t.Employee_User_idUser = e.User_idUser
                    JOIN User u ON e.User_idUser = u.idUser";

                var command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Model.Task
                        {
                            TaskId = reader.GetInt32("TaskId"),
                            TaskName = reader.GetString("TaskName"),
                            AssignedTo = reader.GetString("AssignedTo"),
                            DueDate = reader.GetDateTime("DueDate"),
                            Status = reader.GetString("Status"),
                            Description = reader.GetString("Description")
                        });
                    }
                }
            }

            return tasks;
        }
       

        public void AddTask(Model.Task task)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    INSERT INTO Task (title, description, deadline, status)
                    VALUES (@Title, @Description, @Deadline, @Status)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", task.TaskName);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@Deadline", task.DueDate);
                command.Parameters.AddWithValue("@Status", task.Status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EditTask(Model.Task task)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    UPDATE Task 
                    SET title = @Title, description = @Description, deadline = @Deadline, status = @Status
                    WHERE idTask = @TaskId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskId", task.TaskId);
                command.Parameters.AddWithValue("@Title", task.TaskName);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@Deadline", task.DueDate);
                command.Parameters.AddWithValue("@Status", task.Status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int taskId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "DELETE FROM Task WHERE idTask = @TaskId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskId", taskId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Model.Task> GetTasksByEmployeeId(int employeeId)
        {
            var tasks = new List<Model.Task>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
            SELECT 
                t.idTask AS TaskId, 
                t.title AS TaskName, 
                u.ime AS AssignedTo, 
                t.deadline AS DueDate, 
                t.status AS Status, 
                t.priority AS priority,
                t.description AS Description
            FROM Task t
            JOIN Employee e ON t.Employee_User_idUser = e.User_idUser
            JOIN User u ON e.User_idUser = u.idUser
            WHERE e.User_idUser = @EmployeeId";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Model.Task
                        {
                            TaskId = reader.GetInt32("TaskId"),
                            TaskName = reader.GetString("TaskName"),
                            AssignedTo = reader.GetString("AssignedTo"),
                            DueDate = reader.GetDateTime("DueDate"),
                           
                            Status = reader.GetString("Status"),
                            priority = reader.GetString("priority"),
                            Description = reader.GetString("Description")
                        });
                    }
                }
            }

            return tasks;
        }
    }
}
