using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaster.Services
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;

    namespace EmployeeMaster.Services
    {
        public class PositionService
        {
            private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=mydb;User Id=root;Password=02100078;";


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
        }

  
        public class Position
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}
