using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.StepsFolder
{
    class StepsConverter
    {
        public static Step ConvertToStep(int slotIdSource,int slotIdDestination , Turn turn)
        {
            if(turn == Turn.White)
            {
                if(slotIdSource == int.MinValue)
                {
                    slotIdSource = RulesContainer.FinishedSlotBlack;
                }
                return new Step { Value = slotIdDestination - slotIdSource };
            }
            else // black turn
            {
                if (slotIdSource == int.MinValue)
                {
                    slotIdSource = RulesContainer.FinishedSlotWhite;
                }
                return new Step { Value = slotIdSource - slotIdDestination };
            }
        }
    }
}
