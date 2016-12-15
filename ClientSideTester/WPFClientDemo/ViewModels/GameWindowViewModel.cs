using Backgammon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities.GameServiceEntities;
using WPFClientDemo.Commands;
using WPFClientDemo.InfraStructure.EventArgsFolder;
using WPFClientDemo.Models;
using WPFClientDemo.Models.ServiceProxy;

namespace WPFClientDemo.ViewModels
{
    public class GameWindowViewModel : ViewModelBase
    {

        public IUserName ChosenContact { get; set; }
        private Slot _chosenSlot;

        public Slot ChosenSlot
        {
            get { return _chosenSlot; }
            set
            {
                _chosenSlot = value;
                OnPropertyChanged();
                onChosenSlotChanged?.Invoke(value, EventArgs.Empty);
            }
        }
        public event EventHandler onChosenSlotChanged;
        //UI Slots:
        private ObservableCollection<Slot> _slotsFirstRow;
        public ObservableCollection<Slot> SlotsFirstRow
        {
            get { return _slotsFirstRow; }
            set
            {
                _slotsFirstRow = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Slot> _slotsSecondRow;
        public ObservableCollection<Slot> SlotsSecondRow
        {
            get { return _slotsSecondRow; }
            set
            {
                _slotsSecondRow = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Slot> _outPiecesSlot;
                    
        public ObservableCollection<Slot> OutPiecesSlot
        {
            get { return _outPiecesSlot; }
            set {
                _outPiecesSlot = value;
                OnPropertyChanged();
            }
        }


        public ConsoleLog ConsoleLog { get; set; }
        private GameManager gameManager;
        private ObservableCollection<Step> cubesSteps;

        public ObservableCollection<Step> CubesSteps
        {
            get { return cubesSteps; }
            set
            {
                cubesSteps = value;
                OnPropertyChanged();
            }
        }
        public bool CanRoll { get; set; }
        //proxies:
        public GameServiceProxy GameServiceProxy { get; set; }
        public GameWindowViewModel(IUserName chosenContact)
        {
            //get opponent:
            ChosenContact = chosenContact;
            //init Game:
            gameManager = new GameManager("hen", "simon");
            gameManager.GameChanged += GameManager_OnGameChanged;
            gameManager.Game.StepsEnded += Game_OnStepsEnded;
            //init proxies:
            GameServiceProxy = GameServiceProxy.Instance;
            GameServiceProxy.OnInGameEvent += GameServiceProxy_OnInGame;
            //init UI Slots:
            RefreshGameUI();
            cubesSteps = new ObservableCollection<Step>(gameManager.Game.Steps);
            ConsoleLog = new ConsoleLog();
            //chosenSlotInit:
            onChosenSlotChanged += slotClicked;
            //init Commands:
            RollCubesCommand = new MyCommand(RollCubes);



        }

        private void GameServiceProxy_OnInGame(object sender, OnInGameEventArgs e)
        {
            
          //slotClicked(new Slot { Id = e.SlotIdSource },EventArgs.Empty);
          //  slotClicked(new Slot { Id = e.SlotIdDestination }, EventArgs.Empty);
        }

        private void RollCubes()
        {
            if (CanRoll == true)
            {

                ConsoleLog.Document = gameManager.Roll();
                RefreshGameUI();
                CanRoll = false;
            }
        }

        public ICommand RollCubesCommand { get; set; }
        private void Game_OnStepsEnded(object sender, EventArgs e)
        {
            CanRoll = true;
        }

        private void GameManager_OnGameChanged(object sender, OnInGameEventArgs e)
        {
            RefreshGameUI();
           // GameServiceProxy.InGame(new InGameRequest { UserOpponent = ChosenContact.UserName, SlotIdSource = e.SlotIdSource, SlotIdDestination = e.SlotIdDestination });
            
        }

        private void slotClicked(object sender, EventArgs e)
        {

            Slot slotClicked = (Slot)sender;
            string message = gameManager.SlotClicked(slotClicked);
            ConsoleLog.Document = message;

        }
        private void RefreshGameUI()
        {
            //take first 12 slots of board (1-12)
            SlotsFirstRow = new ObservableCollection<Slot>(gameManager.Game.Board.Slots.OrderBy(s => s.Id).Take(13));
            //take second 12 slots of board (13-24)
            SlotsSecondRow = new ObservableCollection<Slot>(gameManager.Game.Board.Slots.OrderBy(s => s.Id).Skip(13).Take(13));
            //take the out pieces slot:
            List<Slot> outPiecesSlot = new List<Slot>() { gameManager.Game.Board.OutPiecesSlot };
            OutPiecesSlot = new ObservableCollection<Slot>(outPiecesSlot);
            CubesSteps = new ObservableCollection<Step>(gameManager.Game.Steps);
        }


    }
}
