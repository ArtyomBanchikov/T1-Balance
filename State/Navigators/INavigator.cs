using System.Windows.Input;
using T1Balance.Core;

namespace T1Balance.State.Navigators
{
    public enum ViewType
    {
        Info,
        Settings
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
