using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using employee = EmployeeMaster.Model.Employee;


    public class EmployeeService
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";


         public List<employee> GetEmployees()
        {
            var employees = new List<employee>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT 
                        u.idUser AS Id, 
                        u.ime AS FirstName, 
                        u.lastname AS LastName, 
                        p.name AS Position, 
                        e.employmentDate AS EmploymentDate, 
                        e.salary AS Salary
                    FROM Employee e
                    JOIN User u ON e.User_idUser = u.idUser
                    LEFT JOIN Employee_has_Position ep ON e.User_idUser = ep.Employee_User_idUser
                    LEFT JOIN Position p ON ep.Position_idPosition = p.idPosition";

                var command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new employee
                        {
                            Id = reader.GetInt32("Id"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Position =  reader.GetString("Position"),
                            HireDate = DateOnly.FromDateTime(reader.GetDateTime("EmploymentDate")),
                            Salary =  reader.GetInt32("Salary")
                        });
                    }
                }
            }

            return employees;
        }

        public List<employee> SearchEmployees(string searchTerm)
        {
            var employees = new List<employee>();

            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    SELECT 
                        u.idUser AS Id, 
                        u.ime AS FirstName, 
                        u.lastname AS LastName, 
                        p.name AS Position, 
                        e.employmentDate AS EmploymentDate, 
                        e.salary AS Salary
                    FROM Employee e
                    JOIN User u ON e.User_idUser = u.idUser
                    LEFT JOIN Employee_has_Position ep ON e.User_idUser = ep.Employee_User_idUser
                    LEFT JOIN Position p ON ep.Position_idPosition = p.idPosition
                    WHERE u.ime LIKE @SearchTerm OR u.lastname LIKE @SearchTerm OR p.name LIKE @SearchTerm";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new employee
                        {
                            Id = reader.GetInt32("Id"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Position = reader.GetString("Position"),
                            HireDate = DateOnly.FromDateTime(reader.GetDateTime("EmploymentDate")),

                            Salary = reader.GetInt32("Salary")
                        });
                    }
                }
            }

            return employees;
        }
    public List<string> GetUserRole(int userId)
    {
        var roles = new List<string>();

        using (var connection = new MySqlConnection(connectionString))
        {
            var query = @"
        SELECT r.name AS Role
        FROM Role r
        JOIN Employee_has_Role ehr ON r.idRole = ehr.Role_idRole
        JOIN User u ON ehr.Employee_User_idUser = u.idUser
        WHERE u.idUser = @UserId";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) // Koristimo `while` da dohvatimo sve role
                {
                    roles.Add(reader.GetString("Role"));
                }
            }
        }

        return roles;
    }



}

