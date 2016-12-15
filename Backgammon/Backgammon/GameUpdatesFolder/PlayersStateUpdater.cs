using Backgammon.enumsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.GameUpdatesFolder
{
    public class PlayersStateUpdater
    {
        public static void Update(IPlayer blackPlayer,IPlayer whitePlayer, Board board)
        {
            UpdateBlackPlayer(blackPlayer,board);
            UpdateWhitePlayer(whitePlayer, board);
        }

        private static void UpdateWhitePlayer(IPlayer whitePlayer, Board board)
        {
           //if white player have pieces outside:
           if(board.OutPiecesSlot.WhitePieces.Count != 0)
            {
                //it can only be in state of TakingIn or Blocked:
                for (int i = RulesContainer.FirstSlotIndex; i <= RulesContainer.MaxBlackBaseSlot ; i++)
                {
                    if(board.Slots[i].BlackPieces.Count  < 2)//if the white player have a chance to return his peice to the board
                    {
                        whitePlayer.PlayerState = PlayerState.TakingIn;
                        return;
                    }
                }
                whitePlayer.PlayerState = PlayerState.Blocked;
                return;
            }
            else // it can only be in state of Normal or Putting out
            {
                for (int i = RulesContainer.FirstSlotIndex; i < RulesContainer.MaxWhiteBaseSlot; i++)
                {
                    if(board.Slots[i].WhitePieces.Count != 0)// if player didnt return all pieces to base
                    {
                        whitePlayer.PlayerState = PlayerState.Normal;
                        return;
                    }
                }
                whitePlayer.PlayerState = PlayerState.TakingOut;
            }
            
        }

        private static void UpdateBlackPlayer(IPlayer blackPlayer, Board board)
        {
            //if Black player have pieces outside:
            if (board.OutPiecesSlot.BlackPieces.Count != 0)
            {
                //it can only be in state of TakingIn or Blocked:
                for (int i = RulesContainer.LastSlotIndex; i >= RulesContainer.MaxWhiteBaseSlot; i--)
                {
                    if (board.Slots[i].WhitePieces.Count < 2)//if the black player have a chance to return his peice to the board
                    { 
                        blackPlayer.PlayerState = PlayerState.TakingIn;
                        return;
                    }
                }
                blackPlayer.PlayerState = PlayerState.Blocked;
                return;
            }
            else // it can only be in state of Normal or Putting out
            {
                for (int i = RulesContainer.MaxBlackBaseSlot + 1; i <= RulesContainer.LastSlotIndex; i++)
                {
                    if (board.Slots[i].BlackPieces.Count != 0)// if player didnt return all pieces to base
                    {
                        blackPlayer.PlayerState = PlayerState.Normal;
                        return;
                    }
                }
                blackPlayer.PlayerState = PlayerState.TakingOut;
                return;
            }

        }
    }
   
}
