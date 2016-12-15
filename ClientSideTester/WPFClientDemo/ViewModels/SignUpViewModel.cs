using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TalkBack.ClientDemo;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities;
using WPFClientDemo.Models;

namespace WPFClientDemo.ViewModels
{
    class SignUpViewModel : ViewModelBase
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public ConsoleLog ConsoleLog { get; set; }
        public UserServiceProxy UserServiceProxy { get; set; }
        public SignUpViewModel()
        {
            ConsoleLog = new ConsoleLog();
            ConsoleLog.Document = "checking binding";
            UserServiceProxy = UserServiceProxy.Instance;//TODO: make proxy singleton?
            //init Commands:
            OnClickSignUpCommand = new MyCommand(SendSignUpRequest);
            OnClickBackCommand = new MyCommand(GoBack);
        }

        private void SendSignUpRequest()
        {
            SignUpResponse response = UserServiceProxy.SignUp(new SignUpRequest { UserName = UserName, Password = Password, PasswordConfirm = PasswordConfirm });
            ConsoleLog.Document += $" {response.IsSuccess} {response.Message}";
            //update ContactsManager:
            if (response.IsSuccess == true)
            {
                response.AllOtherSignedUpUsers.ForEach(u => ConsoleLog.Document += $"other users: \n{u.UserName}");
                ContactStateManager.AssignAllCurrentContacts(response.AllOtherSignedUpUsers);

            }


        }
        private void GoBack()
        {
            ViewChanging("LoginView");
        }

        public ICommand OnClickSignUpCommand { get; set; }
        public ICommand OnClickBackCommand { get; set; }
    }
}
