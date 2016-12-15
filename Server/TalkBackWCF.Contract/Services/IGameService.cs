using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Entities.GameServiceEntities;

namespace TalkBackWCF.Contract.Services
{
    [ServiceContract(CallbackContract = typeof(IGameCallback))]
    public interface IGameService
    {
        [OperationContract]
        JoinGameServiceResponse JoinService(JoinGameServiceRequest joinServiceRequest);
        [OperationContract]
        GameResponse Game(GameRequest gameRequest);
        [OperationContract]
        InGameResponse InGame(InGameRequest inGameRequest);
    }
}
