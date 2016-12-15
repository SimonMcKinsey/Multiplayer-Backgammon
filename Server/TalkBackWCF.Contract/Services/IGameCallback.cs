using System.ServiceModel;
using TalkBackWCF.Contract.Entities.GameServiceEntities;

namespace TalkBackWCF.Contract.Services
{
    [ServiceContract]
    public interface IGameCallback
    {
        [OperationContract]
        bool OnGame(OnGameRequest requestFromUser);
        [OperationContract]
        void OnInGame(OnInGameRequest opponentMove);
    }
}