using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class BlackPlayer :IPlayer
    {
        public string UserName { get; set; }
        public PlayerState PlayerState { get; set; }
        public BlackPlayer()
        {
            PlayerState = PlayerState.Normal;
        }
    }
}
