using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientDemo.InfraStructure
{
    public class OnMessageSentEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        //TODO DateTime of message
    }
}
