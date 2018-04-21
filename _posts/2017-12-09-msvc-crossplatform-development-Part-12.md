---
layout: single
title: MSVC Crossplatform Development Part 12
toc: true
---
**Xamarin Android App** We will create a Xamarin Android application to test the previous library. 
<!--more--> 

In the solution Explorer, Right-click the `Tests` folder and choose:

`Add -> New Project ->Visual C# -> Android -> Single Page App`

Name the project `NetAndroidApp`.

![Screenshot]({{ "/images/msvc_part12_1.png" | absolute_url }})

Add a reference to `DemoTools.NET.Android`.

Open `Resources\Layout\main.axml` and add this markup code:

## Markup

{% highlight axml %}
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="match_parent">
  <TextView
        android:id="@+id/platformText"
        android:textAppearance="?android:attr/textAppearanceLarge"
        android:layout_width="match_parent"
        android:layout_height="wrap_content" />
   <Button
        android:id="@+id/addButton"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:text="Add One" />
  <Button
        android:id="@+id/resetButton"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:text="Reset" />
  <TextView
        android:id="@+id/counterText"
        android:textAppearance="?android:attr/textAppearanceLarge"
        android:layout_width="match_parent"
        android:layout_height="wrap_content" />
</LinearLayout>
{% endhighlight %}

## Code

Next, open `mainActivity.cs` and add the code to test the library: 

{% highlight c# %}
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
{% endhighlight %}

At this point the application should run, although you might need to rebuild all libraries first if you didn't do that in the previous part. 