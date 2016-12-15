using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Domain.Repositories;

namespace TalkBackWCF.BL
{
    public sealed class UsersStateInfo // contain the info about SignedUp Users and Online Users
    {
        public IUserRepository userRepository { get; set; }
        public List<UserDTO> AllSignedUpUsers { get; set; }
        public Dictionary<Guid, UserDTO> OnlineUsers { get; set; }
        public List<IUserName> AlreadyPlaying { get; set; }

        //private static readonly Lazy<UsersStateInfo>
        private static UsersStateInfo instance = null;
        public static UsersStateInfo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UsersStateInfo();
                    }
                    return instance;
                }
            }

        }
        private static readonly object padlock = new object();

        private UsersStateInfo()
        {
            userRepository = new UserRepository();
            InitInstance();
        }

        private void InitInstance()
        {
            AllSignedUpUsers = userRepository.GetAllUsers();
            OnlineUsers = new Dictionary<Guid, UserDTO>();
            AlreadyPlaying = new List<IUserName>();
        }
        public bool IsUserSignedUp(ISignedUp user) //return if there is signed up user with the input userName and Password
        {
            UserDTO userDTO = AllSignedUpUsers.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (userDTO == null)
            {
                return false;
            }
            return true;
        }

        internal void SignUpUser(ISignedUp user)
        {
            userRepository.AddUser(user);
            AllSignedUpUsers.Add(new UserDTO { UserName = user.UserName, Password = user.Password });

        }
    }
}
