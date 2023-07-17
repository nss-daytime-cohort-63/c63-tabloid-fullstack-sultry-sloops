using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        List<Post> GetAllPastApprovedPosts();
        List<Post> GetPostByUserId(int userProfileId);
        Post GetPublishedPostById(int id);
    }
}