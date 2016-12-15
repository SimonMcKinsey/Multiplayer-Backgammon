using Backgammon.PieceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Slot
    {
        public int Id { get; set; }
        public List<IPiece> WhitePieces { get; set; }
        public List<IPiece> BlackPieces { get; set; }
        public Slot()
        {
            WhitePieces = new List<IPiece>();
            BlackPieces = new List<IPiece>();
        }
    }
}
