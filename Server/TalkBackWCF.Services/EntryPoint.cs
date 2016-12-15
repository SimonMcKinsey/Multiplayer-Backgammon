using DevTrends.WCFDataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.BL;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Contract.Services;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Entities.GameServiceEntities;

namespace TalkBackWCF.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ValidateDataAnnotationsBehavior]
    public partial class EntryPoint : IUserService// will contain all services and be a the main endpoint
    {
        public UserService UserService { get; set; }
        public ChatService ChatService { get; set; }
        public GameService GameService { get; set; }
        public EntryPoint()
        {
            UserService = new UserService();
            ChatService = new ChatService();
            GameService = new GameService();
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
             return UserService.Login(loginRequest);
        }

        public LogoutResponse Logout(LogoutRequest logoutRequest)
        {
            return UserService.Logout(logoutRequest);
        }

        public SignUpResponse SignUp(SignUpRequest signUpRequest)
        {
            return UserService.SignUp(signUpRequest);
        }

   

       
    } //IUserService
    public partial class EntryPoint : IChatService
    {
        public JoinChatServiceResponse JoinService(JoinChatServiceRequest joinServiceRequest)
        {
            return ChatService.JoinService(joinServiceRequest);
        }

        public SendMessageResponse SendMessage(SendMessageRequest messageRequest)
        {
            return ChatService.SendMessage(messageRequest);
        }
    } //IChatService

    public partial class EntryPoint : IGameService
    {
        public JoinGameServiceResponse JoinService(JoinGameServiceRequest joinServiceRequest)
        {
            return GameService.JoinService(joinServiceRequest);
        }
        public GameResponse Game(GameRequest gameRequest)
        {
            return GameService.Game(gameRequest);
        }

        public InGameResponse InGame(InGameRequest inGameRequest)
        {
            return GameService.InGame(inGameRequest);
        }
    } //IGameService
}
