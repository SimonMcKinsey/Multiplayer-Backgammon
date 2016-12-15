using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClientDemo.InfraStructure
{
    public class WindowService
    {

        public static Window ShowWindow(object viewModel)
        {
            var window = new Window();
            window.ResizeMode = ResizeMode.NoResize;
            window.Content = viewModel;
            window.Height = 525;
            window.Width = 550;
            window.Show();
            return window;
        }
        public static Window ShowGameWindow(object viewModel)
        {
            var window = new Window();
            window.ResizeMode = ResizeMode.NoResize;
            window.Content = viewModel;
            window.Height = 550;
            window.Width = 1120;
            window.Show();
            return window;
        }
    }

}
