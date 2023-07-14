using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Category> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT *
                            FROM Category 
                            ";
                    List<Category> categories = new List<Category>();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")

                        };

                        categories.Add(category);
                    }

                    return categories;
                }
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, Name
                    FROM Category
                    WHERE Id = @id
                    ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category category = new Category()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };

                            return category;
                        }

                        return null;
                    }
                }
            }
        }
        public void AddCategory(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Category (Name)
                    OUTPUT INSERTED.ID
                    VALUES (@name)
                ";
                    cmd.Parameters.AddWithValue("@name", category.Name);

                    category.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void DeleteCategory(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Category
                        WHERE Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
            public void UpdateCategory(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Category
                        SET
                            [Name] = @name
                        WHERE Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.Parameters.AddWithValue("@id", category.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
