using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.Contract.Entities.ChatServiceEntities
{
    [DataContract]
    public class OnMessageSentRequest: IUserName
    {
        [DataMember]
        public string MessageSent { get; set; }
        [DataMember]
        public string UserName { get; set; }

    }
}
