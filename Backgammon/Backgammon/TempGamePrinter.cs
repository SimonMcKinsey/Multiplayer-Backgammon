using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
     public class TempGamePrinter
    {
        public static string ChooseSourceMessages(bool SlotIdValid, bool IsSlotContainPiecesOfPlayer)
        {
            if( SlotIdValid == false)
            {
                return "Slot Id choosen is out of boundry";
            }
            if(IsSlotContainPiecesOfPlayer == false)
            {
                return "the player doesnt have pieces to move in this slot";
            }
            return "source slot is ligeal, now choose destination";
        }
        public static string ChooseDestinationMessages(bool isPlayerGoingForward , bool IsDestinationInsideBorders,bool isSlotBlocked, bool isCubeStepExist)
        {
            if (isPlayerGoingForward == false)
            {
                return "player is trying to go backwards";
            }
            if (IsDestinationInsideBorders == false)
            {
                return "player trying to go beyond bounderies";
            }
            if(isSlotBlocked == true)
            {
                return "this slot is blocked";
            }
            if(isCubeStepExist == false)
            {
                return "player dont have that step available";
            }
            return "GOOD. player bounderies is fine" + "GOOD. player going forward";

        }
       public static string CubesValuePrint(List<Step> steps)
        {
            string message = "Cubes are Rolled your steps are : ";
            foreach (var step in steps)
            {
                message += step.Value + " , ";
            }
            return message;
        }
    }
}
