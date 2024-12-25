using EmployeeMaster.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace EmployeeMaster.Services
{
    public class WorkHourService
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";

        public List<WorkHour> GetAllWorkHours()
        {
            var workHours = new List<WorkHour>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
            SELECT 
                w.idWorkHour, 
                w.`Start`, 
                w.`Finish`, 
                w.datum, 
                w.Shift, 
                w.Employee_User_idUser, 
                u.ime, 
                u.lastname
            FROM WorkHour w
            JOIN User u ON w.Employee_User_idUser = u.idUser";

                var command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workHours.Add(new WorkHour
                        {
                            Id = reader.GetInt32("idWorkHour"),
                            StartDate = reader.GetTimeSpan("Start"),
                            FinishDate = reader.GetTimeSpan("Finish"),
                            Date = DateOnly.FromDateTime(reader.GetDateTime("datum")),
                            Shift = reader.GetString("Shift"),
                            EmployeeId = reader.GetInt32("Employee_User_idUser"),
                            fullName = reader.GetString("ime")+' '+ reader.GetString("lastname"), 
                            
                        });
                    }
                }
            }

            return workHours;
        }

        public void AddWorkHour(WorkHour workHour)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    INSERT INTO WorkHour (`Start`, `Finish`, datum, Shift, Employee_User_idUser)
                    VALUES (@StartDate, @FinishDate, @Date, @Shift, @EmployeeId)";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", workHour.StartDate);
                command.Parameters.AddWithValue("@FinishDate", workHour.FinishDate);
                command.Parameters.AddWithValue("@Date", workHour.Date);
                command.Parameters.AddWithValue("@Shift", workHour.Shift);
                command.Parameters.AddWithValue("@EmployeeId", workHour.EmployeeId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public List<WorkHour> GetWorkHoursByDate(DateTime date)
        {
            var workHours = new List<WorkHour>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT idWorkHour, `Start`, `Finish`, HoursWorked, Shift, Employee_User_idUser 
                    FROM WorkHour
                    WHERE DATE(`Start`) = @Date";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workHours.Add(new WorkHour
                        {
                            Id = reader.GetInt32("idWorkHour"),
                            StartDate = reader.GetTimeSpan("Start"),
                            FinishDate = reader.GetTimeSpan("Finish"),
                            Date = DateOnly.FromDateTime(reader.GetDateTime("datum")),
                            Shift = reader.GetString("Shift"),
                            EmployeeId = reader.GetInt32("Employee_User_idUser")
                        });
                    }
                }
            }

            return workHours;
        }

        // Nova metoda: Filtriraj radne sate po EmployeeId
        public List<WorkHour> GetWorkHoursByEmployeeId(int employeeId)
        {
            var workHours = new List<WorkHour>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT idWorkHour, `Start`, `Finish`, datum, Shift, Employee_User_idUser 
                    FROM WorkHour
                    WHERE Employee_User_idUser = @EmployeeId";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workHours.Add(new WorkHour
                        {
                            Id = reader.GetInt32("idWorkHour"),
                            StartDate = reader.GetTimeSpan("Start"),
                            FinishDate = reader.GetTimeSpan("Finish"),
                            Date = DateOnly.FromDateTime(reader.GetDateTime("datum")),
                            Shift = reader.GetString("Shift"),
                            EmployeeId = reader.GetInt32("Employee_User_idUser")
                        });
                    }
                }
            }

            return workHours;
        }
    }
}
