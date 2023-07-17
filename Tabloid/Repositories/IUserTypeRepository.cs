using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IUserTypeRepository
    {
        List<UserType> GetAll();
    }
}