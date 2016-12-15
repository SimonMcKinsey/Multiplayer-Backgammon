using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.enumsFolder
{
    public enum PlayerState
    {
        Normal,
        TakingOut,
        TakingIn,
        Blocked
    
    }
    public enum PieceState
    {
        In,
        Out,
        Finished
    }
    public enum Turn
    {
        Black,
        White
    }
}
