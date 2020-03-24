using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MvvmHelpers.Commands;
using TestCustomContentView.Models;

namespace TestCustomContentView.ViewModels
{
    public class FirstContentViewViewModel : BaseTabViewModel
    {
        public ICommand ButtonCommand { get; set; }

        public FirstContentViewViewModel()
        {
            ButtonCommand = new AsyncCommand(ButtonAction);
        }

        private Task ButtonAction()
        {
            return UserDialogs.Instance.AlertAsync("first");         
        }

        public override Task Init<T>(T initaram = default)
        {
            return Task.FromResult(true);
        }
    }
}
