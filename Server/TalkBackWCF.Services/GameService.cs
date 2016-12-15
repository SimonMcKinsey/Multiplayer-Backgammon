using DevTrends.WCFDataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Services;
using TalkBackWCF.Contract.Entities.GameServiceEntities;
using TalkBackWCF.BL;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;

namespace TalkBackWCF.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ValidateDataAnnotationsBehavior]
    public class GameService : IGameService
    {
        private GameServiceLogic serviceLogic;
        private Dictionary<string, IGameCallback> callbacksByUserName; //callback by UserName


        public GameService()
        {
            serviceLogic = new GameServiceLogic();
            callbacksByUserName = new Dictionary<string, IGameCallback>();

        }

        public GameResponse Game(GameRequest gameRequest)
        {
            //check authenticaiton:
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO userGameSender = serviceLogic.TryGetUserByAuthentication(authentication);
            if (userGameSender == null)
            {
                return new GameResponse { IsSuccess = false, Message = "User failed to authenticate" };
            }
            //check if the opponent is online and can get the request:
            IGameCallback callback;
            bool IsRecipientOnline = callbacksByUserName.TryGetValue(gameRequest.UserOpponent, out callback);
            if (IsRecipientOnline == false)
            {
                return new GameResponse { IsSuccess = false, Message = "sorry the user can't get that Game Request right now" };
            }
            //check if one of the players is already playing
            bool AreUsersAvailable = serviceLogic.CheckIfUsersAvailable(userGameSender.UserName, gameRequest.UserOpponent);
            if (AreUsersAvailable == false)
            {
                return new GameResponse { IsSuccess = false, Message = "You or Your Opponent are already playing" };
            }
            //check if opponent accepted the game invitation
            bool HasOpponentAcceptedRequest = callback.OnGame(new OnGameRequest { UserName = userGameSender.UserName });
            if (HasOpponentAcceptedRequest == false)
            {
                return new GameResponse { IsSuccess = false, Message = "the opponent refused to accept your game invitation" };
            }

            return new GameResponse { IsSuccess = true, Message = "invitation accepted" };


        }

        public JoinGameServiceResponse JoinService(JoinGameServiceRequest joinServiceRequest)
        {
            //checking user authentication:
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO UserGameSender = serviceLogic.TryGetUserByAuthentication(authentication);
            if (UserGameSender == null)//if user exist
            {
                return new JoinGameServiceResponse { IsSuccess = false, Message = "user failed to authenticate you are not allowed to join this service" };
            }
            //if user is authenticated - adding him to the chat service:
            IGameCallback callback = OperationContext.Current.GetCallbackChannel<IGameCallback>();
            if (!callbacksByUserName.ContainsKey(UserGameSender.UserName))
            {//from now on, this user will be able to get messages.
                callbacksByUserName.Add(UserGameSender.UserName, callback);
            }
            return new JoinGameServiceResponse { IsSuccess = true, Message = "Joined the game service succesfully" };
        }

        public InGameResponse InGame(InGameRequest inGameRequest)
        {
            //check authenticaiton:
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO userGameSender = serviceLogic.TryGetUserByAuthentication(authentication);
            if (userGameSender == null)
            {
                return new InGameResponse { IsSuccess = false, Message = "User failed to authenticate" };
            }
            //check if the opponent is online and can get the request:
            IGameCallback callback;
            bool IsRecipientOnline = callbacksByUserName.TryGetValue(inGameRequest.UserOpponent, out callback);
            if (IsRecipientOnline == false)
            {
                return new InGameResponse { IsSuccess = false, Message = "sorry the user can't get that Game Request right now" };
            }
            callback.OnInGame(new OnInGameRequest { UserName = userGameSender.UserName, SlotIdSource = inGameRequest.SlotIdSource, SlotIdDestination = inGameRequest.SlotIdDestination });
            return new InGameResponse { IsSuccess = true, Message = "your move has sent to your opponent" };
        }
    }
}
