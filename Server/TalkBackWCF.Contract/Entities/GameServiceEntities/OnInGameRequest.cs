using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.Contract.Entities.GameServiceEntities
{
    [DataContract]
    public class OnInGameRequest : IUserName
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int SlotIdSource { get; set; }
        [DataMember]
        public int SlotIdDestination { get; set; }
    }
}
