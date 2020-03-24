using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using TestCustomContentView.ContentViews;
using TestCustomContentView.Models;

namespace TestCustomContentView.ViewModels
{
    public class MainPageViewModel : BaseTabViewModel
    {
        FirstContentViewViewModel firstContentViewViewModel;
        public FirstContentViewViewModel FirstContentViewViewModel
        {
            get => firstContentViewViewModel;
            set => SetProperty(ref firstContentViewViewModel, value);
        }

        SecondContentViewViewModel secondContentViewViewModel;
        public SecondContentViewViewModel SecondContentViewViewModel
        {
            get => secondContentViewViewModel;
            set => SetProperty(ref secondContentViewViewModel, value);
        }

        ObservableCollection<TabItem> pages;
        public ObservableCollection<TabItem> Pages {
            get => pages;
            set => SetProperty(ref pages, value);
        }

        TabItem itemSelected;
        public TabItem ItemSelected {
            get => itemSelected;
            set => SetProperty(ref itemSelected, value);

        }

        public ICommand ItemSelectedCommand { get; set; }

        public MainPageViewModel()
        {
            var list = new List<TabItem>() {
                new TabItem{
                    Title = "First Tab",
                    ContentType= typeof(FirstContentView)
                },
                new TabItem{
                    Title = "Second Tab",
                    ContentType= typeof(SecondContentView)
                },
                new TabItem{
                    Title = "Third Tab",
                    ContentType= typeof(FirstContentView)
                },
                new TabItem{
                    Title = "fourth Tab",
                    ContentType= typeof(SecondContentView)
                },
                new TabItem{
                    Title = "fifth tab",
                    ContentType= typeof(FirstContentView)
                },
                new TabItem{
                    Title = "sixth tab",
                    ContentType= typeof(SecondContentView)
                }

            };

            ItemSelectedCommand = new AsyncCommand<TabItem>(ItemSelectedAction);
            InitViewModels();
            Pages = new ObservableCollection<TabItem>(list);
            
        }

        private Task ItemSelectedAction(TabItem tabItem)
        {
            var itemSelected= Pages.FirstOrDefault(x => x.ContentType == tabItem.ContentType);
            ItemSelected = itemSelected;
            
            return Task.FromResult(true);
        }

        private void InitViewModels()
        {
            firstContentViewViewModel = new FirstContentViewViewModel();
            SecondContentViewViewModel = new SecondContentViewViewModel();

        }
    }
}
