using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MvvmHelpers.Commands;

namespace TestCustomContentView.ViewModels
{
    public class SecondContentViewViewModel : BaseTabViewModel
    {
        public ICommand ButtonCommand { get; set; }

        public SecondContentViewViewModel()
        {
            ButtonCommand = new AsyncCommand(ButtonAction);
        }

        private Task ButtonAction()
        {
            return UserDialogs.Instance.AlertAsync("second");
        }

        public override Task Init<T>(T initaram = default)
        {
            return Task.FromResult(true);
        }
    }
}
