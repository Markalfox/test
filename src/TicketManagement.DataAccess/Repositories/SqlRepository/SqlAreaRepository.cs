using System;
using System.Collections.Generic;
using System.Data;
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
            AreaEntity result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Area] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader = command.ExecuteReader();

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
                }
            }

            return result;
        }

        public void Create(AreaEntity item)
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
                        command.CommandText = "INSERT INTO [Area] VALUES (@layoutId, @description, @coordX, @coordY)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@layoutId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));
                        command.Parameters.Add(new SqlParameter("@coordX", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@coordY", SqlDbType.Int));

                        command.Parameters["@layoutId"].Value = item.LayoutId;
                        command.Parameters["@description"].Value = item.Description;
                        command.Parameters["@coordX"].Value = item.CoordX;
                        command.Parameters["@coordY"].Value = item.CoordY;

                        command.ExecuteNonQuery();
                    }
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE [Area] SET [LayoutId] = @layoutId, [Description] = @description, [CoordX] = @coordX, [CoordY] = @coordY WHERE [Id] = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(new SqlParameter("@layoutId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 200));
                        command.Parameters.Add(new SqlParameter("@coordX", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@coordY", SqlDbType.Int));

                        command.Parameters["@layoutId"].Value = item.LayoutId;
                        command.Parameters["@description"].Value = item.Description;
                        command.Parameters["@coordX"].Value = item.CoordX;
                        command.Parameters["@coordY"].Value = item.CoordY;

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
                    command.CommandText = "DELETE FROM [Area] WHERE [Id] = @id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
