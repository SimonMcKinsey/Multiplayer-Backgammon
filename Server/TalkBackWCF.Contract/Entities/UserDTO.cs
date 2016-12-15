using System;
using System.Runtime.Serialization;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [Serializable]
    [DataContract]
    public class UserDTO : ISignedUp
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}