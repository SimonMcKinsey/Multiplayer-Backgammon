using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TalkBackWCF.Contract;
namespace TalkBack.ClientDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserServiceProxy userServiceProxy;
        private Guid logedUser;
        public MainWindow()
        {
            InitializeComponent();
            userServiceProxy = new UserServiceProxy();
            logedUser = Guid.Empty;
            userServiceProxy.OnLoginEvent += Proxy_OnLoginEvent;
        }
        private void Proxy_OnLoginEvent(object sender, OnLoginEventArgs e)
        {
            consoleLog.Text += $"\n {e.UserName} has just loged in ";
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            LoginResponse response = userServiceProxy.Login(new LoginRequest {    UserName = userName.Text, Password = password.Text  });
            consoleLog.Text += response.IsSuccess + " " + response.Message + "\n";
            foreach (var user in response.AllOtherUsers)
            {

                consoleLog.Text += user.UserName + "\n";
            }
            if(response.IsSuccess == true)
            {
                logedUser = response.Authentication;
            }
            consoleLog.Text +="\n the authentication" + response.Authentication;
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            //LogoutResponse response = userServiceProxy.Logout(new LogoutRequest { Authentication = logedUser });
            //consoleLog.Text+= response.IsSuccess + " " + response.Message + "\n";

        }
    }
}
