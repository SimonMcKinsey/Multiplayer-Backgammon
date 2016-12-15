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
using WPFClientDemo;
using WPFClientDemo.InfraStructure;
using WPFClientDemo.ViewModels;
using WPFClientDemo.Views;

namespace TalkBack.ClientDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private UserServiceProxy userServiceProxy;
        //private AuthenticatedUser currentLogedUser;
        public MainWindow()
        {
            InitializeComponent();
            AppViewModel appViewModel = new AppViewModel();
            DataContext = appViewModel;
            
        }

    }
}
