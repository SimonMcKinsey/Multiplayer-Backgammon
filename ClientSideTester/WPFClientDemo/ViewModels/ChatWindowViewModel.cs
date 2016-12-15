using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Entities.ChatServiceEntities;
using TalkBackWCF.Contract.Services;
using WPFClientDemo.InfraStructure;
using WPFClientDemo.Models;

namespace WPFClientDemo.ViewModels
{
    public class ChatWindowViewModel : ViewModelBase
    {
        public IUserName ChosenContact { get; set; }
        private string messageContent;

        public string MessageContent
        {
            get { return messageContent; }
            set
            {
                messageContent = value;
                OnPropertyChanged();
            }
        }
        private string messagesLog;

        public string MessagesLog
        {
            get { return messagesLog; }
            set
            {
                messagesLog = value;
                OnPropertyChanged();
            }
        }


        //proxies:
        public ChatServiceProxy ChatServiceProxy { get; set; }
        //ctor:
        public ChatWindowViewModel(IUserName chosenContact)
        {
            ChosenContact = chosenContact;
            //init proxies:
            ChatServiceProxy = ChatServiceProxy.Instance;
            ChatServiceProxy.OnMessageSentEvent += ChatServiceProxy_OnMessageSentEvent;
            //init commands:
            SendMessageCommand = new MyCommand(SendMessage); //sendMessageRequest
        }
        private void SendMessage()
        {
            SendMessageResponse response = ChatServiceProxy.SendMessage(new SendMessageRequest { Message = messageContent, UserName = ChosenContact.UserName });
            if (response.IsSuccess == true)
            {
                MessagesLog += ChatMessageFormatter.Format(messageContent, AuthenticatedUser.Instance.UserName);
            }
            else
            {
                MessagesLog += ChatMessageFormatter.Format(response.Message, AuthenticatedUser.Instance.UserName);
            }
            MessageContent = string.Empty; //clear the message send box

        }

        public ICommand SendMessageCommand { get; set; }

        private void ChatServiceProxy_OnMessageSentEvent(object sender, OnMessageSentEventArgs e)
        {
          MessagesLog += ChatMessageFormatter.Format(e.Message,e.UserName);
        }
    }
}
