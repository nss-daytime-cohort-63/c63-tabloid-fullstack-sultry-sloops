using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }


        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name]
                        FROM Tag
                        ORDER BY Name ASC";
                    var reader = cmd.ExecuteReader();
                    var tags = new List<Tag>();
                    while (reader.Read())
                    {
                        var tag = new Tag()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        tags.Add(tag);
                    }
                    return tags;

                }
            }
        }
        public Tag Get(int id)
        {
            throw new System.NotImplementedException();
        }
        public void Add(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tag ([Name])
                        OUTPUT INSERTED.Id
                        VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", tag.Name);
                    tag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Tag WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Update(Tag tag)
        {
            throw new System.NotImplementedException();
        }
    }
}
