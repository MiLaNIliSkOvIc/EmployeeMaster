using EmployeeMaster.Model;
using EmployeeMaster.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using employee = EmployeeMaster.Model.Employee;
using System.Configuration;


public class EmployeeService
{
    private readonly string connectionString;

    public EmployeeService()
    {
        connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
    }

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

    public void AddEmployee(string firstName, string lastName, string username, string password, string email, string phone, string picture, int? positionId, DateTime hireDate, int salary,int roleId,DateTime dateOfBirth)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {

                    var getMaxUserIdQuery = "SELECT MAX(idUser) FROM User";
                    var getMaxUserIdCommand = new MySqlCommand(getMaxUserIdQuery, connection, transaction);
                    var maxUserId = getMaxUserIdCommand.ExecuteScalar();
                    int userId = (maxUserId == DBNull.Value) ? 1 : Convert.ToInt32(maxUserId) + 1;


                    var userQuery = @"
                INSERT INTO User (idUser, ime, lastname, username, password, email, phone, picture,dateOfBirth)
                VALUES (@UserId, @FirstName, @LastName, @Username, @Password, @Email, @Phone, @Picture, @Date);";

                    var userCommand = new MySqlCommand(userQuery, connection, transaction);
                    userCommand.Parameters.AddWithValue("@UserId", userId);
                    userCommand.Parameters.AddWithValue("@FirstName", firstName);
                    userCommand.Parameters.AddWithValue("@LastName", lastName);
                    userCommand.Parameters.AddWithValue("@Username", username);
                    userCommand.Parameters.AddWithValue("@Password", password);
                    userCommand.Parameters.AddWithValue("@Email", email);
                    userCommand.Parameters.AddWithValue("@Phone", phone);
                    userCommand.Parameters.AddWithValue("@Picture", picture);
                    userCommand.Parameters.AddWithValue("@Date", dateOfBirth.Date);

                    userCommand.ExecuteNonQuery();



                    var employeeQuery = @"
                INSERT INTO Employee (User_idUser, employmentDate, salary)
                VALUES (@UserId, @HireDate, @Salary);";

                    var employeeCommand = new MySqlCommand(employeeQuery, connection, transaction);
                    employeeCommand.Parameters.AddWithValue("@UserId", userId);
                    employeeCommand.Parameters.AddWithValue("@HireDate", hireDate.Date);
                    employeeCommand.Parameters.AddWithValue("@Salary", salary);

                    employeeCommand.ExecuteNonQuery();


                    if (positionId.HasValue)
                    {
                        var positionQuery = @"
                    INSERT INTO Employee_has_Position (Employee_User_idUser, Position_idPosition, date)
                    VALUES (@UserId, @PositionId, @date);";

                        var positionCommand = new MySqlCommand(positionQuery, connection, transaction);
                        positionCommand.Parameters.AddWithValue("@UserId", userId);
                        positionCommand.Parameters.AddWithValue("@date", hireDate.Date);
                        positionCommand.Parameters.AddWithValue("@PositionId", positionId.Value);

                        positionCommand.ExecuteNonQuery();
                    }



                    var roleQuery = @"
                        INSERT INTO Employee_has_Role (Employee_User_idUser, Role_idRole, date)
                        VALUES (@UserId, @RoleId, @Date);";

                    var roleCommand = new MySqlCommand(roleQuery, connection, transaction);
                    roleCommand.Parameters.AddWithValue("@UserId", userId);
                    roleCommand.Parameters.AddWithValue("@RoleId", roleId);
                    roleCommand.Parameters.AddWithValue("@Date", hireDate.Date);

                    roleCommand.ExecuteNonQuery();

                    if (roleId == 1)
                    {
                       
                        var secondRoleCommand = new MySqlCommand(roleQuery, connection, transaction);
                        secondRoleCommand.Parameters.AddWithValue("@UserId", userId);
                        secondRoleCommand.Parameters.AddWithValue("@RoleId", 2); 
                        secondRoleCommand.Parameters.AddWithValue("@Date", hireDate.Date);

                        secondRoleCommand.ExecuteNonQuery();
                    }
  
                    transaction.Commit();


                    new SettingService().AddSetting(new SettingModel(userId));
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public void DeleteEmployee(int userId)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                  
                    var positionQuery = @"
                    DELETE FROM Employee_has_Position
                    WHERE Employee_User_idUser = @UserId";
                    var positionCommand = new MySqlCommand(positionQuery, connection, transaction);
                    positionCommand.Parameters.AddWithValue("@UserId", userId);
                    positionCommand.ExecuteNonQuery();

               
                    var roleQuery = @"
                    DELETE FROM Employee_has_Role
                    WHERE Employee_User_idUser = @UserId";
                    var roleCommand = new MySqlCommand(roleQuery, connection, transaction);
                    roleCommand.Parameters.AddWithValue("@UserId", userId);
                    roleCommand.ExecuteNonQuery();

                   
                    var workHourQuery = @"
                    DELETE FROM WorkHour
                    WHERE Employee_User_idUser = @UserId";
                    var workHourCommand = new MySqlCommand(workHourQuery, connection, transaction);
                    workHourCommand.Parameters.AddWithValue("@UserId", userId);
                    workHourCommand.ExecuteNonQuery();

                 
                    var taskQuery = @"
                    DELETE FROM Task
                    WHERE Employee_User_idUser = @UserId";
                    var taskCommand = new MySqlCommand(taskQuery, connection, transaction);
                    taskCommand.Parameters.AddWithValue("@UserId", userId);
                    taskCommand.ExecuteNonQuery();

                  
                    var vacationQuery = @"
                    DELETE FROM Vacation
                    WHERE Employee_User_idUser = @UserId";
                    var vacationCommand = new MySqlCommand(vacationQuery, connection, transaction);
                    vacationCommand.Parameters.AddWithValue("@UserId", userId);
                    vacationCommand.ExecuteNonQuery();

                   
                    var notificationQuery = @"
                    DELETE FROM Notification
                    WHERE Employee_User_idUser = @UserId";
                    var notificationCommand = new MySqlCommand(notificationQuery, connection, transaction);
                    notificationCommand.Parameters.AddWithValue("@UserId", userId);
                    notificationCommand.ExecuteNonQuery();

               
                    var settingQuery = @"
                    DELETE FROM Setting
                    WHERE User_idUser = @UserId";
                    var settingCommand = new MySqlCommand(settingQuery, connection, transaction);
                    settingCommand.Parameters.AddWithValue("@UserId", userId);
                    settingCommand.ExecuteNonQuery();

                    
                    var employeeQuery = @"
                    DELETE FROM Employee
                    WHERE User_idUser = @UserId";
                    var employeeCommand = new MySqlCommand(employeeQuery, connection, transaction);
                    employeeCommand.Parameters.AddWithValue("@UserId", userId);
                    employeeCommand.ExecuteNonQuery();


                    var userQuery = @"
                    DELETE FROM User
                    WHERE idUser = @UserId";
                    var userCommand = new MySqlCommand(userQuery, connection, transaction);
                    userCommand.Parameters.AddWithValue("@UserId", userId);
                    userCommand.ExecuteNonQuery();

                  
                    transaction.Commit();
                }
                catch (Exception)
                {
                   
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }


}

