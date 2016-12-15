using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.Contract.Entities
{

    public interface ISignedUp : IUserName
    {
        string Password { get; set; }
    }
}
