using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        public List<UserProfile> GetAll();

        public List<UserProfile> GetAllDeactive();
        public UserProfile Get(int id);
        public void Update(UserProfile userProfile);

        public void DeactivateUser(UserProfile user);

        public void ReactivateUser(UserProfile user);

        public List<UserProfile> GetAllAdmins();
    }
}