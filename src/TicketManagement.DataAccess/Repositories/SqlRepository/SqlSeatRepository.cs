using System;
using System.Collections.Generic;
using System.Data;
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Seat] VALUES (@areaId, @row, @number)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@areaId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@row", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("number", SqlDbType.Int));

                        command.Parameters["@areaId"].Value = item.AreaId;
                        command.Parameters["@row"].Value = item.Row;
                        command.Parameters["@number"].Value = item.Number;

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
                    command.CommandText = "DELETE * FROM [Seat] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
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
            SeatEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Seat] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader = command.ExecuteReader();

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
                }
            }

            return result;
        }

        public void Update(SeatEntity item)
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
                        command.CommandText = "UPDATE [Seat] SET [AreaId] = @areaId, [Row] = @row, [Number] = @number WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@areaId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@row", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@number", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                        command.Parameters["@areaId"].Value = item.AreaId;
                        command.Parameters["@row"].Value = item.Row;
                        command.Parameters["@number"].Value = item.Number;
                        command.Parameters["@id"].Value = item.Id;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
