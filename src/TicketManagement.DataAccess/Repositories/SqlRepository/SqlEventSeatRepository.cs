using System;
using System.Collections.Generic;
using System.Data;
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [EventSeat] VALUES (@eventAreaId, @row, @number, @state)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@eventAreaId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@row", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@number", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@state", SqlDbType.Int));

                        command.Parameters["@eventAreaId"].Value = item.EventAreaId;
                        command.Parameters["@row"].Value = item.Row;
                        command.Parameters["@number"].Value = item.Number;
                        command.Parameters["@state"].Value = item.State;

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
                    command.CommandText = "DELETE * FROM [EventSeat] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
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
            EventSeatEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [EventSeat] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader = command.ExecuteReader();

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
                }
            }

            return result;
        }

        public void Update(EventSeatEntity item)
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
                        command.CommandText = "UPDATE [EventSeat] SET [EventAreaId] = @eventAreaId, [Row] = @row, [Number] = @number, [State] = @state";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@eventAreaId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@row", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@number", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@state", SqlDbType.Int));

                        command.Parameters["@eventAreaId"].Value = item.EventAreaId;
                        command.Parameters["@row"].Value = item.Row;
                        command.Parameters["@number"].Value = item.Number;
                        command.Parameters["@state"].Value = item.State;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
