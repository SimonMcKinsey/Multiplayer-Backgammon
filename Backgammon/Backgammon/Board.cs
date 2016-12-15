using Backgammon.PieceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Board
    {
        public Slot[] Slots { get; set; }
        public Slot OutPiecesSlot { get; set; } //slot id - int.minValue
        //public List<IPiece> FinishedBlack { get; set; }
        //public List<IPiece> FinishedWhite { get; set; }
        //public List<IPiece> OutBlack { get; set; }
        //public List<IPiece> OutWhite { get; set; }
        public Board()
        {
            InitSlots();
            //FinishedBlack = new List<IPiece>();
            //FinishedWhite = new List<IPiece>();
            //OutBlack = new List<IPiece>();
            //OutWhite = new List<IPiece>();
        }

        private void InitSlots()
        {
            Slots = new Slot[26];
            for (int i = RulesContainer.FinishedSlotBlack; i < Slots.Length ; i++)
            {
                Slots[i] = SlotsFactory.Instance.InitiateSlot(i);
            }
            //init outPieces slot:
            OutPiecesSlot = SlotsFactory.Instance.InitiateSlot(int.MinValue);
        }
    }
}
