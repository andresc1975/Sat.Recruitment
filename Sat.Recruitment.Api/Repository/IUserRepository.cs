using Sat.Recruitment.Api.Domain;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);

    }
}
