using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace T1Balance.Commands
{
    public abstract class AsyncBaseCommand : ICommand
    {
        private bool isExecuting;
        public bool IsExecuting
        {
            get { return isExecuting; }
            set
            {
                isExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsExecuting = false;
            }
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}
