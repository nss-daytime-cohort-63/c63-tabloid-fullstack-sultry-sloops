using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class PostTagRepository : BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration configuration) : base(configuration) { }

        public List<PostTag> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  pt.id as 'PostTagId', pt.PostId, pt.TagId,
                                        p.Title,p.Content, p.ImageLocation, p.CreateDateTime as 'PostCreate', p.PublishDateTime as 'PostPublish', p.IsApproved,
                                        p.CategoryId, p.UserProfileId,
                                        t.Name as 'TagName', c.Name as 'CategoryName',
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId,
                                        ut.Name as 'UserTypeName'
                                        FROM PostTag pt 
                                        join Post p on pt.PostId = p.Id 
                                        join Tag t on pt.TagId = t.Id
                                        join Category c on p.CategoryId = c.Id
                                        join UserProfile up on p.UserProfileId = up.Id
                                        join UserType ut on up.UserTypeId = ut.Id";

                    List<PostTag> postTags = new List<PostTag>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PostTag postTag = new PostTag()
                        {
                            Id = DbUtils.GetInt(reader, "PostTagId"),
                            PostId = DbUtils.GetInt(reader, "PostId"),
                            TagId = DbUtils.GetInt(reader, "TagId"),
                            Post = new Post()
                            {
                                Id = DbUtils.GetInt(reader, "PostId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Content = DbUtils.GetString(reader, "Content"),
                                ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "PostCreate"),
                                PublishDateTime = DbUtils.GetNullableDateTime(reader, "PostPublish"),
                                IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
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

                            },
                            Tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "TagId"),
                                Name = DbUtils.GetString(reader, "TagName")
                            }

                        };

                        postTags.Add(postTag);
                    }
                    return postTags;
                }
            }
        }

        public List<PostTag> GetAllByPostId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  pt.id as 'PostTagId', pt.PostId, pt.TagId,
                                        p.Title,p.Content, p.ImageLocation, p.CreateDateTime as 'PostCreate', p.PublishDateTime as 'PostPublish', p.IsApproved,
                                        p.CategoryId, p.UserProfileId,
                                        t.Name as 'TagName', c.Name as 'CategoryName',
                                        up.Id as 'UserId', up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime as 'UserCreate', up.ImageLocation as 'UserImage', up.UserTypeId,
                                        ut.Name as 'UserTypeName'
                                        FROM PostTag pt 
                                        join Post p on pt.PostId = p.Id 
                                        join Tag t on pt.TagId = t.Id
                                        join Category c on p.CategoryId = c.Id
                                        join UserProfile up on p.UserProfileId = up.Id
                                        join UserType ut on up.UserTypeId = ut.Id
                                        join category cat on p.CategoryId = cat.Id
                                        WHERE pt.PostId = @id";

                    DbUtils.AddParameter(cmd, "@id", id);


                    List<PostTag> postTags = new List<PostTag>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PostTag postTag = new PostTag()
                        {
                            Id = DbUtils.GetInt(reader, "PostTagId"),
                            PostId = DbUtils.GetInt(reader, "PostId"),
                            TagId = DbUtils.GetInt(reader, "TagId"),
                            Post = new Post()
                            {
                                Id = DbUtils.GetInt(reader, "PostId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Content = DbUtils.GetString(reader, "Content"),
                                ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "PostCreate"),
                                PublishDateTime = DbUtils.GetNullableDateTime(reader, "PostPublish"),
                                IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                },
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

                            },
                            Tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "TagId"),
                                Name = DbUtils.GetString(reader, "TagName")
                            }

                        };

                        postTags.Add(postTag);
                    }
                    return postTags;
                }
            }
        }

        public void AddPostTag(PostTag postTag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostTag (PostId, TagId) OUTPUT INSERTED.ID VALUES(@pid, @tid)";
                    DbUtils.AddParameter(cmd, "@pid", postTag.PostId);
                    DbUtils.AddParameter(cmd, "@tid", postTag.TagId);

                    postTag.Id = (int)cmd.ExecuteScalar();
                    
                }
            }
        }
    }
}
