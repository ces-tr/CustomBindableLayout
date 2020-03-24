using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TestCustomContentView.ContentViews
{
    public partial class SecondContentView : ContentView, IDisposable
    {
        public SecondContentView()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            Console.WriteLine("Dsipose Content here");
        }
    }
}
