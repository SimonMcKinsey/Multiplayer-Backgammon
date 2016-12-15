using DevTrends.WCFDataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.BL;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Contract.Services;

namespace TalkBackWCF.Services
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ValidateDataAnnotationsBehavior]
    public class UserService : IUserService
    {
        private UserServiceLogic serviceLogic;
        private Dictionary<IUserCallback,string> callbacksByUserName;
        private CallbacksHandler callbacksHandler;

        public UserService()
        {
            serviceLogic = new UserServiceLogic();
            callbacksByUserName = new Dictionary<IUserCallback,string>();
            callbacksHandler = new CallbacksHandler();
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
            //service logic should return OnlineUser Guid For User and update dicitonary (in case login=Succes)
            string responseMessage = string.Empty;
            bool loginSuccess = serviceLogic.TryLogin(loginRequest, out responseMessage);
            if (loginSuccess == true)
            {
                Guid authentication = serviceLogic.CreateAuthentication(loginRequest.UserName);
                for (int i = 0; i < callbacksByUserName.Count; i++)
                {
                    var user = callbacksByUserName.ElementAt(i).Key;
                    try
                    {
                        user.OnLogin(new OnLoginRequest { UserName = loginRequest.UserName });
                    }
                    catch
                    {
                        string disconnectedUserName = callbacksHandler.ReturnUserNameByCallback(user,callbacksByUserName);
                        serviceLogic.TryLogout(disconnectedUserName);
                        callbacksHandler.HandleUnexceptedDisconnection(user,callbacksByUserName);
                        i--;
                    }
                }
                List<UserDTO> allOtherUsers = serviceLogic.GetOnlineUsersExcept(loginRequest.UserName); //getting all users already logged in, in order to tell the user about them            



                //making a callback to tell the other users the user is loged in (if succeded)
                IUserCallback callback = OperationContext.Current.GetCallbackChannel<IUserCallback>();
                callbacksHandler.AddCallbackToCallbacksDictionary(callback, loginRequest, callbacksByUserName);
                return new LoginResponse { IsSuccess = true, Message = responseMessage, AllOtherUsers = allOtherUsers, Authentication = authentication };
            }
            else //login didnt succes:
            {
                return new LoginResponse { IsSuccess = false, Message = responseMessage, AllOtherUsers = new List<UserDTO> { }, Authentication = Guid.Empty };
            }
        }



        public LogoutResponse Logout(LogoutRequest logoutRequest)
        {
            Guid authentication = serviceLogic.GetHeaderAuthentication();
            UserDTO LogedoutUser = serviceLogic.TryLogout(logoutRequest, authentication);
            if (LogedoutUser == null)
            {
                return new LogoutResponse { IsSuccess = false, Message = "request not authentiacated" };
            }
            else
            {
                //removing user from callbacks:
                IUserCallback callback = OperationContext.Current.GetCallbackChannel<IUserCallback>();
                callbacksByUserName.Remove(callback);
                for (int i = 0; i < callbacksByUserName.Count; i++)
                {
                    var user = callbacksByUserName.ElementAt(i).Key;
                    try
                    {
                        user.OnLogout(new OnlogoutRequest { UserName = logoutRequest.UserName });
                    }
                    catch
                    {
                        string disconnectedUserName = callbacksHandler.ReturnUserNameByCallback(user, callbacksByUserName);
                        serviceLogic.TryLogout(disconnectedUserName);
                        callbacksHandler.HandleUnexceptedDisconnection(user, callbacksByUserName);
                        i--;
                    }
                }
                return new LogoutResponse { IsSuccess = true, Message = "loged out succefully" };

            }
        }

        public SignUpResponse SignUp(SignUpRequest signUpRequest)
        {
            string messageResponse = string.Empty;
            bool isSigendUp = serviceLogic.TrySignedUp(signUpRequest, out messageResponse);
            if (isSigendUp == false)
            {
                return new SignUpResponse { IsSuccess = false, Message = messageResponse };
            }
            //telling the other logedin users about this user that signed up
            for (int i = 0; i < callbacksByUserName.Count; i++)
            {
                var user = callbacksByUserName.ElementAt(i).Key;
                try
                {
                    user.OnSignedUp(new OnSignedUpRequest { UserName = signUpRequest.UserName });
                }
                catch
                {
                    string disconnectedUser = callbacksHandler.ReturnUserNameByCallback(user, callbacksByUserName);
                    serviceLogic.TryLogout(disconnectedUser);
                    callbacksHandler.HandleUnexceptedDisconnection(user, callbacksByUserName);
                }
            }
            List<UserDTO> allOthersSignedUpUsers = serviceLogic.GetSignedUpUsersExcept(signUpRequest.UserName);
            return new SignUpResponse { IsSuccess = true, Message = messageResponse, AllOtherSignedUpUsers = allOthersSignedUpUsers };

        }


    }


}
