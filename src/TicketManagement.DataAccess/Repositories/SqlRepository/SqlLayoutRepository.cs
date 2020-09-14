using System;
using System.Collections.Generic;
using System.Data;
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSETR INTO [Layout] VALUES (@venueId, @description)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@venueId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));

                        command.Parameters["@venueId"].Value = item.VenueId;
                        command.Parameters["@description"].Value = item.Description;

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
                    command.CommandText = "DELETE * FROM [Layout] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
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
            LayoutEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Layout] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new LayoutEntity
                        {
                            Id = reader.GetInt32(0),
                            VenueId = reader.GetInt32(1),
                            Description = reader.GetString(2),
                        };
                    }
                }
            }

            return result;
        }

        public void Update(LayoutEntity item)
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
                        command.CommandText = "UPDATE [Layout] SET [VenueId] = @venueId, [Description] = @description WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@venueId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));

                        command.Parameters["@venueId"].Value = item.VenueId;
                        command.Parameters["@description"].Value = item.Description;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
