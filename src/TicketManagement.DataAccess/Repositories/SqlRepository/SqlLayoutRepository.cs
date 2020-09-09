using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlLayoutRepository : IRepository<LayoutEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlLayoutRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(LayoutEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "INSERT INTO [Layout] ([Id], [VenueId], [Description]) " +
                               "VALUES (@item.Id, @item.VenueId, @item.Description)";

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
            string query = "DELETE FROM [Layout] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public IEnumerable<LayoutEntity> GetAll()
        {
            string query = "SELECT * FROM [Layout]";
            List<LayoutEntity> layouts = new List<LayoutEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    layouts.Add(new LayoutEntity
                    {
                        Id = reader.GetInt32(0),
                        VenueId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                    });
                }

                command.Dispose();

                return layouts;
            }
        }

        public LayoutEntity GetById(int id)
        {
            string query = "SELECT * FROM [Layout] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                LayoutEntity result = null;

                while (reader.Read())
                {
                    result = new LayoutEntity
                    {
                        Id = reader.GetInt32(0),
                        VenueId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                    };
                }

                command.Dispose();

                return result;
            }
        }

        public void Update(LayoutEntity item)
        {
            string query = "UPDATE [Layout] " +
                           "SET [VenueId] = @item.VenueId, [Description] = @item.Description " +
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
