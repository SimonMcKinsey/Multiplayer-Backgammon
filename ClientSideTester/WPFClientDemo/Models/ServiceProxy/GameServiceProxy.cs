using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Entities.GameServiceEntities;
using TalkBackWCF.Contract.Services;
using WPFClientDemo.InfraStructure;
using WPFClientDemo.InfraStructure.EventArgsFolder;

namespace WPFClientDemo.Models.ServiceProxy
{

    public class GameServiceProxy : IGameService,IGameCallback
    {
        DuplexChannelFactory<IGameService> factory;
        private IGameService proxy; //channel to the service

        public event EventHandler<OnGameEventArgs> OnGameEvent;
        public event EventHandler<OnInGameEventArgs> OnInGameEvent;
        //singelton implementation
        private static readonly object padlock = new object();
        private static GameServiceProxy instance = null;
        public static GameServiceProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameServiceProxy();
                    }
                    return instance;
                }
            }

        }
        private GameServiceProxy()
        {
            factory = new DuplexChannelFactory<IGameService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service
        }

        public JoinGameServiceResponse JoinService(JoinGameServiceRequest joinServiceRequest)
        {
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLogedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLogedUser.Authentication.ToString());
                JoinGameServiceResponse response = proxy.JoinService(joinServiceRequest);
                return response;
            }
        }

        public GameResponse Game(GameRequest gameRequest)
        {
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLogedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLogedUser.Authentication.ToString());
                GameResponse response = proxy.Game(gameRequest);
                return response;
            }
        }

        public bool OnGame(OnGameRequest requestFromUser)
        {
            OnGameEventArgs eventArgs = new OnGameEventArgs { UserName = requestFromUser.UserName };
            OnGameEvent?.DynamicInvoke(this,eventArgs );
            //TODO : get answer from client if he want to play\
            if(eventArgs.Cancel == true)
            {

            }
            return true;
        }

        public InGameResponse InGame(InGameRequest inGameRequest)
        {
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLogedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLogedUser.Authentication.ToString());
                InGameResponse response = proxy.InGame(inGameRequest);
                return response;
            }
        }

        public void OnInGame(OnInGameRequest opponentMove)
        {
            OnInGameEvent?.Invoke(this, new OnInGameEventArgs { UserName = opponentMove.UserName, SlotIdSource = opponentMove.SlotIdSource, SlotIdDestination = opponentMove.SlotIdDestination });
        }
    }
}
