using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Domain;
using TalkBackWCF.Domain.Repositories;

namespace TalkBackWCF.BL
{
    public class UserServiceLogic //TODO: do as singleton
    {
       // private UserRepository rep;
        public UserServiceLogic()
        {
            //rep = new UserRepository();

        }



        public bool TryLogin(ISignedUp user,out string message)
        {
            UsersStateInfo userStateInfo = UsersStateInfo.Instance;
            bool isSignedUp = userStateInfo.IsUserSignedUp(new UserDTO() { UserName = user.UserName, Password = user.Password });
            if (isSignedUp == false) //if loggin inputs are incorrect
            {
                message = "User name Or Password are incorrect";
                return false;
            }
            bool isAlreadyLogedin = UsersStateManager.CheckIfUserIsOnline(user.UserName);
            if(isAlreadyLogedin == true)
            {
                message = "user is already loged in";
                return false;
            }
            message = "user loggedin successfully";
            return true;
        }
        public List<UserDTO> GetOnlineUsersExcept(string userName)
        {
          List<UserDTO> onlineUsers =  UsersStateInfo.Instance.OnlineUsers.Select(u=>u.Value).ToList() //TODO : better query?
                                                                .Where(u => u.UserName != userName).ToList();
            return onlineUsers;
            
        }
        public List<UserDTO> GetSignedUpUsersExcept(string userName)
        {
            List<UserDTO> SignedUpUsers = UsersStateInfo.Instance.AllSignedUpUsers.Where(u => u.UserName != userName).ToList();
            return SignedUpUsers;
        }
        private UserDTO TryGetUserByAuthentication(Guid authentication)
        {
            UserDTO user;
            UsersStateInfo.Instance.OnlineUsers.TryGetValue(authentication, out user);
            return user;
        }
        public Guid CreateAuthentication(string userName)
        {
            Guid authentication = Guid.NewGuid();
            UserDTO logedUser = UsersStateInfo.Instance.AllSignedUpUsers.Where(u => u.UserName == userName).First();
            //updating the onlineUsers Dictionary
            UsersStateInfo.Instance.OnlineUsers.Add(authentication, logedUser);
            return authentication;
        }
        public Guid GetHeaderAuthentication()
        {
            string authenticationString = RequestHeaderProvider.GetHeaderByName("Authentication");
            Guid authentication;
            Guid.TryParse(authenticationString, out authentication);
            return authentication;
        }
        public Guid GetAuthenticationByUserName(string userName)
        {
           return UsersStateManager.TryGetAuthenticationByUserName(userName);
        }
        public UserDTO TryLogout(LogoutRequest logoutRequest, Guid authentication) //checking Authentication, if it fit update onlineUsers Dictiontary and return true
        {
            UserDTO onlineUser = TryGetUserByAuthentication(authentication);
            if(onlineUser == null)
            {
                return null;
            }
            else
            {
                UsersStateInfo.Instance.OnlineUsers.Remove(authentication);
                return onlineUser;
            }
        }
        public UserDTO TryLogout(string userName)
        {
            Guid authentication = GetAuthenticationByUserName(userName);
          return TryLogout(new LogoutRequest { UserName = userName }, authentication);
        }
        public bool TrySignedUp(ISignedUp user, out string message)
        {
            UserDTO isUserNameUsed = UsersStateInfo.Instance.AllSignedUpUsers.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if(isUserNameUsed != null)
            {
                message = "user name already used";
                return false;
            }
            UsersStateInfo.Instance.SignUpUser(user);
            message = $"signed up succesfully : {user.UserName} ";
            return true;
        }
    }
}
