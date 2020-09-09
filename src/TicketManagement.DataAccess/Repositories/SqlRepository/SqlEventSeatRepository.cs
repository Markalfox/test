using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlEventSeatRepository : IRepository<EventSeatEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlEventSeatRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public void Create(EventSeatEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "INSERT INTO [EventSeat] ([Id], [EventAreaId], [Row], [Number], [State]) " +
                               "VALUES (@item.Id, @item.EventAreaId, @item.Row, @item.Number, @item.State)";

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
            string query = "DELETE FROM [EventSeat] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public IEnumerable<EventSeatEntity> GetAll()
        {
            string query = "SELECT * FROM [EventSeat]";
            List<EventSeatEntity> eventSeats = new List<EventSeatEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventSeats.Add(new EventSeatEntity
                    {
                        Id = reader.GetInt32(0),
                        EventAreaId = reader.GetInt32(1),
                        Row = reader.GetInt32(2),
                        Number = reader.GetInt32(3),
                        State = reader.GetInt32(4),
                    });
                }

                command.Dispose();

                return eventSeats;
            }
        }

        public EventSeatEntity GetById(int id)
        {
            string query = "SELECT * FROM [EventSeat] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                EventSeatEntity result = null;

                while (reader.Read())
                {
                    result = new EventSeatEntity
                    {
                        Id = reader.GetInt32(0),
                        EventAreaId = reader.GetInt32(1),
                        Row = reader.GetInt32(2),
                        Number = reader.GetInt32(3),
                        State = reader.GetInt32(4),
                    };
                }

                command.Dispose();

                return result;
            }
        }

        public void Update(EventSeatEntity item)
        {
            string query = "UPDATE [EventSeat] " +
                           "SET [EventAreaId] = @item.EventAreaId, [Row] = @item.Row, [Number] = @item.Number, [State] = @item.Number " +
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
