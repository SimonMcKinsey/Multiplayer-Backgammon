using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.PieceFolder
{
    class PiecesGenerator
    {
        public static List<IPiece> GenerateBlack(int number) 
        {
            List<IPiece> Pieces = new List<IPiece>();
            for (int i = 0; i < number; i++)
            {
                
                Pieces.Add(new BlackPiece());
            }
            return Pieces;
        }
        public static List<IPiece> GenerateWhite(int number)
        {
            List<IPiece> Pieces = new List<IPiece>();
            for (int i = 0; i < number; i++)
            {

                Pieces.Add(new WhitePiece());
            }
            return Pieces;
        }
    }
}
