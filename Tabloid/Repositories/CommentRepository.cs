using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (PostId, UserProfileId, Subject, Content, CreateDateTime)
                        OUTPUT INSERTED.Id
                        VALUES (@postId, @userProfileId, @subject, @content, @createDateTime)
                        ";
                    DbUtils.AddParameter(cmd, "@postId", comment.PostId);
                    DbUtils.AddParameter(cmd, "@userProfileId", comment.UserProfileId);
                    DbUtils.AddParameter(cmd, "@subject", comment.Subject);
                    DbUtils.AddParameter(cmd, "@content", comment.Content);
                    DbUtils.AddParameter(cmd, "@createDateTime", comment.CreateDateTime);
                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Comment WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment
                        SET (Subject = @subject, Content = @content)
                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@subject", comment.Subject);
                    DbUtils.AddParameter(cmd, "@content", comment.Content);
                    DbUtils.AddParameter(cmd, "@id", comment.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Comment> GetByPostId(int postId)
        {
            using (var conn = Connection)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, UserProfileId, Subject, Content, CreateDateTime FROM Comment
                        WHERE PostId = @postId;";
                    DbUtils.AddParameter(cmd, "@postId", postId);
                    using(var reader = cmd.ExecuteReader())
                    {
                        var comments = new List<Comment>();
                        while (reader.Read())
                        {
                            var comment = new Comment()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PostId = postId,
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                            };
                            comments.Add(comment);
                        }
                        return comments;
                    }
                }
            }
        }
    }
}
