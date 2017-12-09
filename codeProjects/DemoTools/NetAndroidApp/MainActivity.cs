using Android.App;
using Android.Widget;
using Android.OS;
using DemoTools;

namespace NetAndroidApp
{
    [Activity(Label = "NetAndroidApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            TextView PlatformText = FindViewById<TextView>(Resource.Id.platformText);
            PlatformID PID = new PlatformID();
            PlatformText.Text = PID.Get();

            TextView CounterText = FindViewById<TextView>(Resource.Id.counterText);

            Button button = FindViewById<Button>(Resource.Id.addButton);
            button.Click += delegate 
            {
                DemoTools.DemoTools.MainCounter().Add(1);
                CounterText.Text = "Count " + DemoTools.DemoTools.MainCounter().Get();
            };

            Button button2 = FindViewById<Button>(Resource.Id.resetButton);
            button2.Click += delegate
            {
                DemoTools.DemoTools.MainCounter().Reset();
                CounterText.Text = "Count " + DemoTools.DemoTools.MainCounter().Get();
            };
        }
    }
}

