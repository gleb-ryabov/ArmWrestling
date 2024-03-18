using System;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.Commands
{
    public class ParameterizedCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public ParameterizedCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null && parameter is T castParameter)
            {
                _execute(castParameter);
            }
        }
    }
}
