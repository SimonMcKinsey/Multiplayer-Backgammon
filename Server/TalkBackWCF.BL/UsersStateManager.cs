using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace TalkBackWCF.BL
{
    public class UsersStateManager
    {
        public static UserDTO TryGetOnlineUser(Guid authentication) //return online user by authentication - if not exist return null
        {
            UserDTO user;
            UsersStateInfo.Instance.OnlineUsers.TryGetValue(authentication, out user);
            return user;
        }
        public static void AddUserToPlayingList(string userName)//adding users that confirmed a game to the list
        {
            UsersStateInfo.Instance.AlreadyPlaying.Add(new UserDTO { UserName = userName });
        }
        public static bool CheckIfUserIsPlaying(string userName)
        {
           IUserName user = UsersStateInfo.Instance.AlreadyPlaying
                                    .Where(u => u.UserName == userName).FirstOrDefault();
            if(user == null)
            {
                return false;
            }
            return true;
        }
        public static bool CheckIfUserIsOnline(string userName)
        {

            UserDTO user = UsersStateInfo.Instance.OnlineUsers
                           .Where(u => u.Value.UserName == userName)
                           .FirstOrDefault().Value;
            bool isAlreadyLogedin = user == null ? false : true;
            return isAlreadyLogedin;
        }

        internal static Guid TryGetAuthenticationByUserName(string userName)
        {
          Guid onlineUser =  UsersStateInfo.Instance.OnlineUsers
                .Where(u => u.Value.UserName == userName).Select(pair => pair.Key).FirstOrDefault();
            return onlineUser;
        }
    }
}
