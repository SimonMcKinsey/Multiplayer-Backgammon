using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;

namespace ClientSideTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var userServiceProxy = new UserServiceProxy();

            var response = userServiceProxy.Login(new LoginRequest { UserName = "hen", Password = "123" });
            Console.WriteLine(response.IsSuccess + response.Message);
            foreach (var user in response.AllOtherUsers)
            {
                Console.WriteLine(user.UserName);
            }
            Console.ReadLine();
        }
        class UserServiceProxy : IUserService, IUserCallback //TODO: Singleton?
        {
            private IUserService proxy; //channel to the service
            public UserServiceProxy()
            {
                var factory = new DuplexChannelFactory<IUserService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888")); //creating the ChannelFactory
                factory.Open();
                proxy = factory.CreateChannel(); //creating channel to the service
            }

            public void Disconnect()
            {
                throw new NotImplementedException();
            }

            public void Disconnect(IUserCallback unexpectedDisconnectedUser)
            {
                throw new NotImplementedException();
            }

            public LoginResponse Login(LoginRequest loginRequest)
            {
                LoginResponse response = proxy.Login(loginRequest);
                return response;

            }

            public LogoutResponse Logout(LogoutRequest logoutRequest)
            {
                throw new NotImplementedException();
            }

            public void OnLogin(OnLoginRequest userLogedin)
            {
              
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
        }
    }
}
