using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace WPFClientDemo.InfraStructure
{
    public class OnLoginEventArgs : EventArgs, IUserName
    {
        public string UserName { get; set; }
    }
}
