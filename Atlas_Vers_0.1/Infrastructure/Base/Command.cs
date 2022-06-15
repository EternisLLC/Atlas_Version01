using System;
using System.Windows.Input;

namespace Atlas_Vers_0._1
{
    public abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter); // Может ли быть выполнена команда

        public abstract void Execute(object parameter); // Выполнение команды
    }
}
