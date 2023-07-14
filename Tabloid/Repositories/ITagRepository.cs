using Tabloid.Models;
using System.Collections.Generic;

namespace Tabloid.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
    }
}
