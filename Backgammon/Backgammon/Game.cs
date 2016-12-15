using Backgammon.CubesFolder;
using Backgammon.enumsFolder;
using Backgammon.GameUpdatesFolder;
using Backgammon.GameValidations;
using Backgammon.GameValidations.DestinationValidations;
using Backgammon.GameValidations.SourceValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Game
    {
        public CubePair Cubes { get; set; }
        public Board Board { get; set; }
        public List<Step> Steps { get; set; }
        public Turn Turn { get; set; }
        public IPlayer BlackPlayer { get; set; }
        public IPlayer WhitePlayer { get; set; }
        //----------------
        public ChoosenMoveDetails Move { get; set; }
        //----game events:
        public event EventHandler StepsEnded;

        public Game(string blackPlayerName, string whitePlayerName)
        {

            Cubes = CubesGenerator.Generate();
            Steps = CubesToStepsConverter.Convert(Cubes);
            Board = new Board();
            Turn = Turn.Black;
            Move = ChoosenMoveDetails.Instance;
            //Init players:
            BlackPlayer = new BlackPlayer() { UserName = blackPlayerName };
            WhitePlayer = new WhitePlayer() { UserName = whitePlayerName };

        }
        public string Roll()
        {
            Cubes = CubesGenerator.Generate();
            Steps = CubesToStepsConverter.Convert(Cubes);
            // print values of cubes:
            return TempGamePrinter.CubesValuePrint(Steps);


        }
        public bool ChooseSource(int slotIdSource, out string message)
        {
            bool SlotIdValid = IsSlotIdSourceValid.Validate(slotIdSource,BlackPlayer,WhitePlayer,Turn);
            bool IsSlotContainPiecesOfPlayer = IsTherePieceToMoveInTheSlot.Validate(slotIdSource, Turn, Board);
            message = TempGamePrinter.ChooseSourceMessages(SlotIdValid, IsSlotContainPiecesOfPlayer);
            if (SlotIdValid == false || IsSlotContainPiecesOfPlayer == false)
            {
                return false;
            }
            Move.SlotIdSource = slotIdSource;
            return true;
        }
        public bool ChooseDestination(int slotIdDestination, out string message)
        {
            //
            bool isPlayerGoingForwards = IsPlayerGoingForward.Validate(Move.SlotIdSource.Value, slotIdDestination, Turn);
            bool isDestinationInsideBorders =  IsSlotIdDestinationValid.Validate(slotIdDestination, BlackPlayer, WhitePlayer, Turn);
            bool isSlotBlocked = IsSlotBlockedByOpponent.Validate(slotIdDestination,Turn,Board);
            bool isCubeStepExist = CubeStepExistValidator.Validate(Move.SlotIdSource.Value, slotIdDestination, BlackPlayer, WhitePlayer,Turn,Steps);
            message = TempGamePrinter.ChooseDestinationMessages(isPlayerGoingForwards, isDestinationInsideBorders, isSlotBlocked, isCubeStepExist);

            if (isPlayerGoingForwards == false || isDestinationInsideBorders == false || isSlotBlocked == true || isCubeStepExist == false)
            {
                return false;
            }
            //update current move details:
            Move.SlotIdDestination = slotIdDestination;
            //updates:

            //update board : 
            BoardUpdater.Update(Move.SlotIdSource.Value, Move.SlotIdDestination.Value, Board, Turn);
            //update each user state :
            PlayersStateUpdater.Update(BlackPlayer, WhitePlayer, Board);
            //update steps left:
            StepsLeftUpdater.Update(Move.SlotIdSource.Value, Move.SlotIdDestination.Value, Steps, Turn);
            //update turn :
            Turn = TurnUpdater.Update(Turn, BlackPlayer, WhitePlayer, Steps);

            //event raise:
            if(Steps.Count == 0)
            {
                StepsEnded?.Invoke(this, EventArgs.Empty);
            }
            //refresh move:
            Move.RefreshForNewMove();
            return true;
        }



    }

}
