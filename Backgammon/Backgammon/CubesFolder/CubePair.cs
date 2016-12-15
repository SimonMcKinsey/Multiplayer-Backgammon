namespace Backgammon.CubesFolder
{
    public class CubePair
    {
        public Cube FirstCube { get; set; }
        public Cube SecondCUbe { get; set; }
        public CubePair(int firstCubeValue, int secondCubeValue)
        {
            this.FirstCube = new Cube { Value = firstCubeValue };
            this.SecondCUbe = new Cube { Value = secondCubeValue };
        }
        public CubePair()
        {
                
        }
    }
}