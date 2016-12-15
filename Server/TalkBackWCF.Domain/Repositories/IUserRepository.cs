using System.Collections.Generic;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Domain.Repositories
{
    public interface IUserRepository
    {
        UserDTO GetUserBy(string UserName, string Password);
        List<UserDTO> GetAllUsers();
        void AddUser(ISignedUp user);
    }
}