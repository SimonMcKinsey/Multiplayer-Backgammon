using Backgammon.enumsFolder;
using Backgammon.StepsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameUpdatesFolder
{
    public class StepsLeftUpdater
    {
        public static void Update(int slotIdSource,int slotIdDestination,List<Step> steps,Turn turn)
        {
            Step stepPlayed = StepsConverter.ConvertToStep(slotIdSource, slotIdDestination, turn);
            Step stepMatchToCube = steps.Where(s => s.Value == stepPlayed.Value).FirstOrDefault();
            if(stepMatchToCube == null)
            {
                throw new Exception("somthing wrong with the stepsLeftUpdater");
            }
            steps.Remove(stepMatchToCube);
            
        }
    }
}
