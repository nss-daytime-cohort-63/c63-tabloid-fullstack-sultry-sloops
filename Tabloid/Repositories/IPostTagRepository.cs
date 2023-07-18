using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IPostTagRepository
    {
        void AddPostTag(PostTag postTag);
        void DeletePostTag(int id);
        List<PostTag> GetAll();
        List<PostTag> GetAllByPostId(int id);
    }
}