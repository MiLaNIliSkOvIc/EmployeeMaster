using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EmployeeMaster.Services
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;

    namespace EmployeeMaster.Services
    {
        public class PositionService
        {
            private readonly string connectionString;

            public PositionService()
            {
                connectionString = ConfigurationManager.ConnectionStrings["EmployeeDatabase"].ConnectionString;
            }

            public List<Position> GetPositions()
            {
                var positions = new List<Position>();

                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = "SELECT idPosition, name FROM Position";

                    var command = new MySqlCommand(query, connection);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            positions.Add(new Position
                            {
                                Id = reader.GetInt32("idPosition"),
                                Name = reader.GetString("name")
                            });
                        }
                    }
                }

                return positions;
            }

         
            public List<Position> SearchPositions(string searchTerm)
            {
                var positions = new List<Position>();

                using (var connection = new MySqlConnection(connectionString))
                {
                    var query = "SELECT idPosition, name FROM Position WHERE name LIKE @SearchTerm";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            positions.Add(new Position
                            {
                                Id = reader.GetInt32("idPosition"),
                                Name = reader.GetString("name")
                            });
                        }
                    }
                }

                return positions;
            }
            public string GetLatestPosition(int userId)
            {
                Position latestPosition = null;

                using (var connection = new MySqlConnection(connectionString))
                {
                    
                    var query = @"
                    SELECT p.idPosition, p.name
                    FROM Position p
                    JOIN Employee_has_Position eph ON eph.Position_idPosition = p.idPosition
                    WHERE eph.Employee_User_idUser = @UserId
                    AND eph.date = (
                        SELECT MAX(date)
                        FROM Employee_has_Position
                        WHERE Employee_User_idUser = @UserId
                    )";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            latestPosition = new Position
                            {
                                Id = reader.GetInt32("idPosition"),
                                Name = reader.GetString("name")
                            };
                        }
                    }
                }

                return latestPosition.Name;
            }
        }

  
        public class Position
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}
