using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace TestCustomContentView.ContentViews
{
    public partial class FirstContentView : ContentView, IDisposable
    {
        public FirstContentView()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            Console.WriteLine("Dsipose Content here");

        }
    }
}
