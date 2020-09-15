using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlVenueRepository : IRepository<VenueEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlVenueRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(VenueEntity item)
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
                        command.CommandText = "INSERT INTO [Venue] VALUES (@description, @address, @phone)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200)).Value = item.Description;
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 50)).Value = item.Address;
                        command.Parameters.Add(new SqlParameter("@phone", SqlDbType.NVarChar, 20)).Value = item.Phone;

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
                    command.CommandText = "DELETE FROM [Venue] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<VenueEntity> GetAll()
        {
            string query = "SELECT * FROM [Venue]";
            List<VenueEntity> venues = new List<VenueEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    venues.Add(new VenueEntity
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                    });
                }

                command.Dispose();

                return venues;
            }
        }

        public VenueEntity GetById(int id)
        {
            VenueEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Venue] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new VenueEntity
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Address = reader.GetString(2),
                            Phone = reader.GetString(3),
                        };
                    }
                }
            }

            return result;
        }

        public void Update(VenueEntity item)
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
                        command.CommandText = "UPDATE [Venue] SET [Description] = @description, [Address] = @address, [Phone] = @phone WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200)).Value = item.Description;
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 50)).Value = item.Address;
                        command.Parameters.Add(new SqlParameter("@phone", SqlDbType.NVarChar, 20)).Value = item.Phone;
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = item.Id;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
