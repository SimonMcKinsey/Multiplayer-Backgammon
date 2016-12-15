using System;
using System.Runtime.Serialization;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class LogoutRequest : IUserName
    {
        //[DataMember]
        //public UserDTO User { get; set; }
        [DataMember]
        public string UserName { get; set; }
        //[DataMember]
        //public Guid Authentication { get; set; }
    }


}