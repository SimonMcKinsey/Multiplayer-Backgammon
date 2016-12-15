using Backgammon.CubesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class CubesToStepsConverter
    {
        public static List<Step> Convert (CubePair cubes)
        {
            List<Step> Steps = new List<Step>();
            if (cubes.FirstCube.Value == cubes.SecondCUbe.Value)
            {
                for (int i = 0; i < 4; i++)
                {
                    Steps.Add(new Step { Value = cubes.FirstCube.Value });
                }
                return Steps;
            }
            Steps.Add(new Step { Value = cubes.FirstCube.Value });
            Steps.Add(new Step { Value = cubes.SecondCUbe.Value });

            return Steps;
        } 
    }
}
