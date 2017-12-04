using DemoTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Counter NewCounter = new Counter();

        public MainWindow()
        {
            InitializeComponent();

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
