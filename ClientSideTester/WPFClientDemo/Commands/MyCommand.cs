using System;
using System.Windows.Input;

namespace WPFClientDemo.ViewModels
{
    internal class MyCommand : ICommand
    {
        private Action _execute;
        public MyCommand(Action execute)
        {
            _execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute();
        }
    }
}