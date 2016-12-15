
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TalkBack.ClientDemo;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Entities.GameServiceEntities;
using WPFClientDemo.Models;
using WPFClientDemo.Models.ServiceProxy;

namespace WPFClientDemo.ViewModels
{
    class LoginViewModel : ViewModelBase
    {


        public AuthenticatedUser AuthenticatedUser { get; set; }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

      

        public ConsoleLog ConsoleLog { get; set; }//TODO: stringbuilder
        //Proxies Instances:
        public UserServiceProxy UserServiceProxy { get; set; }
        public ChatServiceProxy ChatServiceProxy { get; set; }
        public GameServiceProxy GameServiceProxy { get; set; }

        public LoginViewModel()
        {
            ConsoleLog = new ConsoleLog { Document = "check binding \n" }; //test           
            AuthenticatedUser = AuthenticatedUser.Instance;
            //load all contacts:
            ContactStateManager.LoadAllContacts();       
            //init Proxies:
            UserServiceProxy = UserServiceProxy.Instance;
            ChatServiceProxy = ChatServiceProxy.Instance;
            GameServiceProxy = GameServiceProxy.Instance;

            //Init Commands:
            SendLoginRequestCommand = new MyCommand(SendLoginRequest);
            ChangeViewToSignUpCommand = new MyCommand(ChangeViewToSignUp);
        }

        public ICommand SendLoginRequestCommand { get; set; }
        private void SendLoginRequest()
        {
            LoginResponse response = UserServiceProxy.Login(new LoginRequest { UserName = this.UserName, Password = this.Password });
            ConsoleLog.Document += $" { response.IsSuccess } {response.Message} \n ";
            if (response.IsSuccess == true)
            {
                //save loged user authentication
                AuthenticatedUser.Authentication = response.Authentication;
                AuthenticatedUser.UserName = this.UserName;
                //update contactList             
                ContactStateManager.AssignAllCurrentOnlineContacts(response.AllOtherUsers);
                //joining the chat service:
                JoinChatServiceResponse responseFromChatService = ChatServiceProxy.JoinService(new JoinChatServiceRequest());
                ConsoleLog.Document += responseFromChatService.Message;
                //joining the Game Service:
                JoinGameServiceResponse responseFromGameService = GameServiceProxy.JoinService(new JoinGameServiceRequest());
                ConsoleLog.Document += responseFromGameService.Message;
                UserServiceProxy.OnLoginEvent += UserServiceProxy_OnLoginEvent;
                ViewChanging("ContactListView");
                //set online users to something : new class that will save online users
            }
        }

        public ICommand ChangeViewToSignUpCommand { get; set; }
        private void ChangeViewToSignUp()
        {
            ViewChanging("SignUpView");
        }
        private void UserServiceProxy_OnLoginEvent(object sender, InfraStructure.OnLoginEventArgs e)
        {
            ConsoleLog.Document += $"{e.UserName} has logged in.";
        }
    }
}
