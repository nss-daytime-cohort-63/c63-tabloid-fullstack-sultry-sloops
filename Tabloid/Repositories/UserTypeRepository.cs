using Microsoft.Extensions.Configuration;
using Tabloid.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class UserTypeRepository : BaseRepository, IUserTypeRepository
    {
        public UserTypeRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserType> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from UserType";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var types = new List<UserType>();
                        while (reader.Read())
                        {
                            var type = new UserType()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            };
                            types.Add(type);

                        }
                        return types;
                    }
                }
            }
        }
    }
}
