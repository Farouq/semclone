using System;
using System.Windows;
using System.Windows.Controls;
using ProFSharp.Events;

namespace FSharpSilverlightDemo
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            SilverlightEvents.RegisterHandlers(this);
        }
    }
}
