using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace TalkBackWCF.BL
{
    public class CallbacksHandler
    {

        public CallbacksHandler()
        {
                
        }
        public string ReturnUserNameByCallback(IUserCallback callback,Dictionary<IUserCallback, string> callbacksByUserName)
        {
           return callbacksByUserName.ContainsKey(callback) ? callbacksByUserName[callback] : string.Empty;
        }
        public void HandleUnexceptedDisconnection(IUserCallback unexpectedDisconnectedUser, Dictionary<IUserCallback, string> callbacksByUserName)
        {
            //removing from onllineUsers :
            string userName = ReturnUserNameByCallback(unexpectedDisconnectedUser,callbacksByUserName);
            //remove from callbacks:
            callbacksByUserName.Remove(unexpectedDisconnectedUser);

        }
        public void AddCallbackToCallbacksDictionary(IUserCallback callback,IUserName request, Dictionary<IUserCallback, string> callbacksByUserName)
        {

            if (!callbacksByUserName.ContainsKey(callback))
            {//from now on, this user will know each time other user is loged in
                callbacksByUserName.Add(callback, request.UserName);
            }
        }
    }
}
