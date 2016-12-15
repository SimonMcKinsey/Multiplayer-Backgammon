using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameValidations
{

    public class IsSlotIdDestinationValid
    {
        public static bool Validate(int slotIdDestination, IPlayer blackPlayer, IPlayer whitePlayer, Turn turn)
        {
            if (turn == Turn.Black)
            {
                return IsSlotIdDestinationValidForBlack.Validate(slotIdDestination, blackPlayer);
            }
            else
            {
                return IsSlotIdDestinationValidForWhite.Validate(slotIdDestination, whitePlayer);
            }

        }
    }
    public class IsSlotIdDestinationValidForBlack
    {
        public static bool Validate(int slotIdDestination, IPlayer player)
        {
            switch (player.PlayerState)
            {
                case PlayerState.Normal:
                    if (RulesContainer.FinishedSlotWhite > slotIdDestination && slotIdDestination > RulesContainer.FinishedSlotBlack)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.TakingOut:
                    if (RulesContainer.MaxBlackBaseSlot >= slotIdDestination && slotIdDestination >= RulesContainer.FinishedSlotBlack)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.TakingIn:
                    if (RulesContainer.MaxWhiteBaseSlot <= slotIdDestination && slotIdDestination < RulesContainer.FinishedSlotWhite)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.Blocked:
                    return false;
                default:
                    throw new Exception("somthing is wrong in slotidDestionationForBlack");

            }


        }
    }
    public class IsSlotIdDestinationValidForWhite
    {
        public static bool Validate(int slotIdDestination, IPlayer player)
        {
            switch (player.PlayerState)
            {
                case PlayerState.Normal:
                    if (RulesContainer.FinishedSlotBlack < slotIdDestination && slotIdDestination < RulesContainer.FinishedSlotWhite)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.TakingOut:
                    if (RulesContainer.MaxWhiteBaseSlot < slotIdDestination && slotIdDestination <= RulesContainer.FinishedSlotWhite)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.TakingIn:
                    if (RulesContainer.FinishedSlotBlack < slotIdDestination && slotIdDestination <= RulesContainer.MaxBlackBaseSlot)
                    {
                        return true;
                    }
                    return false;
                case PlayerState.Blocked:
                    return false;
                default:
                    throw new Exception("somthing is wrong in slotidDestionationForWhite");
            }
        }
    }
}
