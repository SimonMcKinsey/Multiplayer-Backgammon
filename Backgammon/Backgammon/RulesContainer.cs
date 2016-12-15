using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public  class RulesContainer
    {
        public static readonly int FirstSlotIndex = 1;
        public static readonly int LastSlotIndex = 24;
        public static readonly int FinishedSlotBlack = 0;
        public static readonly int FinishedSlotWhite = 25;
        public static readonly int OutSlotIndex = int.MinValue;
        public static readonly int MaxBlackBaseSlot = 6;
        public static readonly int MaxWhiteBaseSlot = 19;


    }
}
