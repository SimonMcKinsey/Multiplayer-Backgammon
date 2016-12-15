using Backgammon.PieceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class SlotsFactory
    {
        private const int BackgammonSlotsNumber = 26;
        #region Singelton implementation
        private static readonly object padlock = new object();
        private static SlotsFactory instance = null;
        public static SlotsFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SlotsFactory();
                    }
                    return instance;
                }
            }

        }
        #endregion
        private Dictionary<int, Slot> SlotPerId;
        private SlotsFactory()
        {
            SlotPerId = new Dictionary<int, Slot>();

                SlotsGenerator.InitRows();
            for (int i = RulesContainer.FinishedSlotBlack; i < BackgammonSlotsNumber ; i++)
            {
                SlotPerId.Add( i,SlotsGenerator.GenerateSlot(i));
            }
            //init OutPieceSlot:
            SlotPerId.Add(int.MinValue, SlotsGenerator.GenerateSlot(int.MinValue));
            #region another way Test
            ////slot 0:
            //List<IPiece> WhitePieces = PiecesGenerator.GenerateWhite(2);
            //SlotPerId.Add(0, new Slot { Id = 0, BlackPieces = new List<IPiece>(), WhitePieces = WhitePieces });
            ////slot 1,2,3,4:
            //SlotPerId.Add(1, new Slot { Id = 1, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            //SlotPerId.Add(2, new Slot { Id = 2, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            //SlotPerId.Add(3, new Slot { Id = 3, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            //SlotPerId.Add(4, new Slot { Id = 4, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            ////slot 5:
            //List<IPiece> BlackPieces = PiecesGenerator.GenerateBlack(5);
            //SlotPerId.Add(5, new Slot { Id = 5, BlackPieces = BlackPieces, WhitePieces = new List<IPiece>() });
            ////slot 6:
            //SlotPerId.Add(6, new Slot { Id = 6, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            ////slot 7:
            //BlackPieces = PiecesGenerator.GenerateBlack(3);
            //SlotPerId.Add(7, new Slot { Id = 7, BlackPieces = BlackPieces, WhitePieces = new List<IPiece>() });
            ////slot 8,9,10 :
            //SlotPerId.Add(8, new Slot { Id = 8, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            //SlotPerId.Add(9, new Slot { Id = 9, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            //SlotPerId.Add(10, new Slot { Id = 10, BlackPieces = new List<IPiece>(), WhitePieces = new List<IPiece>() });
            ////slot 11:
            //WhitePieces = PiecesGenerator.GenerateWhite(5);
            //SlotPerId.Add(11, new Slot { Id = 11, BlackPieces = new List<IPiece>(), WhitePieces = WhitePieces });
            #endregion
        }
        public Slot InitiateSlot(int id)
        {
            Slot slot = SlotPerId[id];
            return slot;
        }
    }
}
