using DemoTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Counter NewCounter = new Counter();

        public MainPage()
        {
            this.InitializeComponent();

            DemoTools.PlatformID PID = new DemoTools.PlatformID();
            PlatformText.Text = PID.Get();
        }

        private void MainCounterAdd_Click(object sender, RoutedEventArgs e)
        {
            DemoTools.DemoTools.MainCounter().Add(1);
            MainCounterOutput.Text = "Value: " + DemoTools.DemoTools.MainCounter().Get();
        }

        private void MainCounterReset_Click(object sender, RoutedEventArgs e)
        {
            DemoTools.DemoTools.MainCounter().Reset();
            MainCounterOutput.Text = "Value: " + DemoTools.DemoTools.MainCounter().Get();
        }

        private void NewCounterAdd_Click(object sender, RoutedEventArgs e)
        {
            NewCounter.Add(1);
            NewCounterOutput.Text = "Value: " + NewCounter.Get();
        }

        private void NewCounterReset_Click(object sender, RoutedEventArgs e)
        {
            NewCounter.Reset();
            NewCounterOutput.Text = "Value: " + NewCounter.Get();
        }
    }
}
