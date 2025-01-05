using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using EmployeeMaster.Model;
using System.Configuration;

namespace EmployeeMaster.Services
{
  
       public class VacationService
        {
        private readonly string connectionString;

        public VacationService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
        }

        public List<Vacation> GetVacationRequests()
            {
                var vacationRequests = new List<Vacation>();

                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    SELECT v.idVacation, u.ime AS FirstName, u.lastname AS LastName, v.`From` AS StartDate, v.`To` AS EndDate, v.Status
                    FROM Vacation v
                    JOIN Employee e ON v.Employee_User_idUser = e.User_idUser
                    JOIN User u ON e.User_idUser = u.idUser";

                    var command = new MySqlCommand(query, connection);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vacationRequests.Add(new Vacation
                            {
                                VacationRequestId = reader.GetInt32("idVacation"),  
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                StartDate = reader.GetString("StartDate"),  
                                EndDate = reader.GetString("EndDate"),      
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }

                return vacationRequests;
            }

         
            public void AcceptRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    UPDATE Vacation 
                    SET Status = 'Accept' 
                    WHERE idVacation = @VacationRequestId";  

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            public void DenyRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    UPDATE Vacation 
                    SET Status = 'Denied'  
                    WHERE idVacation = @VacationRequestId";  

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            
            public void AddVacationRequest(Vacation request)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    INSERT INTO Vacation (Employee_User_idUser, `From`, `To`, Status) 
                    VALUES (@EmployeeId, @StartDate, @EndDate, @Status)";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                    command.Parameters.AddWithValue("@StartDate", request.StartDate);
                    command.Parameters.AddWithValue("@EndDate", request.EndDate);
                    command.Parameters.AddWithValue("@Status", request.Status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

           
            public void DeleteVacationRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = "DELETE FROM Vacation WHERE idVacation = @VacationRequestId";  

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        public List<Vacation> GetVacationsByEmployeeId(int employeeId)
        {
            var vacations = new List<Vacation>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
            SELECT 
                v.idVacation, 
                u.ime AS FirstName, 
                u.lastname AS LastName, 
                v.`From` AS StartDate, 
                v.`To` AS EndDate, 
                v.Status
            FROM Vacation v
            JOIN Employee e ON v.Employee_User_idUser = e.User_idUser
            JOIN User u ON e.User_idUser = u.idUser
            WHERE e.User_idUser = @EmployeeId";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vacations.Add(new Vacation
                        {
                            VacationRequestId = reader.GetInt32("idVacation"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            StartDate = reader.GetString("StartDate"),
                            EndDate = reader.GetString("EndDate"),
                            Status = reader.GetString("Status")
                        });
                    }
                }
            }

            return vacations;
        }
    }
    }
