using Tabloid.Models;
using System.Collections.Generic;

namespace Tabloid.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag Get(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
    }
}
