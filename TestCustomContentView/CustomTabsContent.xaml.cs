using System;
using System.Collections.Generic;
using TestCustomContentView.BindableLayouts;
using TestCustomContentView.ViewModels;
using Xamarin.Forms;

namespace TestCustomContentView
{
    public partial class CustomTabsContainer : BaseCustomTabsContainer
    {
        public CustomTabsContainer()
        {
            
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            //CustomBindableLayout.SetBindableLayoutController(TabControl, CustomBindableLayout.Get);
        }
    }
}
