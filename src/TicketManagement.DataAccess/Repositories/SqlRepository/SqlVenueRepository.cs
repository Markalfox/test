using System.Collections.Generic;
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
            string query = "INSERT INTO [Venue] ([Id], [Description], [Address], [Phone]) " +
                           "VALUES (@item.Id, @item.Description, @item.Address, @item.Phone)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM [Venue] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
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
            string query = "SELECT * FROM [Venue] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                VenueEntity result = null;

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

                command.Dispose();

                return result;
            }
        }

        public void Update(VenueEntity item)
        {
            string query = "UPDATE [Venue] " +
                           "SET [Description] = @item.Description, [Address] = @item.Address, [Phone] = @item.Phone " +
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
