using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [DataContract]
    public class LoginRequest : ISignedUp
    {
        [DataMember][Required]
        public string UserName { get; set; }
        [DataMember][Required]
        public string Password { get; set; }
        //[DataMember]
        //public ISignedUp User { get; set; } 
    }
}