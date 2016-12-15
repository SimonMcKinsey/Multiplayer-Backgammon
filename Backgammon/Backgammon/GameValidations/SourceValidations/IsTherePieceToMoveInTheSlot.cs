using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameValidations
{
    public class IsTherePieceToMoveInTheSlot
    {
        public static bool Validate(int slotIdSource, Turn turn, Board board)
        {
            //take slot from board by Id
            Slot slot;
            if (slotIdSource != RulesContainer.OutSlotIndex)
            {
                slot =   board.Slots[slotIdSource];
            }
            else
            {
                slot = board.OutPiecesSlot;
            }
            //validation:
            if (turn == Turn.Black)
            {
                //if there is at least one black piece
                if (slot.WhitePieces.Count == 0 && slot.BlackPieces.Count > 0)
                {
                    return true;
                }
                return false;
            }
            else //white turn
            {
                //if there is at least one white piece
                if (slot.BlackPieces.Count == 0 && slot.WhitePieces.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
