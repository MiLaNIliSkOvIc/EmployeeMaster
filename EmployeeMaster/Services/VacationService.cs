using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using EmployeeMaster.Model;


namespace EmployeeMaster.Services
{
  
       public class VacationService
        {
            private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";

          
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
                                VacationRequestId = reader.GetInt32("idVacation"),  // Correct column name
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                StartDate = reader.GetString("StartDate"),  // Correct column name for 'From'
                                EndDate = reader.GetString("EndDate"),      // Correct column name for 'To'
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }

                return vacationRequests;
            }

            // Accept a Vacation Request
            public void AcceptRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    UPDATE Vacation 
                    SET Status = 'Accept' 
                    WHERE idVacation = @VacationRequestId";  // Correct column name

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Deny a Vacation Request
            public void DenyRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = @"
                    UPDATE Vacation 
                    SET Status = 'Pending'  // Denying would probably reset to 'Pending'
                    WHERE idVacation = @VacationRequestId";  // Correct column name

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Add a new Vacation Request
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

            // Delete a Vacation Request
            public void DeleteVacationRequest(int vacationRequestId)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = "DELETE FROM Vacation WHERE idVacation = @VacationRequestId";  // Correct column name

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VacationRequestId", vacationRequestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
