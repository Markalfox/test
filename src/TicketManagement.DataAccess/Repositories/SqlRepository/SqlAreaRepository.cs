using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories.SqlRepository
{
    public class SqlAreaRepository : IRepository<AreaEntity>
    {
        // FIELDS
        private readonly string _connectionString;

        // CONSTRUCTORS
        public SqlAreaRepository(string connection)
            => _connectionString = connection;

        // METHODS
        public IEnumerable<AreaEntity> GetAll()
        {
            string query = "SELECT * FROM [Area]";
            List<AreaEntity> areas = new List<AreaEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        areas.Add(new AreaEntity
                        {
                            Id = reader.GetInt32(0),
                            LayoutId = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            CoordX = reader.GetInt32(3),
                            CoordY = reader.GetInt32(4),
                        });
                    }
                }

                return areas;
            }
        }

        public AreaEntity GetById(int id)
        {
            string query = "SELECT * FROM [Area] " +
                           "WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                AreaEntity result = null;

                while (reader.Read())
                {
                    result = new AreaEntity
                    {
                        Id = reader.GetInt32(0),
                        LayoutId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        CoordX = reader.GetInt32(3),
                        CoordY = reader.GetInt32(4),
                    };
                }

                command.Dispose();

                return result;
            }
        }

        public void Create(AreaEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "INSERT INTO [Area] ([Id], [LayoutId], [Description], [CoordX], [CoordY]) " +
                               "VALUES ('@item.Id', '@item.LayoutId', '@item.Description', '@item.CoordX', '@item.CoordY')";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }

        public void Update(AreaEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                string query = "UPDATE [Area] " +
                               "SET [LayoutId] = @item.LayoutId, [Description] = @item.Description, [CoordX] = @item.CoordX, [CoordY] = @item.CoordY " +
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

        public void Delete(int id)
        {
            string query = "DELETE FROM [Area] " +
                           "WHERE [Id] = @id";

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
