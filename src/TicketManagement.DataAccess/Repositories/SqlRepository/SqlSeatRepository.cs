using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlSeatRepository : IRepository<SeatEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlSeatRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(SeatEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "INSERT INTO [Seat] ([Id], [AreaId], [Row], [Number]) " +
                               "VALUES (@item.Id, @item.AreaId, @item.Row, @item.Number)";

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
            string query = "DELETE FROM [Seat] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public IEnumerable<SeatEntity> GetAll()
        {
            string query = "SELECT * FROM [Seat]";
            List<SeatEntity> seats = new List<SeatEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    seats.Add(new SeatEntity
                    {
                        Id = reader.GetInt32(0),
                        AreaId = reader.GetInt32(1),
                        Row = reader.GetInt32(2),
                        Number = reader.GetInt32(3),
                    });
                }

                command.Dispose();

                return seats;
            }
        }

        public SeatEntity GetById(int id)
        {
            string query = "SELECT * FROM [Seat] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                SeatEntity result = null;

                while (reader.Read())
                {
                    result = new SeatEntity
                    {
                        Id = reader.GetInt32(0),
                        AreaId = reader.GetInt32(1),
                        Row = reader.GetInt32(2),
                        Number = reader.GetInt32(3),
                    };
                }

                command.Dispose();

                return result;
            }
        }

        public void Update(SeatEntity item)
        {
            string query = "UPDATE [Seat] " +
                           "SET [AreaId] = @item.AreaId, [Row] = @item.Row, [Number] = @item.Row " +
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
