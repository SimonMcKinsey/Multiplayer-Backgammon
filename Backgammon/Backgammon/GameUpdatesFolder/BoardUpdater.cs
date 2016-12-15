using Backgammon.enumsFolder;
using Backgammon.PieceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameUpdatesFolder
{
    public class BoardUpdater
    {
        public static void Update(int slotIdSource, int slotIdDestination, Board board, Turn turn)
        {
            Slot slotSource;
            Slot slotDestination;
            //is Source slot is an outPieces slot?:
            if (slotIdSource == int.MinValue)
            {
                slotSource = board.OutPiecesSlot;
            }
            else
            {
                slotSource = board.Slots[slotIdSource];
            }
            slotDestination = board.Slots[slotIdDestination];
            if (turn == Turn.Black)
            {
                BoardBlackTurnUpdater.Update(slotSource, slotDestination, board);
            }
            else// white turn
            {
                BoardWhiteTurnUpdater.Update(slotSource, slotDestination, board);
            }
        }
    }
    public class BoardWhiteTurnUpdater
    {
        public static void Update(Slot slotSource, Slot slotDestination, Board board)
        {
            //is Source an outPiece:

            List<IPiece> whitePiecesSource = slotSource.WhitePieces;
            List<IPiece> whitePiecesDestination = slotDestination.WhitePieces;
            List<IPiece> blackPiecesDestination = slotDestination.BlackPieces;
            //removing one piece from source:
            whitePiecesSource.RemoveAt(whitePiecesSource.Count - 1);
            //adding one piece to destination:
            whitePiecesDestination.Add(new WhitePiece { });
            //is destination include opponent piece:
            if(blackPiecesDestination.Count == 1)
            {
                blackPiecesDestination.RemoveAt(blackPiecesDestination.Count - 1);
                board.OutPiecesSlot.BlackPieces.Add(new BlackPiece { });
            }
        }
    }
    public class BoardBlackTurnUpdater
    {
        public static void Update(Slot slotSource, Slot slotDestination, Board board)
        {
            List<IPiece> blackPiecesSource = slotSource.BlackPieces;
            List<IPiece> blackPiecesDestination = slotDestination.BlackPieces;
            List<IPiece> whitePiecesDestination = slotDestination.WhitePieces;
            //removing one piece from source:
            blackPiecesSource.RemoveAt(blackPiecesSource.Count - 1);
            //adding one piece to destination:
            blackPiecesDestination.Add(new BlackPiece { });
            //is destination include opponent piece:
            if (whitePiecesDestination.Count == 1)
            {
                whitePiecesDestination.RemoveAt(whitePiecesDestination.Count - 1);
                board.OutPiecesSlot.WhitePieces.Add(new WhitePiece { });
            }
        }
    }
}
