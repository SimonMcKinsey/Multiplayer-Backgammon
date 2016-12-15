using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Services;
using WPFClientDemo.InfraStructure;

namespace WPFClientDemo.Models
{

    public class ChatServiceProxy : IChatService, IChatCallback //TODO: singleton?
    {
        private IChatService proxy; //channel to the service
        DuplexChannelFactory<IChatService> factory;
        public event EventHandler<OnMessageSentEventArgs> OnMessageSentEvent;

        //singelton implementation
        private static readonly object padlock = new object();
        private static ChatServiceProxy instance = null;
        public static ChatServiceProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ChatServiceProxy();
                    }
                    return instance;
                }
            }

        }
        private ChatServiceProxy()
        {
            factory = new DuplexChannelFactory<IChatService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service  
        }
        public JoinChatServiceResponse JoinService(JoinChatServiceRequest joinServiceRequest)
        {
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLogedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLogedUser.Authentication.ToString());
                JoinChatServiceResponse response = proxy.JoinService(joinServiceRequest);
                return response;
            }
        }

        public bool OnMessageSent(OnMessageSentRequest messageFromUser)
        {
            OnMessageSentEvent?.Invoke(this, new OnMessageSentEventArgs { UserName = messageFromUser.UserName, Message = messageFromUser.MessageSent });
            return true;

        }

        public SendMessageResponse SendMessage(SendMessageRequest messageRequest)
        {
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLoggedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLoggedUser.Authentication.ToString());
                try
                {

                    SendMessageResponse response = proxy.SendMessage(messageRequest);
                    return response;
                }
                catch (Exception ex)
                {
                    return new SendMessageResponse { IsSuccess = false, Message = ex.Message };
                }
            }
        }
    }
}

