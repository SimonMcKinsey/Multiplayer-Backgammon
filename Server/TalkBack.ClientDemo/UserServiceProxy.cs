using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;

namespace TalkBack.ClientDemo
{
    class UserServiceProxy : IUserService, IUserCallback //TODO: Singleton?
    {
        private IUserService proxy; //channel to the service
        public event EventHandler<OnLoginEventArgs> OnLoginEvent;
        public UserServiceProxy()
        {
            var factory = new DuplexChannelFactory<IUserService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888")); //creating the ChannelFactory
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
            LoginResponse response = proxy.Login(loginRequest);
            return response;
        }

        public LogoutResponse Logout(LogoutRequest logoutRequest)
        {
            LogoutResponse response = proxy.Logout(logoutRequest);
            return response;
        }

        public void OnLogin(OnLoginRequest userLogedin) // call back from server about new user that logged in
        {
            OnLoginEvent?.Invoke(this, new OnLoginEventArgs { UserName = userLogedin.UserName } );
            //TODO: implement a info to the wpf about the user.
           // return new OnLoginResponse { IsSuccess = true, Message = "here the user info", userAlreadyLoged = new UserDTO { UserName = "menahem" } };
        }

        public void OnLogout(OnlogoutRequest userLogedout)
        {
            throw new NotImplementedException();
        }

        public void OnSignedUp(OnSignedUpRequest userSignedUp)
        {
            throw new NotImplementedException();
        }

        public SignUpResponse SignUp(SignUpRequest signUpRequest)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect(IUserCallback unexpectedDisconnectedUser)
        {
            throw new NotImplementedException();
        }
    }
}
