using DevTrends.WCFDataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Services;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.BL;
using TalkBackWCF.Contract;

namespace TalkBackWCF.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ValidateDataAnnotationsBehavior]
    public class ChatService : IChatService
    {
        private ChatServiceLogic serviceLogic;
        private Dictionary<string,IChatCallback> callbacksByUserName;
        public ChatService()
        {
            serviceLogic = new ChatServiceLogic();
            callbacksByUserName = new Dictionary<string, IChatCallback>();
        }
        //inserting callback also to chatService, from now on this user also can get messages:

        public SendMessageResponse SendMessage(SendMessageRequest messageRequest)/////////////////fix callback problam DICTIONARY
        {
            //checking user authentication:
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO UserMessageSender = serviceLogic.TryGetUserByAuthentication(authentication);
            if(UserMessageSender == null)
            {
                return new SendMessageResponse { IsSuccess = false, Message = "user failed to authenticate, you are not allowed to use this service" };
            }
            IChatCallback callback;
            bool IsRecipientOnline = callbacksByUserName.TryGetValue(messageRequest.UserName,out callback);
            if (IsRecipientOnline == false)
            {
                return new SendMessageResponse { IsSuccess = false, Message = "sorry the user can't get that message right now" };
            }
            //trying to send callback:
            try
            {
            bool IsMessageSentSuccefully = callback.OnMessageSent(new OnMessageSentRequest { MessageSent = messageRequest.Message, UserName = UserMessageSender.UserName });
            if(IsMessageSentSuccefully == false)
            {
                return new SendMessageResponse { IsSuccess = false, Message = "user online but couldn't get your message for some reason" };
            }
            return new SendMessageResponse { IsSuccess = true, Message = messageRequest.Message };

            }
            catch
            {
                //removing the disconnected user:
                callbacksByUserName.Remove(messageRequest.UserName);
                return new SendMessageResponse { IsSuccess = false, Message = "sorry the user can't get that message right now " };
            }
        }

        public JoinChatServiceResponse JoinService(JoinChatServiceRequest joinServiceRequest)
        {
            //checking user authentication:
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO UserMessageSender = serviceLogic.TryGetUserByAuthentication(authentication);
            if(UserMessageSender== null)//if user exist
            {
                return new JoinChatServiceResponse { IsSuccess = false, Message = "user failed to authenticate you are not allowed to join this service" };
            }
            //if user is authenticated - adding him to the chat service:
            IChatCallback callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            if (!callbacksByUserName.ContainsKey(UserMessageSender.UserName))
            {//from now on, this user will be able to get messages.
                callbacksByUserName.Add(UserMessageSender.UserName, callback);
            }
            return new JoinChatServiceResponse { IsSuccess = true, Message = "join the chat service succesfully" };
        }
    }
}
