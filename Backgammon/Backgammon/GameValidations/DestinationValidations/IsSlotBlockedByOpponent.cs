using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameValidations.DestinationValidations
{
    public class IsSlotBlockedByOpponent
    {
        public static bool Validate(int slotIdDestination ,Turn turn,Board board)
        {
            Slot destinationSlot = board.Slots[slotIdDestination];
            if(turn == Turn.Black)
            {
                if(destinationSlot.WhitePieces.Count > 1)
                {
                    return true;
                }
                return false;
            }
            else // white turn
            {
                if(destinationSlot.BlackPieces.Count > 1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
