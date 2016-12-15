using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserDTO GetUserBy(string UserName, string Password)
        {
            using (var context = new TalkBackDBContext())
            {
                User user = context.Users.Where(u => u.UserName == UserName && u.Password == Password).FirstOrDefault();
                return ConvertToDTO(user);
            }
        }
        public List<UserDTO> GetAllUsers()
        {
            using (var context = new TalkBackDBContext())
            {
                var users = context.Users.ToList(); //TODO: can select be before to list without cast
                return users.Select(u => ConvertToDTO(u)).ToList();
            }
        }

        private UserDTO ConvertToDTO(User user)
        {
            if (user != null)
            {
                return new UserDTO { UserName = user.UserName, Password = user.Password };
            }
            return null;
        }
        private User ConvertToModel(UserDTO user)
        {
            if(user != null)
            {
            return new User { UserName = user.UserName, Password = user.Password };

            }
            return null;
        }
        private User ConvertToModel(ISignedUp user)
        {
            if (user != null)
            {
                return new User { UserName = user.UserName, Password = user.Password };

            }
            return null;
        }

        public void AddUser(ISignedUp user)
        {
            using(var context = new TalkBackDBContext())
            {
                User userDB = ConvertToModel(user);
                context.Users.Add(userDB);
                context.SaveChanges();
            }
        }
    }

}
