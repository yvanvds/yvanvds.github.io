using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        DemoTools.ICounter Counter;
        DemoTools.IPlatformID PID = Global.DT.CreatePlatformID();

        public MainPage()
        {
            InitializeComponent();

            Counter = Global.DT.CreateCounter();
            Counter1Label.Text = "" + Counter.Value;

            Counter2Label.Text = "" + Global.DT.MainCounter().Value;

            IDLabel.Text = PID.Value;
        }

        private void Counter1ResetButton_Clicked(object sender, EventArgs e)
        {
            Counter.Reset();
            Counter1Label.Text = "" + Counter.Value;
        }

        private void Counter1AddButton_Clicked(object sender, EventArgs e)
        {
            Counter.Add(1);
            Counter1Label.Text = "" + Counter.Value;
        }

        private void Counter2ResetButton_Clicked(object sender, EventArgs e)
        {
            Global.DT.MainCounter().Reset();
            Counter2Label.Text = "" + Global.DT.MainCounter().Value;
        }

        private void Counter2AddButton_Clicked(object sender, EventArgs e)
        {
            Global.DT.MainCounter().Add(Counter);
            Counter2Label.Text = "" + Global.DT.MainCounter().Value;
        }
    }
} 
