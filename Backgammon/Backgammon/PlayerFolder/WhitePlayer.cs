using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class WhitePlayer : IPlayer
    {
        public string UserName { get; set; }
        public PlayerState PlayerState { get; set; }
        public WhitePlayer()
        {
            PlayerState = PlayerState.Normal;
        }
    }
}
