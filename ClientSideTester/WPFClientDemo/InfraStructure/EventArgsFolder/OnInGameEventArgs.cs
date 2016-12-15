using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientDemo.InfraStructure.EventArgsFolder
{
    public class OnInGameEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public int SlotIdSource { get; set; }
        public int SlotIdDestination { get; set; }
    }
}
