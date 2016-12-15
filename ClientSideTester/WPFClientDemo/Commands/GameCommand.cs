using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFClientDemo.Commands
{
    //class GameCommand : ICommand
    //{
    //    private Action _execute;
    //    private Func<bool> _canExecute;

    //    public GameCommand(Action execute, Func<bool> canExecute)
    //    {
    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }


    //    public void RaiseCanExecuteChanged()
    //    {
    //        if (CanExecuteChanged != null)
    //            CanExecuteChanged(this, new EventArgs());
    //    }
    //    public bool CanExecute(object parameter)
    //    {
    //        if (_canExecute != null)
    //            _canExecute();
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }
    //    public void Execute(object parameter)
    //    {
    //        if (_execute != null)
    //            _execute();
    //    }
    //}
}
