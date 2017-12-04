---
layout: post
title: MSVC Crossplatform Development Part 8
---
**WPF Application** The managed library from the previous part can easily be tested in a WPF application. 
<!--more--> 

In the solution Explorer, Right-click the `Tests` folder and choose:

`Add -> New Project -> Visual C# -> Windows Classic Destop -> WPF App`

Name the project `WpfApp`.

![Screenshot]({{ "/images/msvc_part8_1.png" | absolute_url }})

Next, add a reference to `DemoTools.NET.Framework`. We don't need to change any project settings.

For testing we'll add some code to `MainWindow.xaml`.
{% highlight xaml %}
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="50"/>
            <RowDefinition Height="50"/>
            <RowDefinition Height="50"/>
        </Grid.RowDefinitions>

        <TextBlock Grid.Row="0" Grid.Column="0" x:Name="PlatformText"/>
        <StackPanel Grid.Row="1" Grid.Column="0" Orientation="Horizontal">
            <Button x:Name="MainCounterAdd" Content="Add" Padding="10" Click="MainCounterAdd_Click"/>
            <Button x:Name="MainCounterReset" Content="Reset" Padding="10" Click="MainCounterReset_Click"/>
            <TextBlock x:Name="MainCounterOutput" Padding="10" VerticalAlignment="Center" Text="Value: 0"/>
        </StackPanel>
        <StackPanel Grid.Row="2" Grid.Column="0" Orientation="Horizontal">
            <Button x:Name="NewCounterAdd" Content="Add" Padding="10" Click="NewCounterAdd_Click"/>
            <Button x:Name="NewCounterReset" Content="Reset" Padding="10" Click="NewCounterReset_Click"/>
            <TextBlock x:Name="NewCounterOutput" Padding="10" VerticalAlignment="Center" Text="Value: 0"/>
        </StackPanel>
    </Grid>
</Window>
{% endhighlight %}

And last, implement the button methods created above in `MainWindow.xaml.cs`.
{% highlight C# %}
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

{% endhighlight %}