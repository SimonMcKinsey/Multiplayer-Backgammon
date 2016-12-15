using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.CubesFolder
{
     class CubesGenerator
    {
        public static CubePair Generate()
        {
            Random rnd = new Random();
            int firstCubeValue = rnd.Next(1, 7);
            int secondCubeValue = rnd.Next(1, 7);

            return new CubePair(firstCubeValue, secondCubeValue);
        }
    }
}
