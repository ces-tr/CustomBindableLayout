using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestCustomContentView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CustomTabsContainer());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
