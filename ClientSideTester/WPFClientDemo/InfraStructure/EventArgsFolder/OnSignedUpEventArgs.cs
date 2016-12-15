using System;
using TalkBackWCF.Contract;

namespace TalkBack.ClientDemo
{
    public class OnSignedUpEventArgs : EventArgs,IUserName
    {
        public string UserName { get; set; }
    }
}