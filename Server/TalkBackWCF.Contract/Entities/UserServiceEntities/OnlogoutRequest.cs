using System;
using System.Runtime.Serialization;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class OnlogoutRequest : IUserName
    {
        [DataMember]
        public string UserName { get; set; }
    }
}