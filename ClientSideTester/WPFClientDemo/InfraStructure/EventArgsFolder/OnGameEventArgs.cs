using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace WPFClientDemo.InfraStructure
{
    public class OnGameEventArgs : CancelEventArgs, IUserName
    {
        public string UserName { get; set; }
    }
}
