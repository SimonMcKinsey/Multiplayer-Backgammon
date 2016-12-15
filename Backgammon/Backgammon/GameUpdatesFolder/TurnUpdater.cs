using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameUpdatesFolder
{
    public class TurnUpdater // this updater will be last
    {
        public static Turn Update(Turn turn, IPlayer blackPlayer, IPlayer whitePlayer, List<Step> steps)
        {
            if (steps.Count == 0)
            {

                if (turn == Turn.Black)
                {
                    if (whitePlayer.PlayerState == PlayerState.Blocked)
                    {
                        turn = Turn.Black;
                    }
                    turn = Turn.White;

                }
                else// white turn
                {
                    if (blackPlayer.PlayerState == PlayerState.Blocked)
                    {
                        turn = Turn.White;
                    }
                    turn = Turn.Black;
                }
            }
            return turn;
        }
    }

}
