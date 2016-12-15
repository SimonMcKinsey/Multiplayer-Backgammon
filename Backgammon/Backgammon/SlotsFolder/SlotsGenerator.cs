using Backgammon.PieceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class SlotsGenerator
    {
        private static Dictionary<int, int> slotHolder;
        internal static void InitRows()
        {
            //White row
            slotHolder = new Dictionary<int, int>();
            slotHolder.Add(1, 2);
            slotHolder.Add(6, 5);
            slotHolder.Add(8, 3);
            slotHolder.Add(12, 5);

            // Black row
            slotHolder.Add(13, 5);
            slotHolder.Add(17, 3);
            slotHolder.Add(19, 5);
            slotHolder.Add(24, 2);
        }
        internal static Slot GenerateSlot(int id)
        {
            // 2, 5, 3, 5
            if (!slotHolder.ContainsKey(id))
            {
                return new Slot { Id = id, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() };
            }
            switch (id)
            {
                case 1:
                    return new Slot { Id = id, BlackPieces = new List<IPiece>(), WhitePieces = PiecesGenerator.GenerateWhite(slotHolder[id]) };
                case 6:
                    return new Slot { Id = id, BlackPieces = PiecesGenerator.GenerateBlack(slotHolder[id]), WhitePieces = new List<IPiece>() };
                case 8:
                    return new Slot { Id = id, BlackPieces = PiecesGenerator.GenerateBlack(slotHolder[id]), WhitePieces = new List<IPiece>() };
                case 12:
                    return new Slot { Id = id, BlackPieces = new List<IPiece>(), WhitePieces = PiecesGenerator.GenerateWhite(slotHolder[id]) };

                // TODO: order this
                case 13:
                    return new Slot { Id = id, BlackPieces = PiecesGenerator.GenerateBlack(slotHolder[id]), WhitePieces = new List<IPiece>() };
                case 17:
                    return new Slot { Id = id, BlackPieces = new List<IPiece>(), WhitePieces = PiecesGenerator.GenerateWhite(slotHolder[id]) };
                case 19:
                    return new Slot { Id = id, BlackPieces = new List<IPiece>(), WhitePieces = PiecesGenerator.GenerateWhite(slotHolder[id]) };
                case 24:
                    return new Slot { Id = id, BlackPieces = PiecesGenerator.GenerateBlack(slotHolder[id]), WhitePieces = new List<IPiece>() };
                default:
                    return null;
            }
        }
    }
}
