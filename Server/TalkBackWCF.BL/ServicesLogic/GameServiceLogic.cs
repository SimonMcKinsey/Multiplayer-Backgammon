using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace TalkBackWCF.BL
{
    public class GameServiceLogic
    {
        public GameServiceLogic()
        {

        }

        public Guid GetHeaderAuthentication()
        {
            string authenticationString = RequestHeaderProvider.GetHeaderByName("Authentication");
            return HeadersValueConverter.ConvertToGuid(authenticationString);
        }

        public UserDTO TryGetUserByAuthentication(Guid authentication)
        {
            return UsersStateManager.TryGetOnlineUser(authentication);
        }

        public bool CheckIfUsersAvailable(string userNameSender, string userNameOpponent)
        {
            bool isRequesterPlaying = UsersStateManager.CheckIfUserIsPlaying(userNameSender);
            bool isAccepterPlaying = UsersStateManager.CheckIfUserIsPlaying(userNameOpponent);
            if(isAccepterPlaying || isRequesterPlaying)
            {
                return false;
            }
            AddUsersToPlayingList(userNameSender,userNameOpponent);
            return true;

        }

        private void AddUsersToPlayingList(string userNameSender, string userNameOpponent)
        {
            UsersStateManager.AddUserToPlayingList(userNameSender);
            UsersStateManager.AddUserToPlayingList(userNameOpponent);
        }
    }
}
