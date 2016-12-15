using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TalkBackWCF.Contract;
using WPFClientDemo.ViewModels;

namespace WPFClientDemo.InfraStructure
{
    class ChatWindowFactory
    {
        private static Dictionary<string,KeyValuePair<ChatWindowViewModel, Window>> ChatWindowPerContact;
        static ChatWindowFactory()
        {
            ChatWindowPerContact = new Dictionary<string, KeyValuePair<ChatWindowViewModel, Window>>();
        }
        public static Window GetChatWindow(string chatPartnerName,string message)
        {
            if (ChatWindowPerContact.ContainsKey(chatPartnerName))
            {
                ChatWindowViewModel ExistchatWindow =ChatWindowPerContact[chatPartnerName].Key;
                return ChatWindowPerContact[chatPartnerName].Value;
            }

            ChatWindowViewModel chatWindowViewModel = new ChatWindowViewModel(new UserDTO { UserName = chatPartnerName });
            chatWindowViewModel.MessagesLog += (message != string.Empty) ? ChatMessageFormatter.Format(message, chatPartnerName) : ""; 
            Window chatWindow=  WindowService.ShowWindow(chatWindowViewModel);
            chatWindow.Closing += OnWindowClosing;
            ChatWindowPerContact.Add(chatPartnerName, new KeyValuePair<ChatWindowViewModel,Window>(chatWindowViewModel, chatWindow));
            return chatWindow;
        }

        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Window chatWindow =sender as Window;
            e.Cancel = true;
            chatWindow.Visibility = Visibility.Hidden;
        }
    }
}
