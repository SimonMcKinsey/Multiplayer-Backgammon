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
    class GameWindowFactory
    {
        private static Dictionary<string, KeyValuePair<GameWindowViewModel, Window>> GameWindowPerContact;
        static GameWindowFactory()
        {
            GameWindowPerContact = new Dictionary<string, KeyValuePair<GameWindowViewModel, Window>>();
        }
        public static Window GetGameWindow(string gamePartnerName)
        {
            if (GameWindowPerContact.ContainsKey(gamePartnerName))
            {
                GameWindowViewModel ExistchatWindow = GameWindowPerContact[gamePartnerName].Key;
                return GameWindowPerContact[gamePartnerName].Value;
            }

            GameWindowViewModel gameWindowViewModel = new GameWindowViewModel(new UserDTO { UserName = gamePartnerName });
            Window gameWindow = WindowService.ShowGameWindow(gameWindowViewModel);
            gameWindow.Closing += OnWindowClosing;
            GameWindowPerContact.Add(gamePartnerName, new KeyValuePair<GameWindowViewModel, Window>(gameWindowViewModel, gameWindow));
            return gameWindow;
        }

        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Window chatWindow = sender as Window;
            e.Cancel = true;
            chatWindow.Visibility = Visibility.Hidden;
        }
    }
}
