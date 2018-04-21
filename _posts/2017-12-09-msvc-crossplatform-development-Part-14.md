---
layout: single
toc: true
title: MSVC Crossplatform Development Part 14
---
**Xamarin Forms Application** The new PCL library will now be used in a Xamarin Forms application. We will only be able to run it from android right now, but this will be enough to demonstrate the workflow.
<!--more--> 

Start by create a new project in the tests folder, called `XamarinApp`.

- `Add -> New Project -> Visual C++ -> Cross Platform -> Xamarin Forms`

![Screenshot]({{ "/images/msvc_part14_1.png" | absolute_url }})

In the next screen, pick the Portable Class (PCL) approach, not the shared library. If you want to implement UWP, make sure you set to minimum framework version to the Windows 10 Fall Creators update or higher. *(Why? See the part about UWP libraries.)*

## XamarinApp (Portable)
- Add a reference to `DemoTools.NET.PCL`.
- Add new class `Global.cs` with a reference to the manager interface:

{% highlight c# %}
namespace XamarinApp
{
    public static class Global
    {
        public static DemoTools.IManager DT;
    }
}
{% endhighlight %}

- Change `MainPage.xaml` to:

{% highlight xaml %}
<StackLayout>

    <StackLayout Orientation="Horizontal">
        <Button x:Name="Counter1AddButton" Text="Add One" Clicked="Counter1AddButton_Clicked"/>
        <Button x:Name="Counter1ResetButton" Text="Reset" Clicked="Counter1ResetButton_Clicked"/>
        <Label x:Name="Counter1Label" Text=""/>

    </StackLayout>

    <StackLayout Orientation="Horizontal">
        <Button x:Name="Counter2AddButton" Text="Add One" Clicked="Counter2AddButton_Clicked"/>
        <Button x:Name="Counter2ResetButton" Text="Reset" Clicked="Counter2ResetButton_Clicked"/>
        <Label x:Name="Counter2Label" Text=""/>
    </StackLayout>

    <Label x:Name="IDLabel" Text="not ready"
        VerticalOptions="CenterAndExpand" 
        HorizontalOptions="CenterAndExpand" />
</StackLayout>
{% endhighlight %}

- Now add the codebehind:

{% highlight c# %}

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
{% endhighlight %}

## XamarinApp.Android

- Add a reference to `DemoTools.NET.PCL` and to `DemoTools.NET.Android`.
- In `MainActivity.cs`, below `base.OnCreate(bundle);` add:

{% highlight c# %}
XamarinApp.Global.DT = new PCLDemoTools.Manager();
{% endhighlight %}

This will create the actual manager object and make it available to the PCL. It's probably the only line of code you will have to add to your Android project for your library. Nothing would be better, but it's not too bad.





