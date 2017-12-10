---
layout: single
title: MSVC Crossplatform Development Part 10
---
**UWP Application** This application will allow us to test the Universal Windows libary. 
<!--more--> 

In the solution Explorer, Right-click the `Tests` folder and choose:

`Add -> New Project ->Visual C# -> Universal Windows -> Blank App`

Name the project `UwpApp`.

![Screenshot]({{ "/images/msvc_part10_1.png" | absolute_url }})

Set the target an minimum version to *Windows 10 Fall Creators Update* or newer. **(Note: you can only do this if you have these windows updated installed on your development machine.)**

Next, add a reference to `DemoTools.NET.Universal`. We don't need to change any project settings.

The xaml markup and codebehind can be copied from the Uwp test project.

At this point everything is in place, but the app will crash on PInvoke. I'll leave this for now, but a good read to solve this might be this [microsoft post](https://docs.microsoft.com/en-gb/cpp/porting/how-to-use-existing-cpp-code-in-a-universal-windows-platform-app).

