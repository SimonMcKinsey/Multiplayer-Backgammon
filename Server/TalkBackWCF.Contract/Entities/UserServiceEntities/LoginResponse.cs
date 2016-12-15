using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class LoginResponse : BaseResponse
    {
        [DataMember]
        public List<UserDTO> AllOtherUsers { get; set; } //except current user that sent the login request
        [DataMember]
        public Guid Authentication { get; set; }
        //public Guid Guid { get; set; } Security Authentication?
    }
}