using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
                                ut.Name as 'UserTypeName', 
                                c.Id as 'CategoryId', c.Name as 'CategoryName' 
                                FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                                JOIN UserType ut on up.UserTypeId = ut.Id 
                                JOIN Category c on p.CategoryId = c.Id
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
                            },
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            }

                        };

                        posts.Add(post);
                    }
                    return posts;

                }
            }
        }

        public List<Post> GetAllPastApprovedPosts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT p.Id, p.Title, p.Content, p.ImageLocation as 'PostImage', p.CreateDateTime as 'PostCreate', p.PublishDateTime, p.IsApproved, p.CategoryId, 
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId, 
                                        ut.Name as 'UserTypeName', 
                                        c.Id as 'CategoryId', c.Name as 'CategoryName' 
                                        FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                                        JOIN UserType ut on up.UserTypeId = ut.Id
                                        JOIN Category c on p.CategoryId = c.Id
                                        WHERE p.IsApproved = 1 AND p.PublishDateTime < SYSDATETIME()
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
                            },
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            }

                        };

                        posts.Add(post);
                    }
                    return posts;

                }
            }
        }


        public Post GetPublishedPostById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                            SELECT p.Id, p.Title, p.Content, p.ImageLocation as 'PostImage', p.CreateDateTime as 'PostCreate', p.PublishDateTime, p.IsApproved, p.CategoryId, 
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId, 
                                        ut.Name as 'UserTypeName', 
                                        c.Id as 'CategoryId', c.Name as 'CategoryName' 
                                    FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                                        JOIN UserType ut on up.UserTypeId = ut.Id
                                        JOIN Category c on p.CategoryId = c.Id
                                    WHERE p.IsApproved = 1 AND p.PublishDateTime < SYSDATETIME() AND p.Id = @Id
                                        ORDER BY p.PublishDateTime ASC";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Post post = null;

                        if (reader.Read())
                        {
                            post = new Post()
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
                                },
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                }

                            };
                        }

                        return post;
                    }

                }
            }
        }

        public List<Post> GetPostByUserId(int userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.Title, p.Content, p.ImageLocation as 'PostImage', p.CreateDateTime as 'PostCreate', p.PublishDateTime, p.IsApproved, p.CategoryId, 
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId, 
                                        ut.Name as 'UserTypeName', 
                                        c.Id as 'CategoryId', c.Name as 'CategoryName' 
                                    FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                                        JOIN UserType ut on up.UserTypeId = ut.Id
                                        JOIN Category c on p.CategoryId = c.Id
                                    WHERE p.IsApproved = 1 AND p.PublishDateTime < SYSDATETIME() AND p.UserProfileId = @userProfileId
                                        ORDER BY p.PublishDateTime ASC";

                    List<Post> posts = new List<Post>();


                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                                },
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                }

                            };
                            posts.Add(post);
                        }
                        return posts;

                    }
                }
            }
        }


        public List<Post> GetPostByUserId(int userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.Title, p.Content, p.ImageLocation as 'PostImage', p.CreateDateTime as 'PostCreate', p.PublishDateTime, p.IsApproved, p.CategoryId, 
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId, 
                                        ut.Name as 'UserTypeName', 
                                        c.Id as 'CategoryId', c.Name as 'CategoryName' 
                                    FROM Post p JOIN UserProfile up ON p.UserProfileId = up.Id 
                                        JOIN UserType ut on up.UserTypeId = ut.Id
                                        JOIN Category c on p.CategoryId = c.Id
                                    WHERE p.IsApproved = 1 AND p.PublishDateTime < SYSDATETIME() AND p.UserProfileId = @userProfileId
                                        ORDER BY p.PublishDateTime ASC";

                    List<Post> posts = new List<Post>();


                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                                },
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                }

                            };
                            posts.Add(post);
                        }
                        return posts;

                    }
                }
            }
        }


        public void Add(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Post (Title, Content, ImageLocation, CreateDateTime, PublishDateTime, IsApproved, CategoryId, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Content, @ImageLocation, @CreateDateTime, @PublishDateTime, @IsApproved, @CategoryId, @UserProfileId)";
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                    cmd.Parameters.AddWithValue("@Content", post.Content);
                    cmd.Parameters.AddWithValue("@ImageLocation", DbUtils.ValueOrDBNull(post.ImageLocation));
                    cmd.Parameters.AddWithValue("@CreateDateTime", post.CreateDateTime);
                    cmd.Parameters.AddWithValue("@PublishDateTime", DbUtils.ValueOrDBNull(post.PublishDateTime));
                    cmd.Parameters.AddWithValue("@IsApproved", post.IsApproved);
                    cmd.Parameters.AddWithValue("@CategoryId", post.CategoryId);
                    cmd.Parameters.AddWithValue("@UserProfileId", post.UserProfileId);

                    post.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

    }
}
