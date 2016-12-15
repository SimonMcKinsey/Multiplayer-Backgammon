using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class ChoosenMoveDetails
    {
        #region Singelton implementation
        private static readonly object padlock = new object();
        private static ChoosenMoveDetails instance = null;
        public static ChoosenMoveDetails Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ChoosenMoveDetails();
                    }
                    return instance;
                }
            }

        }
        #endregion
        public int? SlotIdSource { get; set; }
        public int? SlotIdDestination { get; set; }
        private ChoosenMoveDetails()
        {
            SlotIdSource = null;
            SlotIdDestination = null;
        }

        public void RefreshForNewMove()
        {
            SlotIdSource = null;
            SlotIdDestination = null;
        }

    }
}
