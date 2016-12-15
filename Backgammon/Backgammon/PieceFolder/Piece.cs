using Backgammon.enumsFolder;
using System;

namespace Backgammon.PieceFolder
{
    internal abstract class Piece : IPiece
    {
        public Piece()
        {
            PieceState = PieceState.In;     
        }
        

        public PieceState PieceState { get; set; }
    }
}