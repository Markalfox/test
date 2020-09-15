using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlEventAreaRepository : IRepository<EventAreaEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlEventAreaRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(EventAreaEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [EventArea] VALUES (@eventId, @description, @coordX, @coordY, @price)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@eventId", SqlDbType.Int)).Value = item.EventId;
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200)).Value = item.Description;
                        command.Parameters.Add(new SqlParameter("@coordX", SqlDbType.Int)).Value = item.CoordX;
                        command.Parameters.Add(new SqlParameter("@coordY", SqlDbType.Int)).Value = item.CoordY;
                        command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal)).Value = item.Price;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [EventArea] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<EventAreaEntity> GetAll()
        {
            string query = "SELECT * FROM [EventArea]";
            List<EventAreaEntity> eventAreas = new List<EventAreaEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventAreas.Add(new EventAreaEntity
                    {
                        Id = reader.GetInt32(0),
                        EventId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        CoordX = reader.GetInt32(3),
                        CoordY = reader.GetInt32(4),
                        Price = reader.GetDecimal(5),
                    });
                }

                command.Dispose();

                return eventAreas;
            }
        }

        public EventAreaEntity GetById(int id)
        {
            EventAreaEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [EventArea] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new EventAreaEntity
                        {
                            Id = reader.GetInt32(0),
                            EventId = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            CoordX = reader.GetInt32(3),
                            CoordY = reader.GetInt32(4),
                            Price = reader.GetDecimal(5),
                        };
                    }
                }
            }

            return result;
        }

        public void Update(EventAreaEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE [EventArea] SET [EventId] = @eventId, [Description] = @description, [CoordX] = @coordX, [CoordY] = @coordY, [Price] = @price WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@eventId", SqlDbType.Int)).Value = item.EventId;
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200)).Value = item.Description;
                        command.Parameters.Add(new SqlParameter("@coordX", SqlDbType.Int)).Value = item.CoordX;
                        command.Parameters.Add(new SqlParameter("@coordY", SqlDbType.Int)).Value = item.CoordY;
                        command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal)).Value = item.Price;
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = item.Id;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
