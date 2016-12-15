using System.Runtime.Serialization;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class OnSignedUpRequest : IUserName
    {
        [DataMember]
        public string UserName { get; set; }

    }
}