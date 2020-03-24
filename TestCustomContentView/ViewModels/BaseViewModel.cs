using System.Threading.Tasks;

namespace TestCustomContentView.ViewModels
{
    public class BaseTabViewModel : MvvmHelpers.BaseViewModel, IViewModelInitializable
    {

        public virtual Task Init<T>(T initaram = default)
        {
            return Task.FromResult(true);
        }
    }

    public interface IViewModelInitializable
    {
        Task Init<T>(T initaram = default);
    }
}