using Backgammon.enumsFolder;
using Backgammon.StepsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameValidations.DestinationValidations
{
    public class IsPlayerGoingForward
    {
        public static bool Validate (int slotIdSource,int slotIdDestination, Turn turn)
        {
            //if(turn == Turn.Black)
            //{
            //    if(slotIdSource-slotIdDestination <= 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            //else
            //{
            //    if (slotIdSource - slotIdDestination >= 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}

            Step playerStep = StepsConverter.ConvertToStep(slotIdSource, slotIdDestination, turn);
            if(playerStep.Value >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
