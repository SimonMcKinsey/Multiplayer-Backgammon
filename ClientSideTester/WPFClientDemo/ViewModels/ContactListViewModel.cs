using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TalkBack.ClientDemo;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities.GameServiceEntities;
using WPFClientDemo.InfraStructure;
using WPFClientDemo.Models;
using WPFClientDemo.Models.ServiceProxy;

namespace WPFClientDemo.ViewModels
{
    public class ContactListViewModel : ViewModelBase
    {
        private UserDTO chosenContact;
        public UserDTO ChosenContact
        {
            get { return chosenContact; }
            set
            {
                chosenContact = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<UserDTO> onlineUsers;
        public ObservableCollection<UserDTO> OnlineUsers
        {
            get
            {
                return onlineUsers;
            }
            set
            {
                onlineUsers = value;
                OnPropertyChanged();

            }
        }
        private ObservableCollection<UserDTO> offlineUsers;
        public ObservableCollection<UserDTO> OfflineUsers
        {
            get
            {
                return offlineUsers;
            }
            set
            {
                offlineUsers = value;
                OnPropertyChanged();
            }
        }

        public ConsoleLog ConsoleLog { get; set; }

        public UserServiceProxy UserServiceProxy { get; set; } //TODO: is needed?
        public GameServiceProxy GameServiceProxy { get; set; }
        public ChatServiceProxy ChatServiceProxy { get; set; }
        //ctor:
        public ContactListViewModel()
        {
            onlineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOnlineContacts());
            offlineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOfflineContacts());
            ConsoleLog = new ConsoleLog();
            //init proxies:
            UserServiceProxy = UserServiceProxy.Instance;  //TODO: is needed?
            UserServiceProxy.OnLoginEvent += UserServiceProxy_OnLoginEvent;
            UserServiceProxy.OnSignedUpEvent += UserServiceProxy_OnSignedUp;
            UserServiceProxy.OnLogoutEvent += UserServiceProxy_OnLogout;
            ChatServiceProxy = ChatServiceProxy.Instance;
            ChatServiceProxy.OnMessageSentEvent += ChatServiceProxy_OnMessageSent;
            GameServiceProxy = GameServiceProxy.Instance;
            GameServiceProxy.OnGameEvent += GameServiceProxy_OnGameEvent;
            //init ICommands:
            SendGameRequestCommand = new MyCommand(SendGameRequest);
            OpenChatWindowCommand = new MyCommand(OpenChatWindow);
            //subscribe to Closing Window event:
            Application.Current.MainWindow.Closing += MainWindow_OnWindowClosing;
        }

        public ICommand SendGameRequestCommand { get; set; }
        private void SendGameRequest()
        {
            if (ChosenContact != null)
            {
                GameResponse response = GameServiceProxy.Game(new GameRequest { UserOpponent = ChosenContact.UserName });
                if(response.IsSuccess == true) //contact accepted invetion
                {
                    Window gameWindow = GameWindowFactory.GetGameWindow(chosenContact.UserName);
                    gameWindow.Show();
                }
                ConsoleLog.Document = $" {response.IsSuccess} +\n {response.Message}";
            }
        }

        public ICommand OpenChatWindowCommand { get; set; }
        private void OpenChatWindow()
        {
            if(ChosenContact != null)
            {

            Window chatWindow = ChatWindowFactory.GetChatWindow(chosenContact.UserName, string.Empty);
            chatWindow.Show();

            }

        }

        private void MainWindow_OnWindowClosing(object sender, CancelEventArgs e)
        {
            //sending logout request:
            UserServiceProxy.Logout(new LogoutRequest { UserName = AuthenticatedUser.Instance.UserName });
            //shut down the application:
            App.Current.Shutdown();
        }

        private void ChatServiceProxy_OnMessageSent(object sender, OnMessageSentEventArgs e)
        {
            Window chatWindow = ChatWindowFactory.GetChatWindow(e.UserName, e.Message);
            chatWindow.Show();
        }

        private void UserServiceProxy_OnSignedUp(object sender, OnSignedUpEventArgs e)
        {
            //Getting the updated Offline contacts list:
            OfflineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOfflineContacts());
            ConsoleLog.Document = e.UserName + "has singed up";
        }

        private void UserServiceProxy_OnLoginEvent(object sender, OnLoginEventArgs e)
        {
            OnlineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOnlineContacts());
            OfflineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOfflineContacts());
            ConsoleLog.Document = e.UserName + "has loged in";
            //OnlineUsers.Add(new UserDTO {UserName= e.UserName });
        }

        private void GameServiceProxy_OnGameEvent(object sender, OnGameEventArgs e)
        {
            ConsoleLog.Document += $"{e.UserName} wanna play with u.";
            Window gameWindow = GameWindowFactory.GetGameWindow(e.UserName);
            gameWindow.Show();

        }
        private void UserServiceProxy_OnLogout(object sender, OnLogoutEventArgs e)
        {
            OnlineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOnlineContacts());
            OfflineUsers = new ObservableCollection<UserDTO>(ContactStateManager.GetOfflineContacts());
            ConsoleLog.Document += $"{e.UserName} has loged out";
        }
    }
}
