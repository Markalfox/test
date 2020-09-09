using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlEventRepository : IRepository<EventEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlEventRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(EventEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "INSERT INTO [Event] ([Id], [Name], [Description], [LayoutId]) " +
                               "VALUES (@item.Id, @item.Name, @item.Description, @item.LayoutId)";

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
            string query = "DELETE FROM [Event] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public IEnumerable<EventEntity> GetAll()
        {
            string query = "SELECT * FROM [Event]";
            List<EventEntity> events = new List<EventEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    events.Add(new EventEntity
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        LayoutId = reader.GetInt32(3),
                    });
                }

                command.Dispose();

                return events;
            }
        }

        public EventEntity GetById(int id)
        {
            string query = "SELECT * FROM [Event] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                EventEntity result = null;

                while (reader.Read())
                {
                    result = new EventEntity
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        LayoutId = reader.GetInt32(3),
                    };
                }

                command.Dispose();

                return result;
            }
        }

        public void Update(EventEntity item)
        {
            string query = "UPDATE [Event] " +
                           "SET [Name] = @item.Name, [Description] = @item.Description, [LayoutId] = @item.LayoutId " +
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
