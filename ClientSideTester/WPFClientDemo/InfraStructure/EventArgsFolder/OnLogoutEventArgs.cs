using System;
using TalkBackWCF.Contract;

namespace TalkBack.ClientDemo
{
    public class OnLogoutEventArgs : EventArgs, IUserName
    {
        public string UserName { get; set; }
    }
}