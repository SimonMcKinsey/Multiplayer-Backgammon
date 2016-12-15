using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;

namespace TalkBackWCF.BL
{
    public class ChatServiceLogic
    {
        public Guid GetHeaderAuthentication()
        {
            string authenticationString = RequestHeaderProvider.GetHeaderByName("Authentication");
            return HeadersValueConverter.ConvertToGuid(authenticationString);
        }
        public UserDTO TryGetUserByAuthentication(Guid authentication) //TODO: duplication like UserService, Refactor?
        {
            UserDTO user;
            UsersStateInfo.Instance.OnlineUsers.TryGetValue(authentication, out user);
            return user;
        }
        //public UserDTO TrySendMessage(SendMessageRequest messageRequest, Guid authentication)
        //{
        //    return null;
           
        //}
    }
}
