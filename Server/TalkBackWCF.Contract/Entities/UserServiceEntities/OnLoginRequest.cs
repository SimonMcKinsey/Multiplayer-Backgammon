using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class OnLoginRequest : IUserName
    {
        [DataMember]
        public string UserName { get; set; }

    }
}