using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;

namespace TalkBackWCF.Contract.Services
{
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract]
        JoinChatServiceResponse JoinService(JoinChatServiceRequest joinServiceRequest);
        [OperationContract]
        SendMessageResponse SendMessage(SendMessageRequest messageRequest);
    }
}
