using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.Contract.Entities.ChatServiceEntities
{
    [DataContract]
    public class SendMessageRequest: IUserName
    {
        [DataMember][Required(ErrorMessage = "you cant send an empty Message")]
        public string Message { get; set; }
        [DataMember]
        public string UserName { get; set; }

    }
}
