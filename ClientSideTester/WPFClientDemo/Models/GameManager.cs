using Backgammon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientDemo.InfraStructure.EventArgsFolder;

namespace WPFClientDemo.Models
{
    public class GameManager
    {
        public Game Game { get; set; }
        public SlotClickPurpose SrcOrDst { get; set; }
        private string message;
        public event EventHandler<OnInGameEventArgs> GameChanged;
        public int LastSlotIdSource { get; set; }
        public int LastSlotIdDestination { get; set; }

        public GameManager(string blackPlayerName, string whitePlayerName)
        {
            Game = new Game(blackPlayerName, whitePlayerName);
            SrcOrDst = SlotClickPurpose.Source;
            message = string.Empty;
            
        }

        public string SlotClicked(Slot slot)
        {
            bool hasMoveSucceded = false;
            if (SrcOrDst == SlotClickPurpose.Source)
            {
                try
                {

                    hasMoveSucceded = Game.ChooseSource(slot.Id, out message);
                    if(hasMoveSucceded == true)
                    {
                        LastSlotIdSource = slot.Id;
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            else //SlotIdDestination
            {
                try
                {
                    hasMoveSucceded = Game.ChooseDestination(slot.Id, out message);
                    if (hasMoveSucceded == true)
                    {
                        LastSlotIdDestination = slot.Id;
                        GameChanged?.Invoke(Game,new OnInGameEventArgs { SlotIdSource = LastSlotIdSource, SlotIdDestination = LastSlotIdDestination });
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            updateSlotClickPurpose(hasMoveSucceded);
            return message;
        }
        public string Roll()
        {
            return Game.Roll();
        }
        private void updateSlotClickPurpose(bool hasMoveSucceded)
        {

            if (SrcOrDst == SlotClickPurpose.Source)
            {
                if (hasMoveSucceded)
                {
                    SrcOrDst = SlotClickPurpose.Destination;
                }

            }
            else
            {
                SrcOrDst = SlotClickPurpose.Source;
            }
        }
    }
    public enum SlotClickPurpose
    {
        Source,
        Destination
    }
}
