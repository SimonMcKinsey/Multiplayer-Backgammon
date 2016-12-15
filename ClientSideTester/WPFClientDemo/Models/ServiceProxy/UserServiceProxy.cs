using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Services;
using WPFClientDemo;
using WPFClientDemo.InfraStructure;
using WPFClientDemo.Models;

namespace TalkBack.ClientDemo
{
    public class UserServiceProxy : IUserService, IUserCallback //TODO: Singleton?
    {
        private IUserService proxy; //channel to the service
        public event EventHandler<OnLoginEventArgs> OnLoginEvent;
        public event EventHandler<OnLogoutEventArgs> OnLogoutEvent;
        public EventHandler<OnSignedUpEventArgs> OnSignedUpEvent { get; set; }
        DuplexChannelFactory<IUserService> factory;

        //singelton implementation
        private static readonly object padlock = new object();
        private static UserServiceProxy instance = null;
        public static UserServiceProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserServiceProxy();
                    }
                    return instance;
                }
            }

        }

        private UserServiceProxy()
        {
            //var factory = new DuplexChannelFactory<IUserService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888")); //creating the ChannelFactory
            factory = new DuplexChannelFactory<IUserService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
            try
            {
                LoginResponse response = proxy.Login(loginRequest);
            return response;

            }
            catch(Exception e)
            {
                return new LoginResponse { IsSuccess = false, Message = e.Message };
            }

        }

        public LogoutResponse Logout(LogoutRequest logoutRequest)
        {

            // IUserService proxy = factory.CreateChannel();
            using (new OperationContextScope((IClientChannel)proxy))
            {
                AuthenticatedUser currentLogedUser = AuthenticatedUser.Instance;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Authentication", currentLogedUser.Authentication.ToString());
                LogoutResponse response = proxy.Logout(logoutRequest);
                return response;
            }
        }

        public SignUpResponse SignUp(SignUpRequest signUpRequest)
        {
            try
            {

            SignUpResponse response = proxy.SignUp(signUpRequest);
            return response;
            }
            catch(Exception e)
            {
                return new SignUpResponse { IsSuccess = false, Message = e.Message };
            }
        }
        public void OnLogin(OnLoginRequest userLogedin) // call back from server about new user that logged in
        {
            //adding the new logedin to the OnlineUsers:
            ContactStateManager.UpdateContactToOnline(new UserDTO { UserName = userLogedin.UserName });
            //raising the event:
            OnLoginEvent?.Invoke(this, new OnLoginEventArgs { UserName = userLogedin.UserName });
            //TODO: implement a info to the wpf about the user.
            // return new OnLoginResponse { IsSuccess = true, Message = "here the user info", userAlreadyLoged = new UserDTO { UserName = "menahem" } };
        }

        public void OnLogout(OnlogoutRequest userLogedout)
        {
            ContactStateManager.UpdateContactToOffline(new UserDTO { UserName = userLogedout.UserName });
            OnLogoutEvent?.Invoke(this, new OnLogoutEventArgs { UserName = userLogedout.UserName });
        }

        public void OnSignedUp(OnSignedUpRequest userSignedUp)
        {
            ContactStateManager.AddNewSignedContact(new UserDTO { UserName = userSignedUp.UserName });
            OnSignedUpEvent?.Invoke(this, new OnSignedUpEventArgs { UserName = userSignedUp.UserName });
        }

    }
}
