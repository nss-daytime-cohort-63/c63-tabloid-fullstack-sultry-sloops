using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }

        public List<Post> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT p.Id, p.Title, p.Content, p.ImageLocation as 'PostImage', p.CreateDateTime as 'PostCreate', p.PublishDateTime, p.IsApproved, p.CategoryId, 
                
                       up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId, 

                        ut.Name as 'UserTypeName' 

                        FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                        
                        JOIN UserType ut on up.UserTypeId = ut.Id 

                       ORDER BY p.PublishDateTime ASC
                            ";

                    List<Post> posts = new List<Post>();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            Content = DbUtils.GetString(reader, "Content"),
                            ImageLocation = DbUtils.GetNullableString(reader, "PostImage"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "PostCreate"),
                            PublishDateTime = DbUtils.GetNullableDateTime(reader, "PublishDateTime"),
                            IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            UserProfileId = DbUtils.GetInt(reader, "UserId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "UserCreate"),
                                ImageLocation = DbUtils.GetNullableString(reader, "UserImage"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "UserTypeId"),
                                    Name = DbUtils.GetString(reader, "UserTypeName"),
                                }
                            }

                        };

                        posts.Add(post);
                    }
                    return posts;

                }
            }
        }





    }
}
