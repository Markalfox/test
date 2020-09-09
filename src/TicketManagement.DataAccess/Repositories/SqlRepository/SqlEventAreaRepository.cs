using System;
using System.Collections.Generic;
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
                string query = "INSERT INTO [EventArea] ([Id], [EventId], [Description], [CoordX], [CoordY], [Price]) " +
                               "VALUES (@item.Id, @item.EventId, @item.Description, @item.CoordX, @item.CoordY, @item.Price)";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM [EventArea] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
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
            string query = "SELECT * FROM [EventArea] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                EventAreaEntity result = null;

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

                command.Dispose();

                return result;
            }
        }

        public void Update(EventAreaEntity item)
        {
            string query = "UPDATE [EventArea] " +
                           "SET [EventId] = @item.EventId, [Description] = @item.Description, [CoordX] = @item.CoordX, [CoordY] = @item.CoordY, [Price] = @item.Price " +
                           "WHERE [Id] = @item.Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
    }
}
