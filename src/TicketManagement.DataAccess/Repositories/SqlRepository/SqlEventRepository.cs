using System;
using System.Collections.Generic;
using System.Data;
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Event] VALUES (@name, @description, @layoutId, @startDate, @endDate)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));
                        command.Parameters.Add(new SqlParameter("@layoutId", SqlDbType.Int));
                        command.Parameters.Add("@startDate", SqlDbType.DateTime);
                        command.Parameters.Add("@endDate", SqlDbType.DateTime);

                        command.Parameters["@name"].Value = item.Name;
                        command.Parameters["@description"].Value = item.Description;
                        command.Parameters["@layoutId"].Value = item.LayoutId;
                        command.Parameters["@startDate"].Value = item.StartDate;
                        command.Parameters["@endDate"].Value = item.EndDate;

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
                    command.CommandText = "DELETE * FROM [Event] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
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
                        StartDate = reader.GetDateTime(4),
                        EndDate = reader.GetDateTime(5),
                    });
                }

                command.Dispose();

                return events;
            }
        }

        public EventEntity GetById(int id)
        {
            EventEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Event] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new EventEntity
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            LayoutId = reader.GetInt32(3),
                            StartDate = reader.GetDateTime(4),
                            EndDate = reader.GetDateTime(5),
                        };
                    }
                }
            }

            return result;
        }

        public void Update(EventEntity item)
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
                        command.CommandText = "UPDATE [Event] " +
                                              "SET [Name] = @name, [Description] = @description, [LayoutId] = @layoutId, [StartDate] = @startDate, [EndDate] = @endDate " +
                                              "WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));
                        command.Parameters.Add(new SqlParameter("@layoutId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@startDate", SqlDbType.DateTime));
                        command.Parameters.Add(new SqlParameter("@endDate", SqlDbType.DateTime));

                        command.Parameters["@name"].Value = item.Name;
                        command.Parameters["@description"].Value = item.Description;
                        command.Parameters["@layoutId"].Value = item.LayoutId;
                        command.Parameters["@startDate"].Value = item.StartDate;
                        command.Parameters["@endDate"].Value = item.EndDate;

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
