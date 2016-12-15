using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameValidations.SourceValidations
{
    public class IsSlotIdSourceValid
    {
        public static bool Validate(int slotIdSource, IPlayer blackPlayer, IPlayer whitePlayer, Turn turn)
        {
            //checking witch player is playing:
            IPlayer player;
            if (turn == Turn.Black)
            {
                player = blackPlayer;
            }
            else // turn white
            {
                player = whitePlayer;
            }
            //-----check validation:

            if ((slotIdSource < RulesContainer.FinishedSlotWhite && slotIdSource > RulesContainer.FinishedSlotBlack) || slotIdSource == RulesContainer.OutSlotIndex) // int.MinValue = choosing an out piece
            {
                if (player.PlayerState == PlayerState.Normal || player.PlayerState == PlayerState.TakingOut)
                {
                    return true;
                }
                else // if player have pieces outside he must pick them into the board first
                {
                    if (slotIdSource == RulesContainer.OutSlotIndex)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }


}
