using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using( var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select u.id as Id,u.isActive,u.UserTypeId,DisplayName,FirstName,LastName, t.name as Type from UserProfile u 
                    join UserType t on UserTypeId = t.Id where u.isActive = 1 order by u.DisplayName";
                    using( SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var user = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                isActive =reader.GetBoolean(reader.GetOrdinal("isActive")),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "UserTypeId"),
                                    Name = DbUtils.GetString(reader, "Type"),
                                }
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }

        }
        public List<UserProfile> GetAllAdmins()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from UserProfile where UserTypeId = 1 and isActive = 1 ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var user = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                isActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                               
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }

        }
        public List<UserProfile> GetAllDeactive()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select u.id as Id,u.isActive,u.UserTypeId,DisplayName,FirstName,LastName, t.name as Type from UserProfile u 
                    join UserType t on UserTypeId = t.Id where u.isActive = 0 order by u.DisplayName";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var user = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                isActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "UserTypeId"),
                                    Name = DbUtils.GetString(reader, "Type"),
                                }
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }

        }

        public UserProfile Get(int id)
        {
            using( var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select u.id as Id,u.isActive, u.UserTypeId,CreateDateTime,ImageLocation,
                    Email,DisplayName,FirstName,LastName, t.name as 
                    Type from UserProfile u join UserType t on UserTypeId = t.Id where u.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile user = null;
                        if (reader.Read())
                        {
                            user = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                Email=DbUtils.GetString(reader, "Email"),
                                isActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "UserTypeId"),
                                    Name = DbUtils.GetString(reader, "Type"),
                                }
                            };
                            if (!reader.IsDBNull(reader.GetOrdinal("ImageLocation")))
                            {
                                user.ImageLocation = reader.GetString(reader.GetOrdinal("ImageLocation"));
                            }
                        }
                        return user;
                    }
                }
            }
        }
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.FirstName, up.LastName, up.DisplayName, 
                               up.Email, up.CreateDateTime, up.ImageLocation, up.UserTypeId,
                               ut.Name AS UserTypeName
                          FROM UserProfile up
                               LEFT JOIN UserType ut on up.UserTypeId = ut.Id
                         WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                            UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                            UserType = new UserType()
                            {
                                Id = DbUtils.GetInt(reader, "UserTypeId"),
                                Name = DbUtils.GetString(reader, "UserTypeName"),
                            }
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using( var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update UserProfile set UserTypeId = @userId Where Id = @id";
                    DbUtils.AddParameter(cmd,"@id",userProfile.Id);
                    DbUtils.AddParameter(cmd, "@userId", userProfile.UserTypeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (FirebaseUserId, FirstName, LastName, DisplayName, 
                                                                 Email, CreateDateTime, ImageLocation, UserTypeId,isActive)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @FirstName, @LastName, @DisplayName, 
                                                @Email, @CreateDateTime, @ImageLocation, @UserTypeId,1)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@FirstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@DisplayName", userProfile.DisplayName);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", userProfile.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@ImageLocation", userProfile.ImageLocation);
                    DbUtils.AddParameter(cmd, "@UserTypeId", userProfile.UserTypeId);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void DeactivateUser(UserProfile user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update UserProfile
                                        Set isActive = 0
                                        Where Id = @id";
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ReactivateUser(UserProfile user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update UserProfile
                                        Set isActive = 1
                                        Where Id = @id";
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        /*
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            return _context.UserProfile
                       .Include(up => up.UserType) 
                       .FirstOrDefault(up => up.FirebaseUserId == firebaseUserId);
        }

        public void Add(UserProfile userProfile)
        {
            _context.Add(userProfile);
            _context.SaveChanges();
        }
        */
    }
}
