using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tema2_MemoryGame.Commands
{
    internal class RelayCommand : ICommand
    {
        private Action<object> commandTask;
        private Predicate<object> canExecuteTask;

        public RelayCommand(Action<object> workToDo, Predicate<object> canExecute=null)
        {
            commandTask = workToDo;
            canExecuteTask = canExecute ?? DefaultCanExecute;
        }
        private static bool DefaultCanExecute(object parameter) => true;
    
        public bool CanExecute(object parameter) => canExecuteTask != null && canExecuteTask(parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public void Execute(object parameter) => commandTask(parameter);
    }
}
