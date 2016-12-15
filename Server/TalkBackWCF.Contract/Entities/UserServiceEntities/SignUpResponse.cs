using System.Collections.Generic;
using System.Runtime.Serialization;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class SignUpResponse : BaseResponse
    {
        [DataMember]
       public List<UserDTO> AllOtherSignedUpUsers;
    }
}