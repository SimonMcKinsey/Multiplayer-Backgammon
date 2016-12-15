using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.Contract.Entities
{
    [DataContract]
    public class SignUpRequest : ISignedUp
    {
        [DataMember][Required,StringLength(30,MinimumLength =2)]
        public string UserName { get; set; }
        [DataMember][Required,StringLength(30,MinimumLength =3)]
        public string Password { get; set; }
        [DataMember][Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
