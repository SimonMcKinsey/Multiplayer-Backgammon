using System.ServiceModel;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;

namespace TalkBackWCF.Contract.Services
{
    [ServiceContract]
    public interface IChatCallback
    {
        [OperationContract]
        bool OnMessageSent(OnMessageSentRequest messageFromUser);
    }
}